// most of the methods in service interfaces have only the operations related to
// transaction in discussion. We can extend the interfaces to have other or all of the 
// CRUD methods that might be used. For example CompanyService might have addCompany() 
// method that can be invoked by admin to add a new company, similarly for other services.
// Task is a keyword in C# but we have used it as class name

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string Password { get; set; }

    public List<UserWorksForCompany> UserWorksForCompany {get; set;}

    public List<ProjectScheduledForUser> ProjectScheduledForUser {get; set;}

    public List<ResourceScheduledForUser> ResourceScheduledForUser {get; set;}
}

public class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyDatabaseUrl { get; set; }

    public List<UserWorksForCompany> UserWorksForCompany {get; set;}

    public List<Project> Project {get; set;}

    public List<Resource> Resource {get; set;}
}

public class UserWorksForCompany
{
    public int UserId { get; set; }

    public string CompanyId { get; set; }

    public User User { get; set; }

    public Company Company { get; set; }
}

public class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; }

    public string ProjectDescription { get; set; }

    public List<ProjectScheduledForUser> ProjectScheduledForUser {get; set; }

    public Company Company {get; set;}

    public List<Task> Task;
}

public class ProjectScheduledForUser
{
    public int ProjectId { get; set; }

    public int UserId {get; set;}

    public Project Project {get; set;}

    public User User {get; set;}
}

public class Resource
{
    public int ResourceId { get; set; }

    public string ResourceName { get; set; }

    public bool ResourceAvailable { get; set; }

    public Company Company {get; set;}

    public List<ResourceScheduledForUser> ResourceScheduledForUser;
}

public class ResourceScheduledForUser
{
    public int ResourceId { get; set; }

    public int UserId {get; set;}

    public Resource Resource {get; set;}

    public User User {get; set;}
}

public class Task
{
    public int TaskId { get; set; }

    public string TaskName { get; set; }

    public string TaskDescription { get; set; }

    public Project Project {get; set;}
}

public class UserTask
{
    public int UserTaskId { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }

    public int ResourceId { get; set; }

    public bool TaskCompleted {get; set;}

    public DateTime CheckInTime { get; set; }
    
    public DateTime CheckOutTime { get; set; }
}

public interface IUserCompanyService
{
    public User user;

    public Company company;
    List<int> GetCompaniesIdByUserId(int userId);
}

public interface ICompanyService
{
    List<Company> GetCompanyById(List<int> companyIds);

    string GetUrlForCompany(int companyId);
}

public interface IUserProjectService
{
    public List<int> GetProjectIdsForUser(int userId);
}

public interface IProjectService
{
    public List<Project> GetProjectsById(List<int> projectId);
}

public interface IUserResouceService
{
    public List<int> GetResourceIdForUser(int userId);
}

public interface IResourceService
{   
    List<Resource> GetResourcesById(List<int> resourceIds);
    void UpdateResourceAvailability(int resourceId, bool isAvailable);
}

public interface ITaskService
{
    List<Task> GetTasksByProjectId(int projectId);
}

public interface IUserTasksService
{
    void CreateUserTask(int userId, int taskId, int resourceId, DateTime checkInTime);

    int GetUserTaskId(int userId, int taskId, int resourceId, bool taskCompleted);

    void UpdateUserTaskCompletion(int userTaskId, DateTime checkOutTime, bool taskCompleted);
}



