// auth handling, error handling and unit testing not implemented.

[Route("api/[controller]")]
public class UserCompanyController : Controller
{
    private readonly IUserCompanyService userCompanyService;

    public UserCompanyController(IUserCompanyService userCompanyService)
    {
        userCompanyService = userCompanyService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetCompaniesByUserId(int userId)
    {
        var companies = userCompanyService.GetCompaniesByUserId(userId);
        return Ok(companies);
    }
}

[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService companyService;

    public CompanyController(ICompanyService companyService)
    {
        companyService = companyService;
    }

    [HttpGet("{companyId}/url")]
    public ActionResult<string> GetUrlForCompany(int companyId)
    {
        var url = companyService.GetUrlForCompany(companyId);
        return Ok(url);
    }
}

[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService projectService;

    public ProjectController(IProjectService projectService)
    {
        projectService = projectService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetProjectsForUser(int userId)
    {
        var projects = projectService.GetProjectsForUser(userId);
        return Ok(projects);
    }
}

[Route("api/[controller]")]
public class ResourceController : ControllerBase
{
    private readonly IResourceService resourceService;

    public ResourceController(IResourceService resourceService)
    {
        resourceService = resourceService;
    }

    [HttpGet("users/{userId}")]
    public ActionResult<List<Resource>> GetResourcesForUser(int userId)
    {
        var resources = resourceService.GetResourcesForUser(userId);
        return Ok(resources);
    }

    [HttpPut("{resourceId}/availability/{isAvailable}")]
    public IActionResult UpdateResourceAvailability(int resourceId, bool isAvailable)
    {
        resourceService.UpdateResourceAvailability(resourceId, isAvailable);
        return Ok();
    }
}

[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService taskService;

    public TaskController(ITaskService taskService)
    {
        taskService = taskService;
    }

    [HttpGet("project/{projectId}")]
    public IActionResult GetTasksByProjectId(int projectId)
    {
        var tasks = taskService.GetTasksByProjectId(projectId);
        return Ok(tasks);
    }
}


[Route("api/[controller]")]
public class UserTaskController : ControllerBase
{
    private readonly IUserTaskService userTaskService;

    public UserTaskController(IUserTaskService userTaskService)
    {
        userTaskService = userTaskService;
    }

    [HttpPost]
    public ActionResult CreateUserTask(int userId, int taskId, int resourceId, bool taskCompleted)
    {
        userTaskService.CreateUserTask(userId, taskId, resourceId, taskCompleted);    
    }

    [HttpGet("{userId}/tasks/{taskId}/resources/{resourceId}/completed/{taskCompleted}")]
    public ActionResult<int> GetUserTaskId(int userId, int taskId, int resourceId, bool taskCompleted)
    {
        var userTaskId = userTaskService.GetUserTaskId(userId, taskId, resourceId, taskCompleted); 
    }

    [HttpPut("{userTaskId}")]
    public ActionResult UpdateUserTaskCompletion(int userTaskId, DateTime checkOutTime, bool taskCompleted)
    {
        userTaskService.UpdateUserTaskCompletion(userTaskId, checkOutTime, taskCompleted);
    }
}



