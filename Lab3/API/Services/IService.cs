using System.Collections.Generic;

namespace Services
{
    public interface IService<T>
    {
        List<T> Get();
        T Get(string id);
        T Create(T obj);
        void Update(string id, T obj);
        void Remove(string id);
    }
}