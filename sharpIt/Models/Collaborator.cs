namespace sharpIt.Models;

public class Collaborator : RepoItem<int>
{
  // public int Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
}

public class AlbumCollaborator : Profile
{
  public int CollaborationId { get; set; }

  // NOTE these properties are all brought in with inheritance
  // public string Id { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }

}

public class MyCollabAlbum : Album
{
  public int CollaborationId { get; set; }

}