﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
namespace Models.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? ServiceName { get; set; }

    public string? Prefix { get; set; }

    public virtual ICollection<CounterServices> CounterServices { get; set; } = new List<CounterServices>();
}