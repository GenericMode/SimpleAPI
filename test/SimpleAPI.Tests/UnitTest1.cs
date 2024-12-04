using System;
using Xunit;
using SimpleAPI.Controllers;
using Microsoft.AspNetCore.Mvc;


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
                var controller = new ValuesController();
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