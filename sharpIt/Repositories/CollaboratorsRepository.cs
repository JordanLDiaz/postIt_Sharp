using sharpIt.Interfaces;

namespace sharpIt.Repositories;

public class CollaboratorsRepository : IRepository<Collaborator>
{
  private readonly IDbConnection _db;

  public CollaboratorsRepository(IDbConnection db)
  {
    _db = db;
  }

  public List<Collaborator> Get()
  {
    throw new NotImplementedException();
  }

  public Collaborator GetOne(int id)
  {
    string sql = "SELECT * FROM collaborators WHERE id = @id;";

    Collaborator collab = _db.Query<Collaborator>(sql, new { id }).FirstOrDefault();
    return collab;
  }

  public Collaborator Insert(Collaborator postData)
  {
    throw new NotImplementedException();
  }

  internal void Remove(int id)
  {
    string sql = "DELETE FROM collaborators WHERE id = @id;";

    _db.Execute(sql, new { id });
  }

  public int Update(Collaborator updateData)
  {
    throw new NotImplementedException();
  }

  internal Collaborator CreateCollab(Collaborator collabData)
  {
    string sql = @"
    INSERT INTO 
    collaborators(albumId, accountId)
    VALUES(@AlbumId, @AccountId);
    SELECT LAST_INSERT_ID()
    ;";

    int id = _db.ExecuteScalar<int>(sql, collabData);
    collabData.Id = id;
    collabData.CreatedAt = DateTime.Now;
    collabData.UpdatedAt = DateTime.Now;
    return collabData;
  }

  internal List<AlbumCollaborator> GetCollaboratorsForAlbum(int albumId)
  {
    string sql = @"
    SELECT
    clab.*,
    prof.*
    FROM collaborators clab
    JOIN accounts prof ON clab.accountId = prof.Id
    WHERE clab.albumId = @albumId
    ;";

    List<AlbumCollaborator> collabs = _db.Query<Collaborator, AlbumCollaborator, AlbumCollaborator>
    (sql, (clab, prof) =>
    {
      prof.CollaborationId = clab.Id;
      return prof;
    }, new { albumId }).ToList();

    return collabs;
  }

  internal List<MyCollabAlbum> GetMyCollabAlbums(string userId)
  {
    string sql = @"
    SELECT
    clab.*,
    alb.*,
    prof.*
    FROM collaborators clab
    JOIN albums alb ON clab.albumId = alb.id
    JOIN accounts prof ON alb.creatorId = prof.id
    WHERE clab.accountId = @userId
    ;";

    List<MyCollabAlbum> albums = _db.Query<Collaborator, MyCollabAlbum, Profile, MyCollabAlbum>
    (sql, (clab, alb, prof) =>
    {
      alb.Creator = prof;
      alb.CollaborationId = clab.Id;
      return alb;
    }, new { userId }).ToList();

    return albums;
  }

  int IRepository<Collaborator>.Remove(int id)
  {
    throw new NotImplementedException();
  }
}
