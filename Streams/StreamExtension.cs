using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams
{
    public static class StreamExtension
    {

        public static string AsString(this Stream @this)
        {
            var reader = new StreamReader(@this.Rewind());
            return reader.ReadToEnd();
        }

        public static T Rewind<T>(this T @this) where T : Stream
        {
            return @this.SetPosition(0L) ;
        }
        public static T SetPosition<T>(this T @this, long newPosition) where T : Stream
        {
            @this.Position = newPosition;
            return @this;
        }

    }
}
