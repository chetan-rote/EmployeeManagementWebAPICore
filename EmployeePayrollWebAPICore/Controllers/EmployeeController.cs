using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.ContextModel;

namespace EmployeePayrollWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBL employeeBL;

        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }               

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                List<CompanyEmployee> empList = this.employeeBL.GetAllEmployee();
                if (empList != null)
                {
                    return this.Ok(new { success = true, Message = "get Employee records successfully", data = empList });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "get Employee records unsuccessfully" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
    }
}
