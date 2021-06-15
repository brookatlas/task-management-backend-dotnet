using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace task_management_backend_dotnet.Models
{

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

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlite(_connectionString);
         }
    }

    public class Project
    {
        public int ProjectId{get;set;}

        [Required(ErrorMessage = "a name for the project is required.")]
        public string name {set;get;}
        public DateTime? creationTime{set;get;}
    }
}