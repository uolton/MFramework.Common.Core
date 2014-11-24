using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MFramework.Common.Core.Resource
{
    /// <summary>
    /// Interfaccia esposta dalle risorse
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    public interface IResource<T>
    {
        T Value { get; }

        IResource<T> Register(IResourceContainer c);
    }
}
