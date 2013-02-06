using System;
using System.Collections;
using System.Collections.Generic;
using Novell.Directory.Ldap;
using NUnit.Framework;
namespace sharpnldap.util
{
	[TestFixture()]
	public class TestZFDApp
	{
		[Test()]
		public void TestZFDAppAssociation_ChangesFound ()
		{
			LDAPZFDApp newApp = new LDAPZFDApp("cn=app1, ou=apps, o=kc");
			LDAPZFDApp existApp = new LDAPZFDApp("cn=app1, ou=apps, o=kc");
			List<string> newAssoc = new List<string>();
			List<string> oldAssoc = new List<string>();
			
			Console.WriteLine ("array1 count before {0}", newAssoc.Count);
			
			newAssoc.Add("cn=user1,ou=users,o=kc");
			oldAssoc.Add("cn=user2,ou=users,o=kc");
			
			newApp.setZENAppAssociations(newAssoc);
			existApp.setZENAppAssociations(oldAssoc);
			Console.WriteLine ("array1 count {0}", newAssoc.Count);
			Console.WriteLine ("array2 count {0}", oldAssoc.Count);
			ArrayList modlist = ZFDAppUtils.BuildZFDAppModifications(newApp, existApp);
			
			foreach (LdapModification s in modlist) {
				Console.WriteLine ("Changes {0}", s.Attribute);
				Assert.AreEqual("cn=user1,ou=users,o=kc", s.Attribute.StringValue);
				Assert.AreEqual("appAssociations", s.Attribute.Name);
			}
			
			Assert.AreEqual(1,modlist.Count);
			
		}
		
		[Test()]
		public void TestZFDAppAssociation_ChangesNotFound() {
			LDAPZFDApp newApp = new LDAPZFDApp("cn=app1, ou=apps, o=kc");
			LDAPZFDApp existApp = new LDAPZFDApp("cn=app1, ou=apps, o=kc");
			List<string> newAssoc = new List<string>();
			List<string> oldAssoc = new List<string>();
			
			Console.WriteLine ("array1 count before {0}", newAssoc.Count);
			
			newAssoc.Add("cn=user1,ou=users,o=kc");
			oldAssoc.Add("cn=user1,ou=users,o=kc");
			
			newApp.setZENAppAssociations(newAssoc);
			existApp.setZENAppAssociations(oldAssoc);
			Console.WriteLine ("array1 count {0}", newAssoc.Count);
			Console.WriteLine ("array2 count {0}", oldAssoc.Count);
			ArrayList modlist = ZFDAppUtils.BuildZFDAppModifications(newApp, existApp);
			Assert.AreEqual(0,modlist.Count);			
		}
		
		[Test()]
		public void TestGetCNFromDN()
		{
			String cn = AttributeUtil.ParseCNfromDN("cn=user1,ou=users,o=kc");
			Assert.AreEqual("user1", cn);
		}
			
		[Test()]
		public void Test_Caption_NotNull() {
			Assert.IsNotNull(AttributeUtil.GetPlural("result"));
		}
			
	}
}

