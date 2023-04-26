namespace sharpIt.Services;

public class PicturesService
{
  private readonly PicturesRepository _repo;

  public PicturesService(PicturesRepository repo)
  {
    _repo = repo;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repo.Insert(pictureData);
    return picture;
  }

  internal List<Picture> GetAlbumPictures(int albumId)
  {
    List<Picture> pictures = _repo.GetAlbumPictures(albumId);
    return pictures;
  }
}
