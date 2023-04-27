namespace sharpIt.Models;

public class Account : Profile
{
  public string Email { get; set; }
}


public class Profile : RepoItem<string>
{
  // public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }

}