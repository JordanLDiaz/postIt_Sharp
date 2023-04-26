using sharpIt.Interfaces;

namespace sharpIt.Repositories;

public class AlbumsRepository : IRepository<Album>
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  public List<Album> Get()
  {
    string sql = @"
    SELECT
    alb.*,
    creator.*
    FROM albums alb
    JOIN accounts creator ON creator.id = alb.creatorId
    ;
    ";
    // -----------------------------⬇️ 1st Select model---------⬇️ data from row
    // -------------------------------------⬇️ 2nd Select model --------⬇️ data from frow
    // ---⬇️----------------------------------------⬇️ return of Query
    List<Album> albums = _db.Query<Album, Account, Album>(sql, (album, creator) =>
    {
      album.Creator = creator; // Attach data back together
      return album; // return data of return model type
    }).ToList();
    return albums;
  }

  public Album GetOne(int id)
  {
    string sql = @"
    SELECT
    alb.*,
    creator.*
    FROM albums alb
    JOIN accounts creator ON creator.id = alb.creatorId
    WHERE alb.id = @id
    ;
    ";
    // -----------------------------⬇️ 1st Select model---------⬇️ data from row
    // -------------------------------------⬇️ 2nd Select model --------⬇️ data from frow
    // ---⬇️----------------------------------------⬇️ return of Query
    Album album = _db.Query<Album, Account, Album>(sql, (album, creator) =>
    {
      album.Creator = creator; // Attach data back together
      return album; // return data of return model type
    }, new { id }).FirstOrDefault();
    return album;
  }

  public Album Insert(Album albumData)
  {
    throw new NotImplementedException();
  }

  public int Remove(int id)
  {
    throw new NotImplementedException();
  }

  public int Update(Album updateData)
  {
    throw new NotImplementedException();
  }
}
