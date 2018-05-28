using System;

namespace Core.Cache
{
    public interface ICacher
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, DateTime expireDate);
    }
}