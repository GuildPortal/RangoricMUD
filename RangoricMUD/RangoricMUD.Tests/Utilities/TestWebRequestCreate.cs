#region License

// RangoricMUD is licensed under the Open Game License.
// The original code and assets provided in this repository are Open Game Content,
// The name RangoricMUD is product identity, and can only be used as a part of the code,
//   or in reference to this project.
// 
// More details and the full text of the license are available at:
//   https://github.com/Rangoric/RangoricMUD/wiki/Open-Game-License
// 
// RangoricMUD's home is at: https://github.com/Rangoric/RangoricMUD

#endregion

#region References

using System;
using System.IO;
using System.Net;
using Moq;

#endregion

namespace RangoricMUD.Tests.Utilities
{
    public class TestWebRequestCreate : IWebRequestCreate
    {
        public Mock<WebRequest> WebRequest { get; set; }
        public Mock<WebResponse> WebResponse { get; set; }

        #region IWebRequestCreate Members

        public WebRequest Create(Uri tUri)
        {
            WebRequest = new Mock<WebRequest>();
            WebResponse = new Mock<WebResponse>();

            WebRequest.SetupProperty(t => t.Headers, new WebHeaderCollection());

            WebRequest.Setup(t => t.GetRequestStream()).Returns(new MemoryStream());
            WebResponse.Setup(t => t.GetResponseStream()).Returns(new MemoryStream());

            WebRequest.Setup(t => t.GetResponse()).Returns(WebResponse.Object);

            return WebRequest.Object;
        }

        #endregion
    }
}