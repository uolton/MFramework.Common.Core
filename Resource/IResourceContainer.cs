﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Resource
{
    public interface IResourceContainer
    {
        string GetString(string name);
        void Configure(IResourceConfiguration cfg);
    }
}
