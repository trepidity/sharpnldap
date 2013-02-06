using System;
using NUnit.Framework;
using sharpnldap;

namespace Application
{
	[TestFixture()]
	public class testLDAPZFDApp
	{
		[Test()]
		public void testCreateZFDApp ()
		{
			LDAPZFDApp app = new LDAPZFDApp("cn=app1,ou=apps,o=KC");
			app.setCN("app1");
			Assert.AreSame("app1", app.getCN());
			Assert.AreSame("cn=app1,ou=apps,o=KC", app.getDN());
		}
	}
}

