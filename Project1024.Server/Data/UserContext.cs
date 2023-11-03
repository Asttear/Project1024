using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project1024.Server.Models;
using System;

namespace Project1024.Server.Data;

public class UserContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public UserContext(DbContextOptions<UserContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(b => { b.ToTable("user"); });
        builder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("user_claim"); });
        builder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("user_login"); });
        builder.Entity<IdentityUserToken<int>>(b => { b.ToTable("user_token"); });
        builder.Entity<IdentityRole<int>>(b => { b.ToTable("role"); });
        builder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("role_claim"); });
        builder.Entity<IdentityUserRole<int>>(b => { b.ToTable("user_role"); });
    }
}
