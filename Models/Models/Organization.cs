﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
namespace Models.Models;

public partial class Organization
{
    public int Orgid { get; set; }

    public string OrgName { get; set; } = null!;

    public virtual ICollection<Counters> Counters { get; set; } = new List<Counters>();

    public virtual ICollection<BookingSettingOrg> BookingSettingOrg { get; set; } = new List<BookingSettingOrg>();

    public virtual ICollection<Display> Display { get; set; } = new List<Display>();

    public virtual ICollection<Employee> Employee { get; set; } = new List<Employee>();

    public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
}