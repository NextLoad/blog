﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nextload.Blog.ToolKits.Base.Paged
{
    public interface IPagedList<T> : IListResult<T>, IHasTotalCount
    {
    }
}
