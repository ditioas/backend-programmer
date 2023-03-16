// auth handling, error handling and unit testing not implemented.

[Route("api/[controller]")]
public class UserCompanyController : Controller
{
    private readonly IUserCompanyService userCompanyService;

    [HttpGet("{userId}")]
    public IActionResult GetCompaniesIdByUserId(int userId)
    {
        var companies = userCompanyService.GetCompaniesByUserId(userId);
        return Ok(companies);
    }
}

[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService companyService;

    [HttpGet("{companyId}")]
    public ActionResult GetCompanyById(List<int> companyIds)
    {
        var company = companyService.GetCompanyById(companyId);
        return Ok(url);
    }

    [HttpGet("{companyId}/url")]
    public ActionResult<string> GetUrlForCompany(int companyId)
    {
        var url = companyService.GetUrlForCompany(companyId);
        return Ok(url);
    }
}


[Route("api/[controller]")]
public class UserProjectController : ControllerBase
{
    private readonly IUserProjectService userProjectService;

    [HttpGet("{userId}")]
    public IActionResult GetProjectIdsForUser(int userId)
    {
        var projects = projectService.GetProjectIdsForUser(userId);
        return Ok(projects);
    }
}

[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService projectService;

    [HttpGet("")]
    public ActionResult GetProjectsById([FromQuery] List<int> projectIds)
    {
        var company = companyService.GetUrlForCompany(companyId);
        return Ok(url);
    }
}

[Route("api/[controller]")]
public class UserResourceController : ControllerBase
{
    private readonly IUserResouceService userResouceService;

    [HttpGet("{userId}")]
    public IActionResult GetResourceIdForUser(int userId)
    {
        var resourceId = userResouceService.GetResourceIdForUser(userId);
        return Ok(resourceId);
    }
}


[Route("api/[controller]")]
public class ResourceController : ControllerBase
{
    private readonly IResourceService resourceService;

    [HttpGet("")]
    public ActionResult<List<Resource>> GetResourcesById([FromQuery] List<int> projectIds)
    {
        var resources = resourceService.GetResourcesById(resourceIds);
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
