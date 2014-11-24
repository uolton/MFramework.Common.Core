using System.IO;

namespace MFramework.Common.Core.Extensions
{
    public static class StreamExtensions
    {
		/// <summary>
		/// ���������� ������ � ������ ����
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
        public static byte[] ToArray(this Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                var buffer = new byte[1024];
            
                int bytes;
                while ((bytes = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, bytes);
                }

                return ms.ToArray();
            }
        }
    }
}