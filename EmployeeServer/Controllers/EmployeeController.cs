using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeServer.Context;
using EmployeeServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using Newtonsoft.Json.Linq;
using System.Security.Policy;

namespace EmployeeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly EmployeeContext EmpDetails;
        //Hosted web API REST Service base url
        string Baseurl = "https://getinvoices.azurewebsites.net/";
        public EmployeeController(EmployeeContext employeeContext)
        {
            EmpDetails = employeeContext;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAsync()
        {
            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Customers");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
                //returning the employee list to view
                return EmpInfo;
            }

        } 
         
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] Employee obj)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)obj);
            var response = await client.PostAsync(Baseurl+ "api/Customer", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee obj)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)obj);
                client.BaseAddress = new Uri(Baseurl);
                var response = client.PutAsync("api/Customer"+id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
            return Ok();

        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var response = client.DeleteAsync("api/Customer/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }          
            return Ok();

        }
    }
}
 