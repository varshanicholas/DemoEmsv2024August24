using DemoEmsv2024August24.Model;
using DemoEmsv2024August24.Repository;
using DemoEmsv2024August24.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoEmsv2024August24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //call repository

        private readonly IEmployeeRepository _repository;


        //Dependency injuction DI ---Constructor Instrucor

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

/*
        // GET: api/Employees
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "varsha", "ahalya","tharak" };
        }
*/
        //// GET api/Employees/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value "+id;
        //}

        #region 1- get all employees-search all
        [HttpGet]
        public async Task<ActionResult<IEnumerable <TblEmployee>>> GetAllEmployees()
        {
            var employees=await _repository .GetTblEmployees ();
            if(employees == null)
            {
                return NotFound ("No Employees found");
            }

            return Ok (employees);
        }

        #endregion

        #region 2- get all employees-search all
        [HttpGet ("vm")]
        public async Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetAllEmployeesByViewModel()
        {
            var employees = await _repository.GetViewModelEmployees ();
            if (employees == null)
            {
                return NotFound("No Employees found");
            }

            return Ok(employees);
        }

        #endregion

        #region 3- get all employees-search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEmployee>> GetEmployeesById(int id)
        {
            var employees = await _repository.GetTblEmployeesById(id);
            if (employees == null)
            {
                return NotFound("No Employees found");
            }

            return Ok(employees);
        }

        #endregion


        #region -4 insert an employee-return employee record

        [HttpPost ]
        public async Task<ActionResult<TblEmployee>> InsertTblEmployeesReturnRecord(TblEmployee employee)
        {
            if(ModelState.IsValid)
            {
                //insert a new record and return as an object named employee

                var newEmployee = await _repository.PostTblEmployeesReturnRecord(employee);

                if(newEmployee!=null)
                {
                    return Ok (newEmployee);
                }
                else
                {
                    return NotFound();
                }
               
            }
            return BadRequest();
        }


        #endregion




        #region -5 insert an employee-return employee id

        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertTblEmployeesReturnId(TblEmployee employee)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee

                var newemployeeId = await _repository.PostTblEmployeesReturnId (employee);

                if (newemployeeId != null)
                {
                    return Ok(newemployeeId);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }


        #endregion

        #region 6 Update an employee

        [HttpPut ("{id}")]
        public async Task<ActionResult<TblEmployee>> UpdateEmployeeReturnRecord(int id, TblEmployee employee)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee

                var updateEmployee = await _repository.PutTblEmployee (id,employee);

                if (updateEmployee != null)
                {
                    return Ok(updateEmployee);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }


        #endregion


        #region 7 delete an employee
        [HttpDelete ("{id}")]
        public IActionResult  DeleteTblEmployee(int id)
        {
            try
            {
                var result = _repository.DeleteTblEmployee(id);
                if(result != null)
                {
                    return NotFound(new { success = false, message = "Employee could not be deleted or not not found" });
                }

                return Ok(result );
            }
            catch (Exception ex)
            {

                //log exception in real world scenario

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexcepted error ocuured" });
            }
        }

        #endregion

        #region 8 get all department

        [HttpGet ("v2")]
        public async Task<ActionResult<IEnumerable<TblDepartment>>> GetAllDepartments()
        {
            var departments=await _repository .GetTblDepartments ();

            if(departments == null)
            {
                return NotFound("No department Found");
            }

            return Ok(departments);
        }


        #endregion
    }
}
