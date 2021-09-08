using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using task_management_backend_dotnet.Models;

namespace task_management_backend_dotnet.Services
{
    public class ProjectService
    {

        private TaskMangementDatabaseSettings _databaseSettings;
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Project> _projects;

        public ProjectService(TaskMangementDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
            _client = new MongoClient(_databaseSettings._ConnectionString);
            _database= _client.GetDatabase(databaseSettings._DatabaseName);
            _projects = _database.GetCollection<Project>(_databaseSettings._CollectionName);
        }


        public List<Project> Get()
        {
            return _projects.Find<Project>(project => true).ToList();
        }

        public Project Get(string name)
        {
            try
            {
                var MatchingProject = _projects.Find<Project>(
                    project => project.name == name
                ).FirstOrDefault();
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
            var MatchingProject = _projects.Find<Project>(
                p => p.name == project.name
            ).FirstOrDefault();
            if(MatchingProject != null)
            {
                throw new ProjectAlreadyExists(
                $"Project named: {project.name} already exists."
                );
            }
            else
            {
                _projects.InsertOne(project);
                return project;
            }
        }

     

        public bool Delete(string name)
        {
            var MatchingProject = _projects.Find<Project>(
                p => p.name == name
            ).FirstOrDefault();
            if(MatchingProject == null)
            {
                throw new ProjectNotFound(
                    $"Project with name: {name} was not found."
                );
            }
            else
            {
                _projects.DeleteOne(project => project.id == MatchingProject.id);
                return true;
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