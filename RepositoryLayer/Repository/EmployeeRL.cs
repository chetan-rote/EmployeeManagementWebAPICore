﻿using CommonLayer.RequestModel;
using RepositoryLayer.ContextModel;
using RepositoryLayer.EmployeeDbContext;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class EmployeeRL : IEmployeeRL
    {
        private readonly EmployeeContext context;

        public EmployeeRL(EmployeeContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Adds new the employee to database.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public bool RegisterEmployee(RegisterModel employee)
        {
            try
            {
                CompanyEmployee employeeObject = new CompanyEmployee()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Password = employee.Password,
                    PhoneNumber = employee.PhoneNumber,
                    Role = "Employee",
                    CreatedDateTime = DateTime.UtcNow,
                    ModifiedDateTime = null
                };

                this.context.Employees.Add(employeeObject);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Gets all employee.
        /// </summary>
        /// <returns></returns>
        public List<CompanyEmployee> GetAllEmployee()
        {
            try
            {
                List<CompanyEmployee> list = (from e in this.context.Employees
                                              select new CompanyEmployee
                                              {
                                                  EmployeeId = e.EmployeeId,
                                                  FirstName = e.FirstName,
                                                  LastName = e.LastName,
                                                  PhoneNumber = e.PhoneNumber,
                                                  Email = e.Email,
                                                  Role = e.Role,
                                                  CreatedDateTime = e.CreatedDateTime,
                                                  ModifiedDateTime = e.ModifiedDateTime
                                              }).ToList<CompanyEmployee>();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Edits the employee.
        /// </summary>
        /// <param name="updatedEmployee">The updated employee.</param>
        /// <param name="EmpId">The emp identifier.</param>
        /// <returns></returns>
        public bool EditEmployee(UpdateModel updatedEmployee, int EmpId)
        {
            try
            {
                CompanyEmployee employee = this.context.Employees.Where(x => x.EmployeeId == EmpId).FirstOrDefault();
                if (employee != null)
                {
                    employee.EmployeeId = EmpId;
                    employee.FirstName = updatedEmployee.FirstName;
                    employee.LastName = updatedEmployee.LastName;
                    employee.PhoneNumber = updatedEmployee.PhoneNumber;
                    employee.Password = updatedEmployee.Password;
                    employee.ModifiedDateTime = DateTime.Now;
                    int result = this.context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Deletes the specified emp identifier.
        /// </summary>
        /// <param name="EmpId">The emp identifier.</param>
        /// <returns></returns>
        public bool Delete(int EmpId)
        {
            try
            {
                CompanyEmployee employee = this.context.Employees.Where(x => x.EmployeeId == EmpId).FirstOrDefault();
                if (employee != null)
                {
                    this.context.Employees.Remove(employee);
                }
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
