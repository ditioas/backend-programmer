public interface IUserService
{
    List<Company> GetAllCompaniesForUser(int userId);

    List<Project> GetAllProjectsForUser(int userId);

    List<Resource> GetAllResourcesForUser(int userId);
}

public interface ICompanyService
{
    string GetDatabaseUrl(int companyId);
}

public interface IProjectService
{
    List<Task> GetAllTasksForProject(int projectId);
}
    
public interface ITaskService
{
    void CreateUserTask(int userId, int taskId, int resourceId, DateTime checkInTime);

    int GetUserTaskId(int userId, int taskId, int resourceId, bool taskCompleted);

    void UpdateUserTaskCompletion(int userTaskId, DateTime checkOutTime, bool taskCompleted);
}