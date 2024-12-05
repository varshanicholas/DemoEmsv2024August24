using System;
using System.Collections.Generic;

namespace DemoEmsv2024August24.Model;

public partial class Item
{
    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
