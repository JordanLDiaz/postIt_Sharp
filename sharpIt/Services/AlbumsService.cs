namespace sharpIt.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repo;
  private readonly PicturesService _picturesService;

  public AlbumsService(AlbumsRepository repo, PicturesService picturesService)
  {
    _repo = repo;
    _picturesService = picturesService;
  }


  internal List<Album> GetAlbums()
  {
    List<Album> albums = _repo.Get();
    return albums;
  }

  internal Album GetOne(int albumId)
  {
    Album album = _repo.GetOne(albumId);
    if (album == null) throw new Exception($"That Id is garbage: {albumId}");
    return album;
  }
  internal List<Picture> GetAlbumPictures(int albumId)
  {
    Album album = GetOne(albumId);
    // TODO what if album is archived?
    List<Picture> pictures = _picturesService.GetAlbumPictures(albumId);
    return pictures;
  }
}
