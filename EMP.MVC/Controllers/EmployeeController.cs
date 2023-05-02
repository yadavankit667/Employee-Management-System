using EMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EMP.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //https://localhost:7158/api/Employee/EmployeeList
            List<Employee> employees = new();
            var client = new RestClient("https://localhost:7158/api/Employee/EmployeeList");
            var request = new RestRequest() { Method = Method.Get };
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content!);
                if (result != null && result.IsSuccess)
                {
                    JObject o = JObject.Parse(response.Content!);
                    JArray a = (JArray)o["result"]!;
                    employees = a.ToObject<List<Employee>>()!;
                    return View(employees);
                }
                else
                {
                    return View(employees);
                }
            }
            return View(new List<Employee>());
        }

        [HttpGet]
        public IActionResult SaveEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveEmployee(Employee employee)
        {
            var client = new RestClient("https://localhost:7158/api/Employee/SaveEmployee");
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json");

            var body = JsonConvert.SerializeObject(employee, Formatting.None);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content);
                if (result != null && result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result.Message);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            Employee employee = new();
            var client = new RestClient($"https://localhost:7158/api/Employee/GetEmployeeById?id={id}");
            var request = new RestRequest() { Method = Method.Get };
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content);
                if (result != null && result.IsSuccess)
                {
                    employee = JsonConvert.DeserializeObject<Employee>(result.Result!.ToString());
                    return View(employee);
                }
                else
                {
                    return View(result.Message);
                }
            }
            return View(employee);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee, int id)
        {
            var client = new RestClient($"https://localhost:7158/api/Employee/UpdateEmployee?id={id}");
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json");

            var body = JsonConvert.SerializeObject(employee, Formatting.None);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content!);
                if (result != null && result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result!.Message);
                }
            }
            return View();
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var client = new RestClient($"https://localhost:7158/api/Employee/DeleteEmployee?id={id}");
            var request = new RestRequest() { Method = Method.Delete };
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content!);
                if (result != null && result.IsSuccess)
                {
                    return View();
                }
                else
                {
                    return View(result.Message);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Employee employee = new();
            var client = new RestClient($"https://localhost:7158/api/Employee/GetEmployeeById?id={id}");
            var request = new RestRequest() { Method = Method.Get };
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CommonResponse>(response.Content);
                if (result != null && result.IsSuccess)
                {
                    employee = JsonConvert.DeserializeObject<Employee>(result.Result!.ToString());
                    return View(employee);
                }
                else
                {
                    return View(result.Message);
                }
            }
            return View(employee);
        }
    }
}
