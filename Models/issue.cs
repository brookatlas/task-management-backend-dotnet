using task_management_backend_dotnet;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace task_management_backend_dotnet.Models
{
    public class Issue
    {
        public int id{get;}

        [ForeignKey("Project")]
        public Project parentProject {set;get;}
        public string name {set;get;}

        public string content {set;get;}
    }


    public class IssueContext: DbContext
    {
        private string _connectionString;
        public DbSet<Issue> Issues {get;set;}
        public IssueContext(string connectionString = null)
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


 
}