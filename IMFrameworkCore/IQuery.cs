﻿namespace IMFrameworkCore;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
