#I @"packages/FAKE/tools"
#r "FakeLib.dll"

open System
open System.IO
open System.Text
open Fake
open Fake.FileUtils
open Fake.TaskRunnerHelper

//--------------------------------------------------------------------------------
// Information about the project for Nuget and Assembly info files
//-------------------------------------------------------------------------------

let product = "Faker"
let authors = [ "Aaron Stannard" ]
let copyright = "Copyright © 2011-2016"
let company = "Aaron Stannard"
let description = "Procedurally generated, randomized data for your C# POCO classes."
let tags = ["faker"; "fakes"; "mock"; "mocks"; "FsCheck"; "QuickCheck"]
let configuration = "Release"

// Read release notes and version
let parsedRelease =
    File.ReadLines "RELEASE_NOTES.md"
    |> ReleaseNotesHelper.parseReleaseNotes

let envBuildNumber = System.Environment.GetEnvironmentVariable("BUILD_NUMBER") //populated by TeamCity build agent
let buildNumber = if String.IsNullOrWhiteSpace(envBuildNumber) then "0" else envBuildNumber

let version = parsedRelease.AssemblyVersion + "." + buildNumber
let preReleaseVersion = version + "-beta" //suffixes the assembly for pre-releases

let isUnstableDocs = hasBuildParam "unstable"
let isPreRelease = hasBuildParam "nugetprerelease"
let release = if isPreRelease then ReleaseNotesHelper.ReleaseNotes.New(version, version + "-beta", parsedRelease.Notes) else parsedRelease
let isMono = Type.GetType("Mono.Runtime") <> null;

//--------------------------------------------------------------------------------
// Directories

let binDir = "bin"
let testOutput = FullName "TestResults"
let perfOutput = FullName "PerfResults"
let docWorking = FullName "_site"

let nugetDir = binDir @@ "nuget"
let workingDir = binDir @@ "build"
let nugetExe = FullName @".nuget\NuGet.exe"

open Fake.RestorePackageHelper
Target "RestorePackages" (fun _ -> 
     "./Faker.sln"
     |> RestoreMSSolutionPackages (fun p ->
         { p with
             OutputPath = "./packages"
             Retries = 4 })
 )

//--------------------------------------------------------------------------------
// Clean build results

Target "Clean" (fun _ ->
    DeleteDir binDir
)

//--------------------------------------------------------------------------------
// Generate AssemblyInfo files with the version for release notes 


open AssemblyInfoFile

Target "AssemblyInfo" (fun _ ->
    let version = release.AssemblyVersion

    CreateCSharpAssemblyInfoWithConfig "src/SharedAssemblyInfo.cs" [
        Attribute.Company company
        Attribute.Copyright copyright
        Attribute.Version version
        Attribute.FileVersion version ] <| AssemblyInfoFileConfig(false)
)

//--------------------------------------------------------------------------------
// Build the solution

Target "Build" (fun _ ->
    !!"Faker.sln"
    |> MSBuildRelease "" "Rebuild"
    |> ignore
)


//--------------------------------------------------------------------------------
// Copy the build output to bin directory
//--------------------------------------------------------------------------------

Target "CopyOutput" (fun _ ->    
    let copyOutput project =
        let src = "src" @@ project @@ "bin" @@ "Release" 
        let dst = binDir @@ project
        CopyDir dst src allFiles
    [ "Faker"
    ]
    |> List.iter copyOutput
)

Target "BuildRelease" DoNothing

//--------------------------------------------------------------------------------
// DocFx targets
//--------------------------------------------------------------------------------
open Fake.DocFxHelper
Target "CleanDocs" (fun _ ->
    DeleteDir docWorking
    CreateDir docWorking
)

Target "DocFx" (fun _ ->
    DocFx (fun p -> { p with Timeout = TimeSpan.FromMinutes 5.0; WorkingDirectory  = currentDirectory; DocFxJson = currentDirectory @@ "docfx.json"})
)

//--------------------------------------------------------------------------------
// Tests targets
//--------------------------------------------------------------------------------

open Fake.Testing
let filterPlatformSpecificAssemblies (assembly:string) =
    match assembly with
    | assembly when (assembly.Contains("PerformanceCounters") && isMono) -> false
    | _ -> true

Target "RunTests" <| fun _ ->
    let unitTestAssemblies = Seq.filter filterPlatformSpecificAssemblies (!! "tests/**/bin/Release/*.Tests.dll" ++ "tests/**/bin/Release/*.Tests.End2End.dll")
    printfn "Are we running on Mono? %b" isMono
    for assembly in xunitTestAssemblies do
         printfn "Executing: %s" assembly

    mkdir testOutput
    //let xunitToolPath = findToolInSubPath "xunit.console.exe" "packages/xunit.runner.console*/tools"
    printfn "Using NUnit runner: %s" xunitToolPath
    xUnit2
        (fun p -> { p with XmlOutputPath = Some (testOutput @@ @"XUnitTestResults.xml"); HtmlOutputPath = Some (testOutput @@ @"XUnitTestResults.HTML"); ToolPath = xunitToolPath; TimeOut = System.TimeSpan.FromMinutes 30.0; Parallel = ParallelMode.NoParallelization })

        xunitTestAssemblies

//--------------------------------------------------------------------------------
// Clean test output

Target "CleanTests" <| fun _ ->
    DeleteDir testOutput


//--------------------------------------------------------------------------------
// NBench targets
//--------------------------------------------------------------------------------
Target "NBench" <| fun _ ->
    let testSearchPath =
        let assemblyFilter = getBuildParamOrDefault "spec-assembly" String.Empty
        sprintf "tests/**/bin/Release/*%s*.Tests.Performance.dll" assemblyFilter

    mkdir perfOutput
    let nbenchTestPath = findToolInSubPath "NBench.Runner.exe" "src/packges/NBench.Runner*"
    let nbenchTestAssemblies = Seq.filter filterPlatformSpecificAssemblies (!! testSearchPath)
    printfn "Using NBench.Runner: %s" nbenchTestPath
    for assembly in nbenchTestAssemblies do
         printfn "Executing: %s" assembly

    let rec runNBench assembly trialsLeft =
        let spec = getBuildParam "spec"

        let args = new StringBuilder()
                |> append assembly
                |> append (sprintf "output-directory=\"%s\"" perfOutput)
                |> append (sprintf "trace=\"%b\"" true)
                |> toText
        try
            let result = ExecProcess(fun info -> 
                info.FileName <- nbenchTestPath
                info.WorkingDirectory <- (Path.GetDirectoryName (FullName nbenchTestPath))
                info.Arguments <- args) (System.TimeSpan.FromMinutes 15.0) (* Reasonably long-running task. *)
            if result <> 0 then failwithf "NBench.Runner failed. %s %s" nbenchTestPath args
        with exn -> 
            if (trialsLeft > 0) then (runNBench assembly (trialsLeft-1))
            else raise exn
    
    for assembly in (nbenchTestAssemblies |> Seq.rev) do
        runNBench assembly 2

//--------------------------------------------------------------------------------
// Clean NBench output
Target "CleanPerf" <| fun _ ->
    DeleteDir perfOutput


//--------------------------------------------------------------------------------
// Help 
//--------------------------------------------------------------------------------

Target "Help" <| fun _ ->
    List.iter printfn [
      "usage:"
      "build [target]"
      ""
      " Targets for building:"
      " * Build      Builds"
      " * Nuget      Create and optionally publish nugets packages"
      " * RunTests   Runs tests"
      " * MultiNodeTests  Runs the slower multiple node specifications"
      " * All        Builds, run tests, creates and optionally publish nuget packages"
      ""
      " Other Targets"
      " * Help       Display this help" 
      " * HelpNuget  Display help about creating and pushing nuget packages" 
      " * HelpDocs   Display help about creating and pushing API docs"
      " * HelpMultiNodeTests  Display help about running the multiple node specifications"
      ""]

//--------------------------------------------------------------------------------
//  Target dependencies
//--------------------------------------------------------------------------------

// build dependencies
"Clean" ==> "AssemblyInfo" ==> "RestorePackages" ==> "Build" ==> "CopyOutput" ==> "BuildRelease"

// tests dependencies
"CleanTests" ==> "RunTests"
//"CleanTests" ==> "MultiNodeTests"

// NBench dependencies
"CleanPerf" ==> "NBench"

// nuget dependencies
//"CleanNuget" ==> "CreateNuget"
//"CleanNuget" ==> "BuildRelease" ==> "Nuget"

//docs dependencies
"BuildRelease" ==> "DocFx"
"CleanDocs" ==> "DocFx"

Target "All" DoNothing
"BuildRelease" ==> "All"
"RunTests" ==> "All"
//"MultiNodeTests" ==> "All"
"NBench" ==> "All"
//"Nuget" ==> "All"

Target "AllTests" DoNothing //used for Mono builds, due to Mono 4.0 bug with FAKE / NuGet https://github.com/fsharp/fsharp/issues/427
"BuildRelease" ==> "AllTests"
"RunTests" ==> "AllTests"
//"MultiNodeTests" ==> "AllTests"

RunTargetOrDefault "Help"