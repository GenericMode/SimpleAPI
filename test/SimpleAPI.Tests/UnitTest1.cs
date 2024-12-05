using Moq;
using System;
using Xunit;
using SimpleAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;


namespace SimpleAPI.Tests
    {
        public class UnitTest1
        {
            [Fact]
            public void Test1()
            {
            }
            [Fact]
            public void GetReturnsCorrectNumber()
            {

                 // Create a mock for IHostEnvironment
                var mockEnv = new Mock<IHostEnvironment>();

                // Set up mock behavior
                // Set up mock behavior for EnvironmentName
                mockEnv.Setup(env => env.EnvironmentName).Returns("Development");  // Simulating the Development environment

                var song = new Song(mockEnv.Object);
                var controller = new ValuesController(song);
                var expectedTitle = "Picture to Burn";

                ActionResult returnValue = controller.Get(1);
                Assert.NotNull(returnValue);  // Ensure that the result is not null
                Assert.IsType<OkObjectResult>(returnValue);  // Ensure that the result is OkObjectResult

                // Extract the anonymous object from the OkObjectResult
                var result = (returnValue as OkObjectResult)?.Value;

                // Since the result is an anonymous object, check for the Title property
                Assert.NotNull(result);  // Ensure the result is not null
                var title = result.GetType().GetProperty("Title")?.GetValue(result, null);  // Access Title property dynamically

                Assert.Equal(expectedTitle, title);
            }
        }
    }