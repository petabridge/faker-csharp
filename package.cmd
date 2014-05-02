if not exist Download mkdir Download
if not exist Download\packages mkdir Download\packages
if not exist Download\packages\Faker mkdir Download\packages\Faker
if not exist Download\packages\Faker\lib mkdir Download\packages\Faker\lib
if not exist Download\packages\Faker\lib\net4 mkdir Download\packages\Faker\lib\net4
if not exist Download\packages\Faker\lib\net35 mkdir Download\packages\Faker\lib\net35

copy LICENSE.txt Download\packages\Faker

echo %CD%\Faker.sln

msbuild "%CD%\Faker.sln" "/p:Configuration=Release;"
copy %CD%\Faker\bin\Release\Faker.dll Download\packages\Faker\lib\net4
copy %CD%\Faker.Net35\bin\Release\Faker.Net35.dll Download\packages\Faker\lib\net35

%CD%\.nuget\NuGet.exe pack Faker.nuspec -BasePath Download\packages\Faker -OutputDirectory Download