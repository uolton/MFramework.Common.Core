using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Extensions
{
    public static class ActionExtension
    {
        public static bool RaiseException(Action @this)
        {
            try
            {
                @this();
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
