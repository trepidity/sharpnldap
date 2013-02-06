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
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using Novell.Directory.Ldap;

namespace sharpnldap.util
{
	public static class ZFDAppUtils
	{
		/// <summary>
		/// This strips the values from the appAssociations attribute so that we get
		/// back the FQN of the associated ZFD App object
		/// 
		/// </summary>
		/// <param name="i">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public static string stripNalAttr(string i)
        {
            int loc = i.IndexOf("#");
            // string pattern = @"^.*\#";
            //string ii2 = i.Substring(loc);		
			Console.WriteLine ("Substring {0}", i.Substring(0, loc));
            return i.Substring(0, loc);
        }
		
		/// <summary>
		/// Returns true if the int value contains a bitwise result of the flag specified
		/// </summary>
		public static bool isZFDFlag(int i, ZFDNALFlags flg) {
			int f = (int)flg;
			return ((i & f) == f);
		}
		/// <summary>
		/// Returns the int value of the bitwise NAL Flag values that are stored in eDirectory
		/// 
		/// Input param expects the attribute appAssociations value i.e. cn=app1,ou=apps,o=okc#1#0
		/// </summary>
		/// <param name="i">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
        public static int getNalAttr(string i)
        {			
			string v = i.Substring(i.Length -3, 1); //the the last value from e.g. "cn=app1,ou=apps,o=okc#1#0"
            return int.Parse(v);
        }
		
		/// <summary>
		/// Compares current values in eDirectory with the values of the object that is to be written to eDirectory
		/// The results are returned as an LdapModifications array
		/// </summary>
		/// <param name="newApp">
		/// A <see cref="LDAPZFDApp"/>
		/// </param>
		/// <param name="oldApp">
		/// A <see cref="LDAPZFDApp"/>
		/// </param>
		/// <returns>
		/// A <see cref="LdapModification[]"/>
		/// </returns>
		internal static ArrayList BuildZFDAppModifications(LDAPZFDApp newApp, LDAPZFDApp oldApp) {
			LdapAttribute attribute;
			ArrayList modList = new ArrayList();

			/* If associations list do not match, replace */
			if (newApp.getZENAppAssociations().SequenceEqual(oldApp.getZENAppAssociations()) == false) {
				
				Logger.Debug("Current Associations and modified Associations do not match {0}", newApp.getDN()); 
				
				attribute = new LdapAttribute( "appAssociations", newApp.getZENAppAssociations().ToArray());
				modList.Add( new LdapModification(LdapModification.REPLACE, attribute)); //Add to the list of mods
			}
			
			return modList;	
		}		
	}
}
