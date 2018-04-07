using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models; //This is my class
using Newtonsoft.Json;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web;

namespace webapi.Controllers
{
    public class EmployeeController : ApiController
    {
        IList<Employee> employees = new List<Employee>()
        {
            new Employee()
                {
                    EmployeeId = 1, EmployeeName = "Mukesh Kumar", Address = "New Delhi", Department = "IT"
                },
                new Employee()
                {
                    EmployeeId = 2, EmployeeName = "Banky Chamber", Address = "London", Department = "HR"
                },
                new Employee()
                {
                    EmployeeId = 3, EmployeeName = "Rahul Rathor", Address = "Laxmi Nagar", Department = "IT"
                },
                new Employee()
                {
                    EmployeeId = 4, EmployeeName = "YaduVeer Singh", Address = "Goa", Department = "Sales"
                },
                new Employee()
                {
                    EmployeeId = 5, EmployeeName = "Manish Sharma", Address = "New Delhi", Department = "HR"
                },
        };
      //  public IList<Employee> GetAllEmployees()
        public string HeadAllEmployees()

        {
            //Return list of all employees  

            var empsInJson = JsonConvert.SerializeObject(employees);
            return empsInJson;

        }

        public Employee GetEmployeeDetails(int id)
        {
            //Return a single employee detail  

            if (CheckAuthorization())
            {
                var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return employee;
            }
            else
                return null;
        }

        public bool CheckAuthorization()
        {
            bool status = false;
            object sid;
            try
            {
                string authorizationHeader = HttpContext.Current.Request.Headers["Authorization"];
                byte[] bArray = Convert.FromBase64String(authorizationHeader);
                MemoryStream ms = new MemoryStream(bArray);
                BinaryFormatter fmt = new BinaryFormatter();
                sid = fmt.Deserialize(ms);
                if (validateAuthorization(sid))
                    status = true;

            }
            catch {
             //   throw new Exception("CyberArk - Authorization header was not set");
            }

            return status;
        }

        public bool validateAuthorization(object sid)
        {
            // Placeholder for authorization verification. currently will always be true
            return true;
        }

    }
}
