using System;

namespace Common
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
