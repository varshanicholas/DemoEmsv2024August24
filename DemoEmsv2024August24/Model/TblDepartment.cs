using System;
using System.Collections.Generic;

namespace DemoEmsv2024August24.Model;

public partial class TblDepartment
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    [System .Text .Json .Serialization .JsonIgnore]
    public virtual ICollection<TblEmployee> TblEmployees { get; set; } = new List<TblEmployee>();
}
