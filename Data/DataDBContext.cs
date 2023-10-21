using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using WebApplication5.Models.DominModels;

namespace WebApplication5.Data;

public class DataDBContext : DbContext
{
    public DataDBContext(DbContextOptions<DataDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=DataDB;Integrated Security=True");
   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       /* modelBuilder.Entity<Bill>()
            .HasOne(e => e.Expense)
            .WithMany(e => e.Bills)
            .HasForeignKey(e => e.ExpenseId )
            .HasPrincipalKey(e => new { e.Bills, e.Id });*/
        
        modelBuilder.Entity<Bill>(
            b =>
            {
                b.HasKey(c => new { c.Id, c.ApartmentId });
            });
    }


    public DbSet<Apartment> Apartments { get; set; }

    public DbSet<Bill> Bills { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<Lease> Leases { get; set; }

    public DbSet<Owner> Owners { get; set; }

    public DbSet<News> News { get; set; }

    public DbSet<NewsSection> NewsSections { get; set; }

}
