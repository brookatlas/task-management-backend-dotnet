using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using task-management-backend-dotnet.Models;

namespace task_management_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ProjectContext _projectContext;

        public ProjectController()
        {
            _projectContext = new ProjectContext();
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Project>> Get()
        {
            try
            {
                return _projectContext.Projects.ToList<Project>();   
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetById(int id)
        {
            try
            {
                var MatchingProject =_projectContext.Projects.Single(
                    p => p.ProjectId == id
                );
                return MatchingProject;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("")]
        public ActionResult<Project> Create(Project model)
        {
            try
            {
                _projectContext.Projects.Add(model);
                _projectContext.SaveChanges();
                return model;   
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPatch("{id}")]
        public ActionResult<Project> Update(int id, Project model)
        {
            try
            {
                var MatchingProject = _projectContext.Projects.Single(
                    p => p.ProjectId == id
                );
                MatchingProject.creationTime = model.creationTime;
                MatchingProject.name = model.name;
                _projectContext.SaveChanges();
                return MatchingProject;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Project> Delete(int id)
        {
            try
            {
                var MatchingProject = _projectContext.Projects.Single(
                    p => p.ProjectId == id
                );
                _projectContext.Projects.Remove(MatchingProject);
                _projectContext.SaveChanges(); 
                return MatchingProject;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}