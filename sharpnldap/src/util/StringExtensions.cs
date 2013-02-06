// found on the internet
// very cool code.

using System;
using System.Linq;
using System.Globalization;

namespace sharpnldap.util
{
	/// <summary>
	/// Really cool string functions
	/// </summary>
    public static class StringExtensions
    {
		/// <summary>
		/// Is the string null or is it's length 0
		/// Either case will cause a return result of true
		/// </summary>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public static bool IsEmpty(string s) {
			if ((s == null) || (s.Length == 0))
				return true;
			return false;
		}		
		
		/// <summary>
		/// Returns remaining string after the specified character
		/// </summary>
		/// <param name="source">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public static string SubstringAfter(this string source, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return source;
            }
            CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;            
            int index = compareInfo.IndexOf(source, value, CompareOptions.Ordinal);
            if (index < 0)
            {
                //No such substring
                return string.Empty;
            }
            return source.Substring(index + value.Length);            
        }

		/// <summary>
		/// Returns the string values before the character specified
		/// </summary>
		/// <param name="source">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public static string SubstringBefore(this string source, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            int index = compareInfo.IndexOf(source, value, CompareOptions.Ordinal);
            if (index < 0)
            {
                //No such substring
                return string.Empty;                    
            }
            return source.Substring(0, index);
        }
    }

}
