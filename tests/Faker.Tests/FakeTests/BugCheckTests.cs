using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Faker.Tests.FakeTests
{
    /// <summary>
    /// Tests used for resolving real-world bugs that have been found in the course of using Faker
    /// </summary>
    
    public class BugCheckTests
    {
        #region TrafficSimulator LoggedHttpRequest bug

        /// <summary>
        /// Grabs the important parts of an HTTP request and stuffs them into a format
        /// that can be used for replay later
        /// </summary>
        [Serializable]
        public class LoggedHttpRequest
        {
            public LoggedHttpRequest()
                : this(new Dictionary<string, string>())
            {
            }

            public LoggedHttpRequest(Dictionary<string, string> headers)
            {
                Headers = headers;
                CaptureTime = DateTime.UtcNow.Ticks;
            }

            /// <summary>
            /// The time this log was captured, expressed as <see cref="DateTime.UtcNow"/> Ticks
            /// </summary>
            public long CaptureTime { get; set; }

            /// <summary>
            /// Root of the url
            /// </summary>
            public string Uri { get; set; }

            /// <summary>
            /// The local path (used to translate requests to a new host)
            /// </summary>
            public string Path { get; set; }

            /// <summary>
            /// The HTTP verb used in the original request
            /// </summary>
            private string _httpMethod;

            public string HttpMethod
            {
                get { return _httpMethod; }
                set { _httpMethod = value.ToUpperInvariant(); } //force uppercase on HTTP verbs
            }

            /// <summary>
            /// The user agent
            /// </summary>
            public string UserAgent { get; set; }

            /// <summary>
            /// All of the headers
            /// </summary>
            public Dictionary<string, string> Headers { get; private set; }

            /// <summary>
            /// The body of the request
            /// </summary>
            public string Body { get; set; }

            /// <summary>
            /// The MIME header for this request's content
            /// </summary>
            public string ContentType { get; set; }
        }

        /// <summary>
        /// TODO: this test fails because of the Headers dictionary property that Faker handles. Need to add support for generic dictionaries
        /// or at least circumvent this bug
        /// </summary>
        [Fact(Skip = "Currently not implemented")]
        public void BugFix_Should_fake_LoggedHttpRequest()
        {
            //arrange
            var fake = new Fake<LoggedHttpRequest>().SetProperty(x => x.CaptureTime, () => new DateTime().Ticks);

            //act
            var instances = fake.Generate(1);

            //assert
            Assert.Equal(1, instances.Count);
        }

        #endregion
    }
}
