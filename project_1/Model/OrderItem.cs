using System;
using System.Collections.Generic;

namespace DemoEmsv2024August24.Model;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitePrice { get; set; }

    public int? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
