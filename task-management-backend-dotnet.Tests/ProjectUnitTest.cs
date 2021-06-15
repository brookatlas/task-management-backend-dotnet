using Xunit;
using task_management_backend_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using task_management_backend_dotnet.Services;

namespace task_management_backend_dotnet.Tests
{

    public class ProjectUnitTest
    {
        private ProjectService _projectService;
        private DbContextOptions<ProjectContext> _contextOptions;
        private ProjectContext _projectContext;
        public ProjectUnitTest()
        {
            _contextOptions = new DbContextOptionsBuilder<ProjectContext>()
            .UseSqlite(@"Data Source=Test.db;").Options;
            _projectContext = new ProjectContext(@"Data Source=Test.db;");
            _projectService = new ProjectService(_projectContext);
            _projectContext.Database.EnsureDeleted();
            _projectContext.Database.EnsureCreated();
        }

        [Fact]
        public void TestProjectCreate()
        {
            var project = new Project();
            project.name = "myProject";
            project.ProjectId = 3;
            _projectService.Create(project);
            project = null;
            project = _projectService.Get(3);
            Assert.Equal("myProject", project.name);
        }
    }
}
