using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace task_management_backend_dotnet.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id {get;set;}
        public string name {get; set;}

        public List<Section> sections {get;set;}
    }


    public class Section
    {
        public string id{get;set;}
        public string name{get;set;}
        public List<Task> tasks;
    }
    public class Task
    {
        public string id {get; set;}

        public int type {get;set;}

        public string title {get; set;}
        public string content {get;set;}

    }

    public interface ITaskManagementDatabaseSettings
    {
        string _CollectionName {get;set;}
        string _ConnectionString {get;set;}
        string _DatabaseName {get;set;}
    
    }

    public class TaskMangementDatabaseSettings: ITaskManagementDatabaseSettings
    {
        public TaskMangementDatabaseSettings(
            string ConnectionString,
            string CollectionName,
            string DatabaseName
        )
        {
            _CollectionName = CollectionName;
            _ConnectionString = ConnectionString;
            _DatabaseName = DatabaseName;
        }
        public string _CollectionName {get;set;}
        public string _ConnectionString {get;set;}
        public string _DatabaseName {get;set;}
    }

}