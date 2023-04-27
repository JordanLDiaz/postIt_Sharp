namespace sharpIt.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
  private readonly CollaboratorsService _collaboratorsService;
  private readonly Auth0Provider _auth;

  public CollaboratorsController(CollaboratorsService collaboratorsService, Auth0Provider auth)
  {
    _collaboratorsService = collaboratorsService;
    _auth = auth;
  }

  [HttpPost]
  public async Task<ActionResult<Collaborator>> CreateCollab([FromBody] Collaborator collabData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      collabData.AccountId = userInfo.Id;
      Collaborator collaborator = _collaboratorsService.CreateCollab(collabData);
      return Ok(collaborator);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{collabId}")]
  public async Task<ActionResult<string>> RemoveCollab(int collabId)
  {
    try
    {

      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _collaboratorsService.RemoveCollab(collabId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
