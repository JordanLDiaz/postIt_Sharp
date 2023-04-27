namespace sharpIt.Models;

public class Picture : RepoItem<int>
{
  // public int Id { get; set; }
  public string ImgUrl { get; set; }
  public int AlbumId { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}
