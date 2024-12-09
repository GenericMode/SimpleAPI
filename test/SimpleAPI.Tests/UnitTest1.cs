using SimpleAPI.Controllers;
using Moq;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;
using System.IO;
using System.Text.Json;



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
            // Create a mock for IFileSystem
            var mockFileSystem = new Mock<IFileSystem>();

            // Setup the mock to simulate reading a file
            mockFileSystem.Setup(fs => fs.File.ReadAllText(It.IsAny<string>())).Returns((string path) =>
            {
                if (path == "/mock/path/songs.json")
                {
                    return "{\"Title\": \"Picture to Burn\"}";
                }
                throw new FileNotFoundException($"File not found: {path}");
            });

            // Mock IWebHostEnvironment
            var mockEnv = new Mock<IHostEnvironment>();
            mockEnv.Setup(env => env.ContentRootPath).Returns("/mock/path");

            // Mock ILogger<Song>
            var mockLogger = new Mock<ILogger<Song>>();

            // Create the Song instance with the mock file system
            var song = new Song(mockEnv.Object, mockLogger.Object);

            // Act with file

            // Set up the real Host Environment
            var host = Host.CreateDefaultBuilder()
                       .ConfigureLogging(logging =>
                       {
                           logging.ClearProviders();
                           logging.AddConsole();  // Use console for logging (or any logger you want)
                       })
                        .UseEnvironment("Development")  // Force environment to "Development"
                        .Build();

            // Get IHostEnvironment and ILogger<Song> from the Host
                   // Arrange: Create a custom IHostEnvironment
            var env = host.Services.GetRequiredService<IHostEnvironment>();
            var logger = host.Services.GetRequiredService<ILogger<Song>>();


            var Actsong = new Song(env, logger);

            ValuesController controller = new ValuesController(Actsong);

            //var Path = "songs.json";
            //var stringContent = System.IO.File.ReadAllText(Path);
            //var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            //var Newsong = jsonResult?.ElementAtOrDefault(2);

            var returnValue = controller.Get(2);

            var okResult = returnValue as OkObjectResult;  // Cast to OkObjectResult
            Assert.NotNull(okResult);  // Ensure the result is OkObjectResult

            // Use reflection to get the Title property from the anonymous object
            var value = okResult.Value;
            var titleProperty = value.GetType().GetProperty("Title");
            Assert.NotNull(titleProperty);  // Ensure that the Title property exists

            var songTitle = titleProperty.GetValue(value).ToString();  // Get the value of the Title property
            Assert.Equal("Teardrops On My Guitar", songTitle);  // Check if the title is correct
            }
        }
    }