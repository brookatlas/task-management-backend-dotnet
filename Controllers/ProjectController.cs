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
                var result = _projectContext.Projects.ToList<Project>();
                return Ok(result);
            }
            catch (System.Exception)
            {
                var error = new Dictionary<string,string>() {
                    {"error", "could not retrieve the projects data properly."}
                };
                return StatusCode(500, error);
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
                return Ok(MatchingProject);
            }
            catch (System.InvalidOperationException)
            {
                var error = new Dictionary<string, string>(){
                    {"error", $"project with id {id} was not found"}
                };
                return NotFound(error);
            }
        }

        [HttpPost("")]
        public ActionResult<Project> Create(Project model)
        {
            Project MatchingProject = null;
            try
            {
                MatchingProject =_projectContext.Projects.Single(
                    p => p.name == model.name
                );
                var error = new Dictionary<string,string>(){
                    {"error", $"project named {model.name} already exists."}
                };
                return Conflict(error);
            }
            catch (System.InvalidOperationException)
            {
                _projectContext.Projects.Add(model);
                _projectContext.SaveChanges();
                return Ok(model);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult<Project> Update(int id, Project model)
        {
            Project MatchingProject = null;
            try
            {
                MatchingProject = _projectContext.Projects.Single(
                    p => p.ProjectId == id
                );
            }
            catch (System.InvalidOperationException)
            {
                var error = new Dictionary<string, string>(){
                    {"error", $"project with id {id} was not found"}
                };
                return NotFound(error);
            }
            try
            {
                MatchingProject.creationTime = model.creationTime;
                MatchingProject.name = model.name;
                _projectContext.SaveChanges();
                return Ok(MatchingProject);   
            }
            catch (System.Exception)
            {
                var error = new Dictionary<string, string>(){
                    {"error", $"could not update the project properly"}
                };
                return StatusCode(500, error);
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
            catch (System.InvalidOperationException)
            {
                var error = new Dictionary<string, string>(){
                    {"error", $"project with id {id} was not found"}
                };
                return NotFound(error);
            }
        }
    }
}