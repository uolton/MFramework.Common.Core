using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.Resource.Containers
{
    public class Resx<TConfiguration> : Resx
        where TConfiguration : Resx.ResxConfigurationBase, new()
    {
        public Resx():base(new TConfiguration()){}
    }
    
    public class Resx:IResourceContainer
    {
        private global::System.Resources.ResourceManager _rm;
        private CultureInfo _ci;
        public Resx(){}

        public Resx(ResxConfigurationBase cfg) : this()
        {
            Configure(cfg);
        }
        public string GetString(string name)
        {
            return _rm.GetString(name, _ci);
        }

        public void Configure(IResourceConfiguration cfg)
        {
            cfg.Configure(this);
        }

        public abstract class ResxConfigurationBase : IResourceConfiguration
        {
            private string _baseName;
            private CultureInfo _culture;
            private Assembly _assembly;

            public ResxConfigurationBase(string baseName, Assembly assembly, CultureInfo ci)
            {
                _baseName = baseName;
                _assembly = assembly;
                _culture = ci;
            }
            private void ConfigureResx(Resx r)
            {
                r._rm = new global::System.Resources.ResourceManager(_baseName, _assembly);
            }
            public void Configure(IResourceContainer c)
            {
                ConfigureResx( c.As<Resx>());
            }
        }

        public class ResxConfigurationOnAssemblyDefault<TAssemblyClass> : ResxConfigurationBase
        {
            public ResxConfigurationOnAssemblyDefault()
                : base(
                    DefaultBaseNameFor(typeof(TAssemblyClass).Assembly), typeof(TAssemblyClass).Assembly, CultureInfo.CurrentCulture) { }
            private static string DefaultBaseNameFor(Assembly assembly)
            {
                string[] manifest = assembly.GetManifestResourceNames();
                return manifest[0].Replace(".resources", string.Empty);
            }

        }
    }
}
