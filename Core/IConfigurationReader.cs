﻿namespace Core
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
    }
}