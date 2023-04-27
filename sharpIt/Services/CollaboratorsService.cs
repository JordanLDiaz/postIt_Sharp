namespace sharpIt.Services;

public class CollaboratorsService
{
  private readonly CollaboratorsRepository _repo;

  public CollaboratorsService(CollaboratorsRepository repo)
  {
    _repo = repo;
  }

  internal Collaborator CreateCollab(Collaborator collabData)
  {
    Collaborator collaborator = _repo.CreateCollab(collabData);
    return collaborator;
  }

  internal List<AlbumCollaborator> GetCollaboratorsForAlbum(int albumId)
  {
    List<AlbumCollaborator> collabs = _repo.GetCollaboratorsForAlbum(albumId);
    return collabs;
  }

  internal List<MyCollabAlbum> GetMyCollabAlbums(string userId)
  {
    List<MyCollabAlbum> albums = _repo.GetMyCollabAlbums(userId);
    return albums;
  }

  internal Collaborator GetOne(int id)
  {
    Collaborator collab = _repo.GetOne(id);
    if (collab == null)
    {
      throw new Exception("YOU'RE DOING YOUR BEST, BUD");
    }
    return collab;
  }

  internal string RemoveCollab(int collabId, string userId)
  {
    Collaborator collab = GetOne(collabId);

    if (collab.AccountId != userId)
    {
      throw new Exception("GET OUTTA HERE");
    }

    _repo.Remove(collabId);

    return "NO LONGER BEST FRIENDS WITH THAT ALBUM";
  }
}
