using LIMS.Data.Configuration;
using LIMS.Data.Entities;
using LIMS.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LIMS.Data.DataContext
{
    public partial class DataDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DataDbContext()
        {
        }

        public DataDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //Configure using Fluent API 
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new BeenLoveConfiguration());
            modelBuilder.ApplyConfiguration(new EventCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new LabelConfiguration());
            modelBuilder.ApplyConfiguration(new LoanDebtConfiguration());
            modelBuilder.ApplyConfiguration(new LoanDebtCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostCommentConfiguration());
            modelBuilder.ApplyConfiguration(new PostCommentImageConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostImageConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TaskLabelConfiguration());
            modelBuilder.ApplyConfiguration(new PostPlaceConfiguration());



            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //Data seeding
            modelBuilder.Seed();
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BeenLove> BeenLoves { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<LoanDebt> LoanDebts { get; set; }
        public DbSet<LoanDebtCategory> LoanDebtCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostCommentImage> PostCommentImages { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<LIMS.Data.Entities.Task> Tasks { get; set; }
        public DbSet<TaskLabel> TaskLabels { get; set; }
        public DbSet<PostPlace> PostPlaces { get; set; }

    }
}
