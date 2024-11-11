using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusProject.Shared.Entities;

namespace NexusProject.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Young> Youngs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<ActivityColection> ActivityColections { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public DbSet<foundation> foundations { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageCollection> MessageCollections { get; set; }
     
     


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Document).IsUnique();
            modelBuilder.Entity<Tutor>() .HasOne(t => t.Users).WithMany().HasForeignKey(t => t.UserDocument).HasPrincipalKey(u => u.Document);
            modelBuilder.Entity<Young>().HasOne(t => t.Users).WithMany().HasForeignKey(t => t.UserDocument).HasPrincipalKey(u => u.Document);
            modelBuilder.Entity<Admin>().HasOne(t => t.Users).WithMany().HasForeignKey(t => t.UserDocument).HasPrincipalKey(u => u.Document);
            modelBuilder.Entity<ActivityColection>().HasOne(t => t.Activities).WithMany().HasForeignKey(t => t.ActivitiesId).HasPrincipalKey(u => u.Id);
            modelBuilder.Entity<ActivityColection>().HasOne(t => t.Tutors).WithMany().HasForeignKey(t => t.TutorsId).HasPrincipalKey(u => u.Id);
            modelBuilder.Entity<ActivityColection>().HasOne(t => t.Youngs).WithMany().HasForeignKey(t => t.Youngid).HasPrincipalKey(u => u.Id);



        }
    }
    }
