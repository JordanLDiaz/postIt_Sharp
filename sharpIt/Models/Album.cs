namespace sharpIt.Models;

public class Album
{
  public int Id;
  public string Title { get; set; }
  public string Category { get; set; }
  public string CoverImg { get; set; }
  public string CreatorId { get; set; }
  public bool Archived { get; set; }

  public Account Creator { get; set; }


  // the {get; set;} allows for more functionality when someone gets a value, or sets a value
  // public bool Archived
  // {
  //   get; set
  //   {
  //     // emit this changed, trigger listeners
  //     // Archived = value
  //   };
  // }
  // TODO need on more thing? what could it be?
}
