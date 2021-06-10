using System.Linq;
using System.Collections.Generic;
using task_management_backend_dotnet.Models;

namespace task_management_backend_dotnet.Services
{
    public class ProjectService
    {

        private ProjectContext _projectContext;

        public ProjectService()
        {
            _projectContext = new ProjectContext();
        }


        public List<Project> Get()
        {
            var result = _projectContext.Projects.ToList<Project>();
            return result;
        }


        public Project Get(int id)
        {
            try
            {
                var MatchingProject =_projectContext.Projects.Single(
                    p => p.ProjectId == id
                );
                return MatchingProject;
            }
            catch (System.InvalidOperationException)
            {
                throw new ProjectNotFound(
                    $"project with id: {id} was not found"
                );
            }
        }

        public Project Get(string name)
        {
            try
            {
                var MatchingProject =_projectContext.Projects.Single(
                    p => p.name == name
                );    
                return MatchingProject;
            }
            catch (System.Exception)
            {
                throw new ProjectNotFound(
                    $"project with name: {name} was not found"
                );
            }
        }

        public Project Create(Project project)
        {
            try
            {
                var MatchingProject =_projectContext.Projects.Single(
                    p => p.name == project.name
                );
                throw new ProjectAlreadyExists(
                    $"Project named: {project.name} already exists."
                );
                
            }
            catch(System.InvalidOperationException)
            {
                _projectContext.Projects.Add(project);
                _projectContext.SaveChanges();
                return project;
            }
        }

        public Project Update(Project project)
        {
            try
            {
                var MatchingProject = _projectContext.Projects.Single(
                    p => p.ProjectId == project.ProjectId
                );
                MatchingProject.creationTime = project.creationTime;
                MatchingProject.name = project.name;
                _projectContext.SaveChanges();
                return MatchingProject;
            }
            catch (System.InvalidOperationException)
            {
                throw new ProjectNotFound(
                    $"Project with id: {project.ProjectId} was not found"
                );
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var MatchingProject = _projectContext.Projects.Single(
                    p => p.ProjectId == id
                );
                _projectContext.Projects.Remove(MatchingProject);
                _projectContext.SaveChanges(); 
                return true;
            }
            catch (System.InvalidOperationException)
            {
                throw new ProjectNotFound(
                    $"Project with id: {id} was not found."
                );
            }
        }
    }


    public class ProjectNotFound : System.Exception
    {
        public ProjectNotFound() { }
        public ProjectNotFound(string message) : base(message) { }
    }

    public class ProjectAlreadyExists : System.Exception
    {
        public ProjectAlreadyExists() { }
        public ProjectAlreadyExists(string message) : base(message) { }
    }
}