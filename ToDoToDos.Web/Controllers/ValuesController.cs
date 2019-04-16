using Microsoft.AspNetCore.Mvc;

namespace ToDoToDos.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public ActionResult<string> Get() => "All is ok";
	}
}
