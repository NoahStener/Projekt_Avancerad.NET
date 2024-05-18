namespace Projekt_Avancerad.NET.Interfaces
{
    public interface ICompany<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T>GetSingle(int id);
        Task<T> Add(T newEntity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);

    }
}
