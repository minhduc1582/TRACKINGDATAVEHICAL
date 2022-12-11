using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop_api.Entities;
using eshop_pbl6.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace eshop_api.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoleInPermission>().HasKey(UserInRole => new {
            UserInRole.RoleId, UserInRole.PermissionId
            });
        }
        public DbSet<User> AppUsers {get; set;}
        public DbSet<Role> Roles {get;set;}
        public DbSet<RoleInPermission> RoleInPermissions{get;set;}
        public DbSet<Permission> Permissions{get;set;}
        public DbSet<Position> Positions{get; set;}
        public DbSet<Image> Images{get; set;}
    }
}