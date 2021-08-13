
using CookieBooking.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class DbContextService: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MailConfiguration> MailConfigurations { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbContextService(DbContextOptions<DbContextService> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables  
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<MailConfiguration>().ToTable("mailconfigurations");
            modelBuilder.Entity<EmailTemplate>().ToTable("email_templates");
            modelBuilder.Entity<Image>().ToTable("images");
            modelBuilder.Entity<Message>().ToTable("messages");
        }
    }
}
