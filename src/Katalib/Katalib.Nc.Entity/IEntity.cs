using System;

namespace Katalib.Nc.Entity
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
