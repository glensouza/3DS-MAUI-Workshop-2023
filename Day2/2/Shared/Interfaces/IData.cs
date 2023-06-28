namespace Shared.Interfaces;

public interface IData<T>
{
    public List<T> GetAll();
    public T? GetById(string id);
    public List<T> GetByName(string name);
    public Task AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(string id);
}
