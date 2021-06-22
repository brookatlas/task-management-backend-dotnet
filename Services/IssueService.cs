using System.Linq;
using task_management_backend_dotnet.Models;


namespace task_management_backend_dotnet.Services
{
    public class IssueService
    {
        private IssueContext _issueContext;


        public IssueService(IssueContext context)
        {
            if(context != null)
            {
                _issueContext = context;
            }
            else
            {
                _issueContext  = new IssueContext();
            }
        }


        public Issue Get(int id)
        {
            try
            {
                var matchingIssue = _issueContext.Issues.Single(
                i => i.id == id
                );
                return matchingIssue;
            }
            catch(System.InvalidOperationException)
            {
                throw new IssueNotFound($"issue with id {id} was not found");
            }
        }

        public Issue Create(Issue issue)
        {
            try
            {
                var matchingIssue = _issueContext.Issues.Single(
                i => i.name == issue.name && i.parentProject == issue.parentProject
                );
                throw new IssueAlreadyExists("the issue already exists.");
            }
            catch(System.InvalidOperationException)
            {
                _issueContext.Issues.Add(issue);
                _issueContext.SaveChanges();
                return issue;
            }
        }

        public Issue Update(int id, string content)
        {
            try
            {
                var matchingIssue = _issueContext.Issues.Single(
                i => i.id == id
                );
                matchingIssue.content = content;
                _issueContext.Issues.Update(matchingIssue);
                _issueContext.SaveChanges();
                return matchingIssue;
            }
            catch(System.InvalidOperationException)
            {
                throw new IssueNotFound($"issue with id {id} was not found");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var matchingIssue = _issueContext.Issues.Single(
                    i => i.id == id
                );
                _issueContext.Issues.Remove(matchingIssue);
                _issueContext.SaveChanges();
                return true;
            }
            catch(System.InvalidOperationException)
            {
                throw new IssueNotFound($"issue with id {id} was not found");
            }
            catch
            {
                return false;
            }

        }

        public class IssueNotFound : System.Exception
        {
            public IssueNotFound() { }
            public IssueNotFound(string message) : base(message) { }
        }

        public class IssueAlreadyExists : System.Exception
        {
            public IssueAlreadyExists() {}
            public IssueAlreadyExists(string message): base(message) {}
        }
       
    }
}