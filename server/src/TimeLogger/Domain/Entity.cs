﻿namespace Timelogger.Domain
{
    public abstract class Entity<T>
    {
        public T? Id { get; protected set; }
    }
}
