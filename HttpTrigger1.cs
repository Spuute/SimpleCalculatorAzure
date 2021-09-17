using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class GitHubMonitorApp
    {
        [FunctionName("GitHubMonitorApp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get","post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Out GitHub Monitor pricessed an action.");

            string first = req.Query["first"];
            string second = req.Query["second"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
          
         
            string responseMessage = string.Empty;
            try
            {
                int firstNumber = int.Parse(first);
                int secondNumber = int.Parse(second);
                int calcResult = firstNumber + secondNumber;

                responseMessage = $"{first} + {second} equals {calcResult}.";
            }
            catch
            {
                responseMessage = $"You did not enter valid integers";
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
