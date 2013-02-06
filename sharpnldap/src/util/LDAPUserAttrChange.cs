using System;
using System.Collections;
using Novell.Directory.Ldap;
namespace sharpnldap.util
{
	public static class LDAPUserAttrChange
	{
		private static LdapAttribute attribute;
		private static ArrayList modList;

		/// <summary>
		/// Compares LDAPUser class with what is already stored in eDirectory.
		/// Returns the results of the comparison if there are changes.
		/// Local differences always override eDirectory
		/// </summary>
		/// <returns>
		/// A <see cref="ArrayList"/>
		/// </returns>
		internal static ArrayList BuildLDAPUserModifications(LDAPUser newUser, LDAPUser currUser)
		{	
			modList = new ArrayList ();	
			/* If values do not match, replace */
			if (AttrEqual(newUser.Title, currUser.Title) == false)
				MakeLdapMod(ATTRNAME.TITLE, newUser.Title);
			
			if (AttrEqual(newUser.DISPLAYNAME, currUser.DISPLAYNAME) == false)
				MakeLdapMod(ATTRNAME.DISPLAYNAME, newUser.DISPLAYNAME);
			
			if (AttrEqual(newUser.DEPARTMENTNUMBER, currUser.DEPARTMENTNUMBER) == false)
				MakeLdapMod(ATTRNAME.DEPARTMENTNUMBER, newUser.DEPARTMENTNUMBER);
			
			return modList;
		}
		
		private static void MakeLdapMod (ATTRNAME attr, string attrVal)
		{
			attribute = new LdapAttribute( attr.ToString(), attrVal);
			modList.Add( new LdapModification(LdapModification.REPLACE, attribute)); //Add to the list of mods
		}
		
		private static bool AttrEqual (string latestVal, string oldVal)
		{
			if (latestVal.Equals (oldVal) == false) {
				Logger.Debug ("Current {0} and modified {1} do not match ", oldVal, latestVal);
				return false;
			}
			return true;
		}
	}
}

