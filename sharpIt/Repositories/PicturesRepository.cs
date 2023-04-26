using sharpIt.Interfaces;

namespace sharpIt.Repositories;

public class PicturesRepository : IRepository<Picture>
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  public List<Picture> Get()
  {
    throw new NotImplementedException();
  }

  public Picture GetOne(int id)
  {
    throw new NotImplementedException();
  }

  public Picture Insert(Picture pictureData)
  {
    string sql = @"
    INSERT INTO pictures
    (imgUrl, creatorId, albumId)
    VALUES
    (@imgUrl, @creatorId, @albumId);

   SELECT
    pic.*,
    creator.*
   FROM pictures pic
   JOIN accounts creator ON pic.creatorId = creator.id
   WHERE pic.id = LAST_INSERT_ID();
    ";
    Picture picture = _db.Query<Picture, Account, Picture>(sql, (picture, creator) =>
    {
      picture.Creator = creator;
      return picture;
    }, pictureData).FirstOrDefault();

    return picture;
  }

  public int Remove(int id)
  {
    throw new NotImplementedException();
  }

  public int Update(Picture updateData)
  {
    throw new NotImplementedException();
  }

  internal List<Picture> GetAlbumPictures(int albumId)
  {
    string sql = @"
   SELECT
    pic.*,
    creator.*
   FROM pictures pic
   JOIN accounts creator ON pic.creatorId = creator.id
   WHERE pic.albumId = @albumId;
   ";
    List<Picture> pictures = _db.Query<Picture, Account, Picture>(sql, (picture, creator) =>
    {
      picture.Creator = creator;
      return picture;
    }, new { albumId }).ToList();
    return pictures;
  }


}
