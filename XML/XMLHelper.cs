﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.XML
{
    public class XMLHelper
    {
        /// <summary>
        /// Converts a .NET type into an XML compatible type - roughly
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MapTypeToXmlType(Type type)
        {
            if (type == typeof(string) || type == typeof(char))
                return "string";
            if (type == typeof(int) || type == typeof(Int32))
                return "integer";
            if (type == typeof(long) || type == typeof(Int64))
                return "long";
            if (type == typeof(bool))
                return "boolean";
            if (type == typeof(DateTime))
                return "datetime";

            if (type == typeof(float))
                return "float";
            if (type == typeof(decimal))
                return "decimal";
            if (type == typeof(double))
                return "double";
            if (type == typeof(Single))
                return "single";

            if (type == typeof(byte))
                return "byte";

            if (type == typeof(byte[]))
                return "base64Binary";

            return null;

            // *** hope for the best
            //return type.ToString().ToLower();
        }


        public static Type MapXmlTypeToType(string xmlType)
        {
            xmlType = xmlType.ToLower();

            if (xmlType == "string")
                return typeof(string);
            if (xmlType == "integer")
                return typeof(int);
            if (xmlType == "long")
                return typeof(long);
            if (xmlType == "boolean")
                return typeof(bool);
            if (xmlType == "datetime")
                return typeof(DateTime);
            if (xmlType == "float")
                return typeof(float);
            if (xmlType == "decimal")
                return typeof(decimal);
            if (xmlType == "double")
                return typeof(Double);
            if (xmlType == "single")
                return typeof(Single);

            if (xmlType == "byte")
                return typeof(byte);
            if (xmlType == "base64binary")
                return typeof(byte[]);


            // return null if no match is found
            // don't throw so the caller can decide more efficiently what to do 
            // with this error result
            return null;
        }
    }
}