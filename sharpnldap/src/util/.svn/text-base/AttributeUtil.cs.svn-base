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
using Novell.Directory.Ldap;

namespace sharpnldap.util
{
	public static class AttributeUtil
	{
		/// <summary>
		/// Returns the attribute value specified in the second string parameter
		/// 
		/// TODO: Need more error handling 
		/// </summary>
		/// <param name="entry">
		/// A <see cref="LdapEntry"/>
		/// </param>
		/// <param name="attr">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public static string getAttr(LdapAttributeSet attrSet, ATTRNAME attr) {
			string sAttr = attr.ToString();
			Logger.Debug("Requesting Attribute value of {0}", attrSet.getAttribute(sAttr));
			
			if (attrSet.getAttribute(sAttr) == null)
				return null;
			else {
				Logger.Debug(" Attribute {0} -> {1}", sAttr, attrSet.getAttribute(sAttr).StringValue);			
				return attrSet.getAttribute(sAttr).StringValue;
			}
		}	
		/// <summary>
		/// Parses a LdapAttributeSet and the specified user DN
		/// Returns a user object.
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="dn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LDAPUser"/>
		/// </returns>
		public static LDAPUser iterUsrAttrs(LdapAttributeSet attrSet, string dn) {
			LDAPUser user;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			if (attrSet.Count == 0) {
				Logger.Debug("No attributes in the AttributeSet for {0}", dn);
				return null;
			}
			
			user = new LDAPUser(dn);
			
			while(ienum.MoveNext())
			{				
				
				LdapAttribute attribute=(LdapAttribute)ienum.Current;		
				Logger.Debug("Parsing {0}", attribute);
				
				if (AttrEquals(attribute, ATTRNAME.NDSHOMEDIRECTORY))
					user.parseNdsHomeDirPath(AttributeUtil.getAttr(attrSet, ATTRNAME.NDSHOMEDIRECTORY));	
				
				if (AttrEquals(attribute, ATTRNAME.SN))
					user.setSN(AttributeUtil.getAttr(attrSet, ATTRNAME.SN));
				
				if (AttrEquals(attribute, ATTRNAME.GIVENNAME))
					user.setGivenName(AttributeUtil.getAttr(attrSet, ATTRNAME.GIVENNAME));
				
				if (AttrEquals(attribute, ATTRNAME.TITLE))
					user.Title = AttributeUtil.getAttr(attrSet, ATTRNAME.TITLE);
				
				if (AttrEquals(attribute, ATTRNAME.HOMEPHONE))
					user.HOMEPHONE = AttributeUtil.getAttr(attrSet, ATTRNAME.HOMEPHONE);
				
				if (AttrEquals(attribute, ATTRNAME.DISPLAYNAME))
					user.DISPLAYNAME = AttributeUtil.getAttr(attrSet, ATTRNAME.DISPLAYNAME);
			}
			return user;	
		}		
		
		private static bool AttrEquals(LdapAttribute attr, ATTRNAME name) {
			if (attr.Name.ToUpper().Equals(name.ToString()))
				return true;
			return false;
		}
			
			
		/// <summary>
		/// Parses a group objects attributes building the LDAPGroup object
		/// Requires the group objects attribute set and the DN.
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="dn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LDAPUser"/>
		/// </returns>
		public static LDAPGroup iterGroupAttrs(LdapAttributeSet attrSet, string dn) {
			LDAPGroup grp;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			if (attrSet.Count == 0)
				return null;
			
			grp = new LDAPGroup(dn);
			
			while(ienum.MoveNext())
			{				
				LdapAttribute attribute=(LdapAttribute)ienum.Current;			
				if (AttrEquals(attribute, (ATTRNAME.SN)))
					grp.setSN(AttributeUtil.getAttr(attrSet, ATTRNAME.SN));
				
				if (AttrEquals(attribute, (ATTRNAME.CN)))
					grp.setCN(AttributeUtil.getAttr(attrSet, ATTRNAME.CN));				
				
				if (AttrEquals(attribute, ATTRNAME.MEMBERS))
					grp.setGroupMembers( AttributeUtil.getListofAttr(attrSet, ATTRNAME.MEMBERS));
			}
			return grp;
		}
		
		/// <summary>
		/// Iterates the ZFD Application attributes
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="dn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LDAPGroup"/>
		/// </returns>
		public static LDAPZFDApp iterZFDAppAttrs(LdapAttributeSet attrSet, string dn) {
			LDAPZFDApp app;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			if (attrSet.Count == 0)
				return null;
			
			app = new LDAPZFDApp(dn);
			
			while(ienum.MoveNext())
			{				
				LdapAttribute attribute=(LdapAttribute)ienum.Current;			
				if (AttrEquals(attribute, ATTRNAME.APPASSOCIATIONS))
					app.setAssociations(AttributeUtil.getListofAttr(attrSet, ATTRNAME.APPASSOCIATIONS));
				
			}
			return app;
		}
		/// <summary>
		/// Returns null of no attributes match the attr parameter value
		/// Returns a list of strings that contain the attribute values that were specified in the attr param
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="attr">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<System.String>"/>
		/// </returns>
		public static List<string> getListofAttr(LdapAttributeSet attrSet, ATTRNAME attr) {
			string sAttr = attr.ToString();
			if (attrSet.getAttribute(sAttr) == null)
				return null;

			List<string> values = null;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			while(ienum.MoveNext())
			{
				LdapAttribute attribute=(LdapAttribute)ienum.Current;
				if (AttrEquals(attribute, attr)) {
					values = new List<string>(attrSet.getAttribute(sAttr).StringValueArray.Length);
					values.AddRange(attrSet.getAttribute(sAttr).StringValueArray); // take the values from the array
					
					if (Logger.LogLevel == Level.DEBUG) {
						foreach (string x in values) //debug purposes
						Logger.Debug("Values in {0} list {1}", attr, x);
					}
				}
			}
			return values;
		}		
		
		/// <summary>
		/// Returns the CN of a DN. 
		/// Provide the DN e.g. cn=jjennings,o=users and the cn "jjennings" will be returned.
		/// </summary>
		public static string ParseCNfromDN(string dn) {
			String a = dn.Substring(3).SubstringBefore(",");
			return a;
		}
		
	    static Dictionary<string, string> _dict = new Dictionary<string, string>
	    {
	        {"entry", "entries"},
	        {"image", "images"},
	        {"view", "views"},
	        {"file", "files"},
	        {"result", "results"},
	        {"word", "words"},
	        {"definition", "definitions"},
	        {"item", "items"},
	        {"megabyte", "megabytes"},
	        {"game", "games"}
	    };
		
		public static Dictionary<string, string> GetAttributes() {
			return _dict;
		}
	    public static string GetPlural(string word)
	    {
	        // Try to get the result in the static Dictionary
	        string result;
	        if (_dict.TryGetValue(word, out result))
	        {
	            return result;
	        }
	        else
	        {
	            return null;
	        }
	    }
		
	}
}
