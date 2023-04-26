namespace sharpIt.Interfaces;

internal interface IRepository<T>
{

  List<T> Get();

  T GetOne(int id);

  T Insert(T postData);

  int Update(T updateData);

  int Remove(int id);
}
