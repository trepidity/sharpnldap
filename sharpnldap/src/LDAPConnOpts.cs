using System;
using Novell.Directory.Ldap;
namespace sharpnldap
{
	/// <summary>
	/// LDAP Options which provide options when interacting with LDAP
	/// </summary>
	public enum LDAPConnOpts
	{
		SCOPE_ONE = LdapConnection.SCOPE_ONE, 
		SCOPE_SUB = LdapConnection.SCOPE_SUB,
		SCOPE_BASE = LdapConnection.SCOPE_BASE
	}
}

