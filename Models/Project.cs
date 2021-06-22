using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace task_management_backend_dotnet.Models
{

    public class Project
    {
        public int ProjectId{get;set;}

        [Required(ErrorMessage = "a name for the project is required.")]
        public string name {set;get;}
        public DateTime? creationTime{set;get;}

        public IList<Issue> Issues{get;set;} = new List<Issue>();
    }
    
    public class ProjectContext: DbContext
    {
        private string _connectionString;
        public DbSet<Project> Projects {get;set;}
        public ProjectContext(string connectionString = null)
        {
            if(connectionString == null)
            {
                _connectionString = @"Data Source=prod.db;";
            }
            else
            {
                _connectionString = connectionString;
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Project>()
                .HasMany(e => e.Issues)
                .WithOne(e => e.parentProject)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
        
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlite(_connectionString);
         }
    }

}