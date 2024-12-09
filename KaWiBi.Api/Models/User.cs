using System.ComponentModel.DataAnnotations.Schema;
using KaWiBi.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace KaWiBi.Api.Models;
public class User : IdentityUser<long>
{
    public List<IdentityRole<long>>? Roles { get; set; }
    [NotMapped]
    public Department Department { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string Post { get; set; } = string.Empty;
    public long DepartmentId { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime{ get; set; }
}