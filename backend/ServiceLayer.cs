public interface IUserService
{
    List<Company> GetAllCompaniesForUser(int userId);

    List<Project> GetAllProjectsForUser(int userId);

    List<Resource> GetAllAvailableResourcesForUser(int userId);
}

public interface ICompanyService
{
    string GetDatabaseUrl(int companyId);
}

public interface IProjectService
{
    List<Task> GetAllTasksForProject(int projectId);
}

public interface IResourceService
{
    void UpdateResourceAvailability(int resourceId, bool isAvailable);
}
    
public interface IUserTaskService
{
    void CreateUserTask(int userId, int taskId, int resourceId, DateTime checkInTime);

    int GetUserTaskId(int userId, int taskId, int resourceId, bool taskCompleted);

    void UpdateUserTaskCompletion(int userTaskId, DateTime checkOutTime, bool taskCompleted);
}

// Client activity sequence for the api calls,
// 1. userService.GetAllCompaniesForUser()
// 2. companyService.GetDatabaseUrl()

// subsequent queries will go the the database server with the URL in step2.
// 3. userService.GetAllProjectsForUser()
// 4. userService.GetAllAvailableResourcesForUser()
// 5. projectService.GetAllTasksForProject()

// Check-in
// 6. userTaskService.CreateUserTask()
// 7. resourceService.UpdateResourceAvailability(false)

// Check-out
// 8. userTaskService.GetUserTaskId()
// 9. userTaskService.UpdateUserTaskCompletion()
// 10. resourceService.UpdateResourceAvailability(true)

