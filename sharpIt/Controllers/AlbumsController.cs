namespace sharpIt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;

  private readonly CollaboratorsService _collaboratorsService;

  public AlbumsController(AlbumsService albumsService, CollaboratorsService collaboratorsService)
  {
    _albumsService = albumsService;
    _collaboratorsService = collaboratorsService;
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAlbums();
      return Ok(albums);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetOne(int albumId)
  {
    try
    {
      Album album = _albumsService.GetOne(albumId);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetAlbumPictures(int albumId)
  {
    try
    {
      List<Picture> pictures = _albumsService.GetAlbumPictures(albumId);
      return Ok(pictures);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}/collaborators")]
  public ActionResult<List<AlbumCollaborator>> GetCollaboratorsForAlbum(int albumId)
  {
    try
    {
      List<AlbumCollaborator> collabs = _collaboratorsService.GetCollaboratorsForAlbum(albumId);
      return Ok(collabs);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
