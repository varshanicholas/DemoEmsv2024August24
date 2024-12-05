using System;
using System.Collections.Generic;

namespace DemoEmsv2024August24.Model;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerNumber { get; set; }

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
