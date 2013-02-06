/******************************************************************************
* The MIT License
* Copyright (c) 2010 Jared L Jennings
* 
* Permission is hereby granted, free of charge, to any person obtaining  a copy
* of this software and associated documentation files (the Software), to deal
* in the Software without restriction, including  without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
* copies of the Software, and to  permit persons to whom the Software is 
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*******************************************************************************/
//
//
// Author:
//   Jared L Jennings (jaredljennings@gmail.com)
//
// (C) 2010 Jared L Jennings (jaredljennings@gmail.com)
//
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using sharpnldap.util;

namespace sharpnldap
{

	/// <summary>
	/// Represents a user object in eDirectory
	/// </summary>
	public class LDAPUser : LDAPObject
	{
		public LDAPUser (string x) {
			dn = x;
		}
		private List<string> memberOf;
		
		private const string objectclass = "person";
		public override string OBJECTCLASS
		{
			get{ 
				return objectclass;
			}
		}
		
		/// <summary>
		/// Contains the value which is read directly from eDirectory
		/// </summary>	
		private string ndsHomeDirectory;
		/// <summary>
		/// Returns the raw string value that as stored in eDirectory.
		/// This value is populated by <c>parseNdsHomeDirPath(string)</c>
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getNdsHomeDirectory()
		{ 
			return this.ndsHomeDirectory;
		}
		public string ndsHomeVolume		{ get;set; }
		/// <summary>
		/// Holds the path inside the volume of the users home directory.
		/// This is any folder below the volume including the users home folder.
		/// e.g. /users/students/jjennings
		/// </summary>
		public string ndsHomePath		{ get;set; }
		/// <summary>
		/// The server name holding the users home directory.
		/// </summary>
		public string ndsHomeServer		{ get;set; }
		public string DEPARTMENTNUMBER	{ get;set; }
		public string DISPLAYNAME		{ get;set; }
		public string EMPLOYEETYPE		{ get;set; }
		public string HOMEPHONE			{ get;set; }
		
		/// <summary>
		/// Sets the user title attribute
		/// </summary>
		public string Title {get;set;}
		
		/// <summary>
		/// Parses the eDirectory attribute ndsHomeDirectory which contains the 
		/// path, volume and server where the users home folder resides.
		/// 
		/// The parsed information is set to the different methods, i.e. home Vol, Home Path, Home Server
		/// the is set to null if a null parameter is passed.
		/// </summary>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		public void parseNdsHomeDirPath(string s) {
			
			if ((s == null) || (s.Length == 0)) {
				Logger.Debug("Passed a null or empty ndshomedirectorypath");
			}
			else {
				string[] a = Regex.Split(s, @",");
				string b = stripNdsHomeDirectory_FQN(a[0]); // remove the cn=
				string[] c = Regex.Split(b, @"_"); // remove the volume from the server
				
				if (c[0] != null) // get the server from the string
					ndsHomeServer = c[0];
				
				if (c[1] != null) // get volume from string
					ndsHomeVolume = c[1];
				
				/* get folder and path from string.
				 * TODO: Really sloppy. should just get last of the string values
				 */
				string p = s.SubstringAfter("#").SubstringAfter("#");
				ndsHomePath = p;
				this.ndsHomeDirectory = s;
			}
			
		}
		
		/// <summary>
		/// Returns the UNC path of the users home directory.
		/// 
		/// returns null if the server, volume and path are not pulated.
		/// Basically if the parseNdsHomeDirectory method wasn't called, these values will be empty.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getUncHome () {
			if ((ndsHomeServer == null) || (ndsHomeVolume == null) || (ndsHomePath == null))
				return null;
			
			//HACK: because some home directories will be set to a users folder instead of their folder
			if ( ndsHomePath.Contains(@"\") == false )
				return null;
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append( @"\\");
			sb.Append(ndsHomeServer);
			sb.Append(@"\");
			sb.Append(ndsHomeVolume);
			sb.Append(@"\");
			sb.Append(removeLeadingSlash(ndsHomePath));
			return sb.ToString();
		}
		
		/// <summary>
		/// Some of the ndsHomeDirectory paths contain an extra backslash (\)
		/// These need to be stripped if so.
		/// 
		/// Returns a empty string if null, does NOT return null
		/// </summary>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		private string removeLeadingSlash(string s) {
			
			if (StringExtensions.IsEmpty(s))
				return "";
			
			if(s.StartsWith(@"\"))
				return StringExtensions.SubstringAfter(s, @"\");
			else
				return s;			
		}

		/// <summary>
		/// Strings the cn= or whatever object type that is normally specified in FQN LDAP syntax strings
		/// returns the string of whatever was passed after the = character
		/// </summary>
		/// <remarks>This method can probably be replaced by AttributeUtil.ParseCNfromDN</remarks>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		private string stripNdsHomeDirectory_FQN(string s) {
			string[] a = Regex.Split(s, @"=");
			Logger.Debug("stripFQN {0}", a[1]);
			return a[1];
		}
		
		/// <summary>
		/// Sets what group objects the user is a member of 
		/// This clears any preexisting group members from the list
		/// </summary>
		/// <param name="mbrs">
		/// A <see cref="List<System.String>"/>
		/// </param>
		public void setGroupMemberOf(List<string> mbrs) {
			this.memberOf = mbrs;
		}
		
		/// <summary>
		/// Adds a group object to the list of group memberships
		/// </summary>
		/// <param name="mbr">
		/// A <see cref="System.String"/>
		/// </param>
		public void addGroupMemberOf(string mbr) {
			if (this.memberOf == null)
				memberOf = new List<string>();
			else
				this.memberOf.Add (mbr);
		}
		/// <summary>
		/// Returns a list of group names that the user is a member of
		/// </summary>
		/// <returns>
		/// A <see cref="List<System.String>"/>
		/// </returns>
		public List<string> getGroupMemberOf() {
			return this.memberOf;
		}
		public void getMethod(string s) {
			Type myType = typeof(LDAPUser);
			MemberInfo[] myMembers = myType.GetMembers();
			
            for(int i = 0; i < myMembers.Length; i++)
            {
				Console.WriteLine ("Member name {0}", myMembers[i]);
//                Object[] myAttributes = myMembers[i].GetCustomAttributes(true);
//                if(myAttributes.Length > 0)
//                {
//                    Console.WriteLine("\nThe attributes for the member {0} are: \n", myMembers[i]);
//                    for(int j = 0; j < myAttributes.Length; j++)
//                        Console.WriteLine("The type of the attribute is {0}.", myAttributes[j].GetType());
//                }
            }
			
			
		}
	}
}