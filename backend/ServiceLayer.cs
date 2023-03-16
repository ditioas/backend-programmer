// most of the methods in service interfaces have only the operations related to
// transaction in discussion. We can extend the interfaces to have other or all of the 
// CRUD methods that might be used. For example CompanyService might have addCompany() 
// method that can be invoked by admin to add a new company, similarly for other services.

public class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyDatabaseUrl { get; set; }
}

public class Project
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string Description { get; set; }
}

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Available { get; set; }
}

public class Task
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
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
    List<Company> GetCompaniesByUserId(int userId);
}

public interface ICompanyService
{
    string GetUrlForCompany(int companyId);
}

public interface IProjectService
{
    public List<Project> GetProjectsForUser(int userId);
}

public interface IResourceService
{
    List<Resource> GetResourcesForUser(int userId);

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

    void UpdateUserTaskCompletion(int userTaskId, DateTime checkOutTime);
}



