using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using task_management_backend_dotnet.Services;
using task_management_backend_dotnet.Models;

namespace task_management_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Project>> Get()
        {
            try
            {
                var result = _projectService.Get();
                return Ok(result);   
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Dictionary<string, string>(){
                    {"error", "could not retrieve the projects correctly."}
                });
            }
        }


        [HttpPost("")]
        public ActionResult<Project> Create(Project project)
        {
            try
            {
                var ProjectCreated = _projectService.Create(project);
                return Ok(ProjectCreated);   
            }
            catch (ProjectAlreadyExists)
            {
                return StatusCode(409, new Dictionary<string, string>() {
                    {"error", "project already exists."}
                });
            }
        }


        [HttpDelete("{name}")]
        public ActionResult<Project> Delete(string name)
        {
           try
           {
                var deleted = _projectService.Delete(name);
                if(deleted)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
           }
           catch (ProjectNotFound)
           {
               return StatusCode(404, new Dictionary<string, string>() {
                    {"error", $"project with id {name} was not found"}
               });
           }
        }
    }
}