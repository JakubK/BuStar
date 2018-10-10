using System.Collections.Generic;
using BuStarAPI.Models;
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
    [Route("buses")]
    public ActionResult<IEnumerable<string>> GetStopNames()
    {
      return Ok(repository.GetStopNames());
    }    

    [HttpGet]
    [Route("stopdata/{stop}")]
    public ActionResult<StopInfoResponse> GetStopData(string stop)
    {
      return Ok();
    }
  }
}