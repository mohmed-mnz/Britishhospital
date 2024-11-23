namespace ApiContracts.groupusers;

public class GroupUserDto
{
    public int Id { get; set; }
    public int? GroupId { get; set; }
    public int? EmpId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? EmpName { get; set; }
    public string? GroupName { get; set; }

}
