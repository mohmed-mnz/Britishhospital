﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
namespace Models.Models;

public partial class Counters
{
    public int CounterId { get; set; }

    public string? CounterName { get; set; }

    public bool? IsActive { get; set; }

    public int? empid {  get; set; }

    public int? Orgid { get; set; }



    public virtual Employee? Emp { get; set; }

    public virtual Organization? Org { get; set; }
    public virtual ICollection<CounterServices> CounterServices { get; set; } = new List<CounterServices>();

    public virtual ICollection<DisplayCounters> DisplayCounters { get; set; } = new List<DisplayCounters>();
}