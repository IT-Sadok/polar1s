namespace Booker.Application.Contracts;

public interface IFileManager<TEntity>
{
    void WriteToFile(List<TEntity> entities, string filePath);
    List<TEntity> ReadFromFile(string filePath);
}