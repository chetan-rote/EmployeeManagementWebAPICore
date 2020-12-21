using BusinessLayer.IServices;
using CommonLayer.RequestModel;
using RepositoryLayer.ContextModel;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class EmployeeBL : IEmployeeBL
    {
        private IEmployeeRL employeeRL;

        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }        

        public List<CompanyEmployee> GetAllEmployee()
        {
            try
            {
                return this.employeeRL.GetAllEmployee();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
