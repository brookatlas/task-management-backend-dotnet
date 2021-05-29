using System;
using Microsoft.EntityFrameworkCore;

namespace task_management_backend_dotnet
{

    public class ProjectContext: DbContext
    {
        public DbSet<Project> Projects {get;set;}


        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite(@"Data Source=c:\\taskManagementDb.db;");
    }

    public class Project
    {
        public int ProjectId{get;set;}
        public string name {set;get;}
        public DateTime? creationTime{set;get;}
    }
}