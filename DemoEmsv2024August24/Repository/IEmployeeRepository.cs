using DemoEmsv2024August24.Model;
using DemoEmsv2024August24.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoEmsv2024August24.Repository
{
    public interface IEmployeeRepository
    {
        #region 1-Get all employees
        public Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployees();

        #endregion

        #region 2- Get All employees using ViewModel

        public Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetViewModelEmployees();


        #endregion

        #region -3 Get an employee based on id

        //Get an Employee based on Id
        public Task<ActionResult<TblEmployee>> GetTblEmployeesById(int id);
        #endregion

        //4--insert an employee-return employee record
        public Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee TbleEmployee);

        //5--insert an employee-return employee id
        public Task<ActionResult<int>> PostTblEmployeesReturnId(TblEmployee TblEmployee);


        //6--update an employee with id and employee
        public Task<ActionResult<TblEmployee>> PutTblEmployee(int id, TblEmployee tblEmployee);


        ////7--delete an employee
        //public Task<ActionResult<TblEmployee>> DeleteTblEmployee(int id);

        public JsonResult DeleteTblEmployee(int id);


        //8--get all departments
        public Task<ActionResult<IEnumerable<TblDepartment>>> GetTblDepartments();



    }
}
