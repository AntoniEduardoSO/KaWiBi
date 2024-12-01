using System.Reflection;
using KaWiBi.Api.Models;
using KaWiBi.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KaWiBi.Api.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<User,
        IdentityRole<long>, 
        long, 
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>>(options) 
{
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}