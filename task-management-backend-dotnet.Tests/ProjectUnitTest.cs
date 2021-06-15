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


        [Fact]
        public void TestProjectUpdate()
        {
            var project = new Project();
            project.name = "myProject";
            project.ProjectId = 3;
            _projectService.Create(project);
            project = null;
            project = _projectService.Get(3);
            Assert.Equal("myProject", project.name);
            project.name = "myProjectUpdated";
            _projectService.Update(project);
            project = null;
            project = _projectService.Get("myProjectUpdated");
            Assert.Equal("myProjectUpdated", project.name);
        }

        [Fact]
        public void TestProjectDelete()
        {
            var project = new Project();
            project.name = "myProject";
            project.ProjectId = 3;
            _projectService.Create(project);
            project = null;
            project = _projectService.Get(3);
            Assert.Equal("myProject", project.name);
            project = null;
            var deleted = _projectService.Delete(3);
            Assert.True(deleted == true);
            project = null;
            try
            {
                project = _projectService.Get("myProject");
                Assert.Equal(1, 0);
            }
            catch(ProjectNotFound)
            {
            }
        }
        
    }
}
