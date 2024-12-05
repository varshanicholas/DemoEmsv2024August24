using System;
using System.Collections.Generic;

namespace DemoEmsv2024August24.Model;

public partial class TblEmployee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string? Designation { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public int DepartmentId { get; set; }

    public string? Contact { get; set; }

    public bool? IsActive { get; set; }

    public virtual TblDepartment? Department { get; set; } = null!;
}
