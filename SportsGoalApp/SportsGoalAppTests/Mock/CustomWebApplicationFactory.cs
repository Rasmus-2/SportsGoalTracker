using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAIApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ChatGPT.Net;
using Microsoft.Extensions.DependencyInjection;
using OpenAIApi.Wrappers;
using Microsoft.Extensions.Configuration;

namespace SportsGoalAppTests.Mock
{
    public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing registrations
                services.RemoveAll(typeof(IChatGpt));

                services.AddSingleton(MockChatGptNetService.Create());
            });

        }
    }
}
