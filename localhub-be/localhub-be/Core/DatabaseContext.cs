using localhub_be.Models.DAOs;
using Microsoft.EntityFrameworkCore;

namespace localhub_be.Core;
public sealed class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Auth> Auths { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Administrator" }, new Role { Id = 2, Name = "User" });
        modelBuilder.Entity<User>().HasData(new User {
            Id = 1, FirstName = "Administrator", LastName = "Administrator", BirthDate = new DateTime(2001, 10, 11),
            Address = "Address bb", Region = "Village Region 1", PhoneNumber = "+387 60 111 2222", MembershipDate = new DateTime(2023, 09, 04)
        });
        modelBuilder.Entity<Auth>().HasData(new Auth { Id = 1, Email = "admin@gmail.com", Password = "$2a$11$llwetYA6KMfLAjuEss9.fOxrLxq9BLJEpcZXVuI4y0.IK4iUnTLNa", RoleId = 1, UserId = 1 });
    }
}

