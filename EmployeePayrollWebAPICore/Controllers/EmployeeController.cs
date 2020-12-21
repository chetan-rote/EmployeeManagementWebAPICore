using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IServices;
using BusinessLayer.MSMQServices;
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
        MSMQ mSMQ = new MSMQ();
        private readonly IEmployeeBL employeeBL;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeBL">The employee bl.</param>
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        /// <summary>
        /// Adds new the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RegisterEmployee(RegisterModel employee)
        {
            try
            {
                if (this.employeeBL.RegisterEmployee(employee))
                {
                    this.mSMQ.AddToQueue(employee.Email + " Employee has been registered successfully.");
                    return this.Ok(new { success = true, Message = "Employee record added successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "Employee record is not added " });
                }
            }
            catch (Exception e)
            {
                var sqlException = e.InnerException as SqlException;

                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { success = false, ErrorMessage = "Cannot insert duplicate Email values." });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = e.Message });
                }

            }
        }
        /// <summary>
        /// Gets all employee.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Edits the employee.
        /// </summary>
        /// <param name="EmpId">The emp identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        [HttpPut("{EmpId}")]
        public IActionResult EditEmployee(int EmpId, UpdateModel employee)
        {
            try
            {
                if (this.employeeBL.EditEmployee(employee, EmpId))
                {
                    return this.Ok(new { success = true, Message = "Employee record updated successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { success = false, Message = "Employee record is not updated " });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
        /// <summary>
        /// Deletes the specified emp identifier.
        /// </summary>
        /// <param name="EmpId">The emp identifier.</param>
        /// <returns></returns>
        [HttpDelete("{EmpId}")]
        public IActionResult Delete(int EmpId)
        {
            try
            {
                if (this.employeeBL.Delete(EmpId))
                {
                    return this.Ok(new { success = true, Message = "Employee record Deleted successfully" });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Employee record Deleted unsuccessfully" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
    }
}
