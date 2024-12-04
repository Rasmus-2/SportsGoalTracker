using ChatGPT.Net;
using Moq;
using OpenAIApi.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIAPITests.Mock
{
    public static class MockChatGptNetService
    {
        public static IChatGpt Create()
        {
            var mock = new Mock<IChatGpt>();
            mock.Setup(service => service.Ask(It.IsAny<string>()))
                .ReturnsAsync("Keep practicing your shooting technique.");
            return mock.Object;
        }
    }
}
