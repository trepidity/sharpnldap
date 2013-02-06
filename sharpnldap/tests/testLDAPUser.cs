using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace sharpnldap
{
	[TestFixture()]
	public class testLDAPUser
	{
		LDAPUser user;
		[SetUp]
	    public void Init()
	    {
			
			user = new LDAPUser("cn=user1,ou=users,o=kc");
			user.setGivenName("user1");
			user.setSN("jennings");
			List<string> members = new List<string>();
			members.Add("cn=everyone,ou=groups,o=kc");
			user.setGroupMemberOf(members);
			user.parseNdsHomeDirPath(@"cn=SOUTHSTUDENT_GENERAL,ou=ACHS,o=USD385#0#Home\2013\WildMC");
			user.Title = "Manager";			
			user.DISPLAYNAME = "Jared L Jennings";
			user.HOMEPHONE = "555.555.5555";
			Assert.AreEqual("cn=user1,ou=users,o=kc", user.getDN());
			Assert.AreEqual("user1", user.getGivenName());
			Assert.AreEqual("jennings", user.getSN());
			Assert.IsTrue(user.getGroupMemberOf().Contains("cn=everyone,ou=groups,o=kc"));
			Assert.AreEqual("person", user.OBJECTCLASS);
			Assert.AreEqual(@"Home\2013\WildMC", user.ndsHomePath);
			Assert.AreEqual("SOUTHSTUDENT", user.ndsHomeServer);
			Assert.AreEqual("GENERAL", user.ndsHomeVolume);
			Assert.AreEqual("Manager", user.Title);			
			Assert.AreEqual("Jared L Jennings", user.DISPLAYNAME);
			Assert.AreEqual("555.555.5555", user.HOMEPHONE);		
		}
		[Test()]
		public void Test_HomePath ()
		{
			Assert.AreEqual(@"Home\2013\WildMC", user.ndsHomePath);
		}
		[Test()]
		public void Test_HomeServer ()
		{
			Assert.AreEqual("SOUTHSTUDENT", user.ndsHomeServer);
			Assert.AreEqual("GENERAL", user.ndsHomeVolume);
		}	
		[Test()]
		public void Test_HomeVolume ()
		{

			Assert.AreEqual("GENERAL", user.ndsHomeVolume);
		}
	}
}

