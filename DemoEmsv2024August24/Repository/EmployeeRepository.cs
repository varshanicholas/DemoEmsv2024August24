using DemoEmsv2024August24.Model;
using DemoEmsv2024August24.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace DemoEmsv2024August24.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //EF--VirtualDatabase

        private readonly DemoAugust2024DbContext _context;

        //DI -Constructor injection

        public EmployeeRepository(DemoAugust2024DbContext context)
        {
            _context = context;
        }

        #region -1 Get all employees


        public async Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployees()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.TblEmployees.Include(emp => emp.Department).ToListAsync();
                }
                //return an empty list if context is null
                return new List<TblEmployee>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion


        #region -2 Get all Using ViewModel

        public async Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetViewModelEmployees()
        {
            try
            {
                if (_context != null)
                {
                    /*
                     Select e.EmployeeId,e.EmpName,d.DepartmentName
                      From TblEmployees e
                      Join TblDepartments d
                      ON e.DepartmentId=d.DepartmentId
                     */
                    return await (from e in _context.TblEmployees
                                  from d in _context.TblDepartments
                                  where e.DepartmentId == d.DepartmentId
                                  select new EmpDeptViewModel
                                  {
                                      EmployeeId = e.EmployeeId,
                                      EmployeeName = e.EmployeeName,
                                      Designation = e.Designation,
                                      DepartmentName = d.DepartmentName,
                                      Contact = e.Contact
                                  }).ToListAsync();
                }
                //return an empty list if context is null
                return new List<EmpDeptViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region -3 get employee details based on id

        public async Task<ActionResult<TblEmployee>> GetTblEmployeesById(int id)
        {
            try
            {
                if (_context != null)
                {
                    //find the employee by id 

                    var employee = await _context.TblEmployees.Include(emp => emp.Department).FirstOrDefaultAsync(e => e.EmployeeId == id);
                    return employee;
                }

                return null;

            }

            catch (Exception ex)
            {
                return null;
            }
        }



        #endregion

        #region  4 insert an employee-return employee record

        public async Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee employee)
        {
            try
            {
                //check if employee object is not null

                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee Data is Null");
                }

                //ensure the context is not null

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //add the employee record to the dbcontext

                await _context.TblEmployees.AddAsync(employee);

                //save changes to the database

                await _context.SaveChangesAsync();

                //retrieve the employee with the related departments

                var employeeWithDepartment = await _context.TblEmployees.Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);//eager load


                //return the added employee record

                return employeeWithDepartment;


            }

            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        #region  5 insert an employee-return employee id

        public async Task<ActionResult<int>> PostTblEmployeesReturnId(TblEmployee employee)
        {
            try
            {
                //check if employee object is not null

                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee Data is Null");
                }

                //ensure the context is not null

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //add the employee record to the dbcontext

                await _context.TblEmployees.AddAsync(employee);

                //save changes to the database

                var changesRecord = await _context.SaveChangesAsync();

                //return the addede employee
                if (changesRecord > 0)
                {
                    //return the aaded employee id

                    return employee.EmployeeId;
                }

                else
                {
                    throw new Exception("Failed to save employee record to the database.");
                }



            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region 6 --Update an employee with ID and employee

        public async Task<ActionResult<TblEmployee>> PutTblEmployee(int id, TblEmployee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee Data is Null");
                }

                //ensure the context is not null

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //find the employee by ID
                var existingEmployee = await _context.TblEmployees.FindAsync(id);

                if (existingEmployee == null)
                {
                    return null;
                }

                //Map values with fields -update
                //existing employee data and newdata

                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.DateOfJoining = employee.DateOfJoining;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.Contact = employee.Contact;
                existingEmployee.IsActive = employee.IsActive;


                //save changes to the database

                await _context.SaveChangesAsync();

                //retrieve the employee with the related departments

                var employeeWithDepartment = await _context.TblEmployees.Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);//eager load


                //return the added employee record

                return employeeWithDepartment;

            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion




        #region 7 --delete an employee

        //    public async Task<ActionResult<TblEmployee>> DeleteTblEmployee(int id)
        //    {
        //        try
        //        {
        //            if (id == null)
        //            {
        //                throw new ArgumentNullException(nameof(id), "id  is Null");
        //            }

        //            //ensure the context is not null

        //            if (_context == null)
        //            {
        //                throw new InvalidOperationException("Database context is not initialized");
        //            }

        //            //find the employee by ID
        //            var existingEmployee = await _context.TblEmployees.FindAsync(id);

        //            if (existingEmployee == null)
        //            {
        //                return null;
        //            }
        //            //remove the employee record from the database

        //            _context.TblEmployees.Remove(existingEmployee);

        //            //save changes to the database

        //            await _context.SaveChangesAsync();  




        //        }

        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}



        public JsonResult DeleteTblEmployee(int id)
        {
            try
            {

                //check if employee object is not null
                if (id == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "invalid employee id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }

                //ensure the context is not null

                if (_context == null)
                {

                    return new JsonResult(new
                    {

                        success = false,
                        message = "Database context is not initialized. "

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                //find the employee by ID
                var existingEmployee = _context.TblEmployees.Find(id);

                if (existingEmployee == null)
                {
                    return new JsonResult(new
                    {

                        success = false,
                        message = "Employee Not Found . "

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove the employee record from the database

                _context.TblEmployees.Remove(existingEmployee);

                //save changes to the database

                _context.SaveChangesAsync();

                return new JsonResult(new
                {

                    success = false,
                    message = "Employee Deleted Successfully . "

                })
                {
                    StatusCode = StatusCodes.Status200OK 
                };




            }

            catch (Exception ex)
            {

                return new JsonResult(new
                {

                    success = false,
                    message = "An error accured "

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
            #endregion

            #region -8  Get all departments

            public async  Task<ActionResult<IEnumerable<TblDepartment>>> GetTblDepartments()
            {
            try
            {
                if (_context != null)
                {
                    //find the employee by id 
            return await _context .TblDepartments.ToListAsync ();
                }

                return null;

            }

            catch (Exception ex)
            {
                return null;
            }
        }
            #endregion



        } 
}
