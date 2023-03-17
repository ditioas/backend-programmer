public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    [HttpGet("{userId}/companies")]
    public IActionResult GetAllCompaniesForUser(int userId)
    {
        var companies = _userService.GetAllCompaniesForUser(userId);
        return Ok(companies);
    }

    [HttpGet("{userId}/projects")]
    public IActionResult GetAllProjectsForUser(int userId)
    {
        var projects = _userService.GetAllProjectsForUser(userId);
        return Ok(projects);
    }

    [HttpGet("{userId}/resources")]
    public IActionResult GetAllAvailableResourcesForUser(int userId)
    {
        var resources = _userService.GetAllAvailableResourcesForUser(userId);
        return Ok(resources);
    }
}

public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    [HttpGet("{companyId}/databaseUrl")]
    public ActionResult<string> GetDatabaseUrl(int companyId)
    {
        var url = _companyService.GetDatabaseUrl(companyId);
        return Ok(url);
    }
}

public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    [HttpGet("{projectId}/tasks")]
    public ActionResult<List<Task>> GetAllTasksForProject(int projectId)
    {
        var tasks = _projectService.GetAllTasksForProject(projectId);
        return Ok(tasks);
    }
}

public class UserTaskController : ControllerBase
{
    private readonly IUserTaskService _userTaskService;
    private readonly IResourceService _resourceService;

    [HttpPost("checkin")]
    public IActionResult Checkin(int userId, int taskId, int resourceId, DateTime checkInTime)
    {
        // on check in user creates a task, with default value for istaskcompleted as false
        _userTaskService.CreateUserTask(userId, taskId, resourceId, checkInTime);
        _resourceService.UpdateResourceAvailability(resourceId, resourceAvailable=false);
        return Ok();
    }

    [HttpPost("checkout")]
    public IActionResult Checkout(int userId, int taskId, int resourceId, DateTime checkOutTime)
    {
        // Get the usertaskid for user,task,resource combination where taskisnotcompleted
        int userTaskId = userTaskService.GetUserTaskId(userId, taskId, resourceId, taskCompleted=false);

        // Update the task as completed, record the checkout time and set the resource available
        userTaskService.UpdateUserTaskCompletion(userTaskId, checkOutTime, taskCompleted=true);
        resourceService.UpdateResourceAvailability(resourceId, resourceAvailable=true);
        return Ok();
    }
}
