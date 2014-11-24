using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using MFramework.Common.Core.Crypto;
using MFramework.Common.Core.Streams;

namespace MFramework.Common.Core.Extensions
{
    ///<summary>
	/// Класс свойств расширений для коллекций строк
	///</summary>
	[PublicAPI]
	public static class StringExtensions
	{
		///<summary>
		/// Объединяет коллекцию строк в одну строку использую разделитель
		///</summary>
		///<param name="source"></param>
		///<param name="separator"></param>
		///<returns></returns>
        [PublicAPI]
		public static string Join(this IEnumerable<string> source, string separator)
		{
			return string.Join(separator, source);
		}

		///<summary>
		/// Объединяет коллекцию строк в одну строку использую разделитель
		///</summary>
		///<param name="source"></param>
		///<param name="separator"></param>
		///<returns></returns>
        [PublicAPI]
		public static string Join(this StringCollection source, string separator)
		{
			return string.Join(separator, source.Cast<string>());
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        [PublicAPI]
        public static bool Contains(this string input, string value, StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(input) == false)
                return input.IndexOf(value, comparisonType) != -1;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [PublicAPI]
        public static bool LikewiseContains(this string input, string value)
        {
            return Contains(input, value, StringComparison.InvariantCultureIgnoreCase);
        }
        public static MemoryStream ToStream<TEncoding>(this string @this) where TEncoding:Encoding,new()
        {

            return ToStream(@this, new TEncoding());
        }
        public static MemoryStream ToStream(this string @this)
        {

            return ToStream(@this, new UTF8Encoding());
        }
        public static MemoryStream ToStream(this string @this,Encoding e)
        {
            
            return new MemoryStream(e.GetBytes(@this));
        }
        public static string Encrypt<TEncryptionProvider, TKey>(this string @this, TKey usingKey)
            where TEncryptionProvider : class, IEncryptionProvider<TKey>, new()
            where TKey : IEncryptionKey
        {

            var c = new TEncryptionProvider().GetEncryptor(usingKey);
            return ByteArrToString(c.Encrypt(Encoding.UTF8.GetBytes(@this)));    

        }

        public static string Decrypt<TEncryptionProvider, TKey>(this string @this, TKey usingKey)
            where TEncryptionProvider : class,IEncryptionProvider<TKey>, new()
            where TKey : IEncryptionKey
        {
            var c = new TEncryptionProvider().GetDecryptor(usingKey);
            return Encoding.UTF8.GetString(c.Decrypt(StrToByteArray(@this))).TrimEnd('\0');    


        }

        public static byte[] ToCypherBuffer(this string @this)
        {
            if (@this.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[@this.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(@this.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < @this.Length);
            return byteArr;   
        }
        public static string CypherBufferToString(this byte[] @this)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= @this.GetUpperBound(0); i++)
            {
                val = @this[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }

        /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //      return encoding.GetBytes(str);
        // However, this results in character values that cannot be passed in a URL.  So, instead, I just
        // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        public static byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
        //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //      return enc.GetString(byteArr);    
        public static string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }
	}
}