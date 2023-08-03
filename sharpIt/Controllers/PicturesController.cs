namespace sharpIt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth;

  public PicturesController(PicturesService picturesService, Auth0Provider auth)
  {
    _picturesService = picturesService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  // NOTE This is an asynchronous request, so we need to specify that this is a task as part of the return type. Task is similar to a promise
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
