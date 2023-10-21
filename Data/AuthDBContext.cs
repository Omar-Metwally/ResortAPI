using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models.DominModels.Auth;

namespace WebApplication5.Data;

public class AuthDBContext : IdentityDbContext<ApplicationUser>
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
    {
    }
}
