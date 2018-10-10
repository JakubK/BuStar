using System.Collections.Generic;
using BuStarAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BuStarAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BuStarController : ControllerBase
  {
    private IRepository repository;

    public BuStarController(IRepository repositoryParam)
    {
      this.repository = repositoryParam;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetStopNames()
    {
      return Ok(repository.GetStopNames());
    }
  }
}