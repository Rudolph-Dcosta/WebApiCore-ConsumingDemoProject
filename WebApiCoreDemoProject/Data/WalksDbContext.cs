using Microsoft.EntityFrameworkCore;
using WebApiCoreDemoProject.Models.Domain;

namespace WebApiCoreDemoProject.Data
{
    public class WalksDbContext:DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>().HasData(new Region { Id = 1, Code = "NRTHL", AreaName = "Northland Region", Area = 13789, Latitude = -35.3708304, Longitude = 172.5717825, Population = 194600, });

            modelBuilder.Entity<Region>().HasData(new Region { Id = 2, Code = "AUCK", AreaName = "Auckland Region", Area = 4894, Latitude = -36.5253207, Longitude = 173.7785704, Population = 1718982 });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

       
    }
}
