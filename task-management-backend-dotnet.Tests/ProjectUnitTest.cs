using System;
using Xunit;
using task_management_backend_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using task_management_backend_dotnet.Services;

namespace task_management_backend_dotnet.Tests
{


    public class ProjectControllerTest
    {

        private DbContextOptions<ProjectContext> _contextOptions;

        protected ProjectControllerTest (DbContextOptions<ProjectContext> contextOptions) 
        {
            _contextOptions = contextOptions;
            InitDatabase();
        }

        private void InitDatabase()
        {
            using(var context = new ProjectContext(
                @"Data Source=Test.db;"
            ))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }

    public class ProjectUnitTest : ProjectControllerTest
    {

        public ProjectUnitTest() : base(
            new DbContextOptionsBuilder<ProjectContext>()
            .UseSqlite("Filename=Test.db")
            .Options
        )
        {

        }

        [Fact]
        public void TestProjectCreate()
        {

        }
    }
}
