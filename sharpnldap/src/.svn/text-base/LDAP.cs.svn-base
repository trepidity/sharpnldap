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
using System.Collections;
using System.Collections.Generic;
using Syscert = System.Security.Cryptography.X509Certificates;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Utilclass;
using Mono.Security.X509;
using Mono.Security.Cryptography;
using System.ComponentModel;
using System.Reflection;

using sharpnldap.util;

namespace sharpnldap
{
	public class LDAP
	{
		private LdapConnection lc;
		
		/// <summary>
		/// Should we connect over Secure LDAP TLS?
		/// </summary>
		//private bool secureLDAP = false;
		
		/// <summary>
		/// Does the user accept the SSL certificate and want to make the secure connection?
		/// </summary>
		protected bool bHowToProceed, quit = false, removeFlag = false;
		
		/// <summary>
		/// How many times we have asked the user to accept the certificate chain
		/// </summary>
		protected int bindCount = 0;				
		private AuthUser authUser;
		
		/// <summary>
		/// Empty constructor
		/// </summary>
		public LDAP () {}
		
		/// <summary>
		/// Create an authenticated connection to the LDAP host specified in the AuthUser object
		/// </summary>
		/// <param name="auth">
		/// A <see cref="AuthUser"/>
		/// </param>
		/// <returns>
		/// A <see cref="LdapConnection"/>
		/// </returns>
		public LdapConnection connect(AuthUser auth) {
			this.authUser = auth;
			this.lc = CreateConnection();
			return this.lc;
		}
		
		/// <summary>
		/// Searchs for a specified user or users.
		/// A null or empty parameter can be passed in which case the search will be wild.
		/// </summary>
		/// <param name="cn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="Dictionary<System.String, LDAPUser>"/>
		/// </returns>
		public Dictionary<string, LDAPUser> findUniqUsers(string cn, LDAPConnOpts lco) {
					
			Logger.Debug("searching wth a scope level of {0}", lco);
				
			Dictionary<string, LDAPUser> d = new Dictionary<string, LDAPUser>();
			if (StringExtensions.IsEmpty(cn))
				cn = "*";
			
			LdapSearchResults lsc=lc.Search(authUser.BASE_DN,
				   (int)lco,
				   "(&(objectClass=Person)(cn=" + cn + "))", //e.g. (&(objectClass=user)(cn=jared*))
				   null,
				   false);
			
			while (lsc.hasMore()) // we found some matches in the search and have results to parse
			{
				LDAPUser user;
				LdapEntry nextEntry = null;
				
				try 
				{
					nextEntry = lsc.next();
					Logger.Debug("Next Entry {0}", nextEntry.DN);	
					user = AttributeUtil.iterUsrAttrs(nextEntry.getAttributeSet(), nextEntry.DN);
					d.Add(AttributeUtil.getAttr(nextEntry.getAttributeSet(), ATTRNAME.CN),user);
				}				
				catch (ArgumentException ae)
				{
					Logger.Debug(ae.StackTrace);
					Logger.Error("Duplicate user {0}", 	nextEntry.DN);
					continue;
				}
				catch(LdapException e) 
				{
					Logger.Error("Error: " + e.LdapErrorMessage);
					// Exception is thrown, go for next entry
					continue;
				}
			}
			return d;
		}
		
		/// <summary>
		/// Search for users with the specified CN.
		/// Wild cards can be used for the search.
		/// 
		/// Must specify the baseDN to search from.
		/// </summary>
		/// <exception cref="Exception">Exception is thrown if no users are found</exception>
		/// <param name="cn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="baseDN">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<LDAPUser>"/>
		/// </returns>
		public List<LDAPUser> findUsers(string cn, string baseDN) {
			return doSearchForUsers(cn, baseDN, LDAPConnOpts.SCOPE_SUB, false);
		}
		
		/// <summary>
		/// Search for users with the specified CN.
		/// Wild cards can be used for the search.
		/// 
		/// Must specify the baseDN to search from.
		/// 
		/// Specify the LDAP search criteria. SUB, One, BASE
		/// These control if the search will searc subcontainers or not
		/// </summary>
		/// <exception cref="Exception">Exception is thrown if no users are found</exception>
		/// <param name="cn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="baseDN">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="lco">
		/// A <see cref="LDAPConnOpts"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<LDAPUser>"/>
		/// </returns>
		public List<LDAPUser> findUsers(string cn, string baseDN, LDAPConnOpts lco) {
			return doSearchForUsers(cn, baseDN, lco, false);
		}
		
		/// <summary>
		/// Search for users with the specified CN.
		/// Wild cards can be used for the search.
		/// 
		/// Must specify the baseDN to search from.
		/// 
		/// Specify the LDAP search criteria. SUB, One, BASE
		/// These control if the search will searc subcontainers or not.
		/// 
		/// The param allAttrs specifies whether or not the property unknown attributes is populated
		/// with attributes that have no match method (property in the LDAPUser object)
		/// </summary>
		/// <exception cref="Exception">Exception is thrown if no users are found</exception>
		/// <param name="cn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="baseDN">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="lco">
		/// A <see cref="LDAPConnOpts"/>
		/// </param>
		/// <param name="allAttrs">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<LDAPUser>"/>
		/// </returns>
		public List<LDAPUser> findUsers(string cn, string baseDN, LDAPConnOpts lco, bool allAttrs) {
			return doSearchForUsers(cn, baseDN, lco, false);
		}
		private List<LDAPUser> doSearchForUsers(string cn, string baseDN, LDAPConnOpts lco, bool allAttrs) {
			List<LDAPUser> users = new List<LDAPUser>();
			if (StringExtensions.IsEmpty(cn))
				cn = "*";
			int x = (int)lco;
			LdapSearchResults lsr = lc.Search(baseDN,
			                                  x,
			                                  "(&(objectClass=user)(cn=" + cn + "))",
			                                  null,
			                                  false
			                                  );
			if (lsr.hasMore() == false) {
				throw new Exception("No matching users found for " + cn);
			}
			else 
			{
				while (lsr.hasMore())
				{
					LDAPUser user;
					LdapEntry nextEntry = null;
					try {
						nextEntry = lsr.next();
						Logger.Debug("Next Entry {0}", nextEntry.DN);
						user = AttributeUtil.iterUsrAttrs(nextEntry.getAttributeSet(), nextEntry.DN);
						users.Add(user);
					}
					catch(LdapException e) 
					{
						Logger.Error("Error: " + e.LdapErrorMessage);
						// Exception is thrown, go for next entry
						continue;
					}
				}
			}
			return users;		
		}
		
		/// <summary>
		/// Returns a dictonary listing of all Group objects that are unique
		/// 
		/// if a null param or empty parm is specified, the search is wild.
		/// </summary>
		/// <param name="cn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="Dictionary<System.String, LDAPGroup>"/>
		/// </returns>
		public Dictionary<string, LDAPGroup> findUniqGroups(string cn, LDAPConnOpts lco) {
			
			Dictionary<string, LDAPGroup> d = new Dictionary<string, LDAPGroup>();
			if (StringExtensions.IsEmpty(cn))
				cn = "*";
			
			LdapSearchResults lsc=lc.Search(authUser.BASE_DN,
				   (int)lco,
				   "(&(objectClass=group)(cn=" + cn + "))", //e.g. (&(objectClass=user)(cn=jared*))
				   null,
				   false);
			
			while (lsc.hasMore()) // we found some matches in the search and have results to parse
			{
				LDAPGroup grp;
				LdapEntry nextEntry = null;
				
				try 
				{
					nextEntry = lsc.next();
					Logger.Debug("Next Entry {0}", nextEntry.DN);	
					grp = AttributeUtil.iterGroupAttrs(nextEntry.getAttributeSet(), nextEntry.DN);
					d.Add(AttributeUtil.getAttr(nextEntry.getAttributeSet(), ATTRNAME.CN),grp);
				}				
				catch (ArgumentException ae)
				{
					Logger.Debug(ae.StackTrace);
					Logger.Error("Duplicate user {0}", 	nextEntry.DN);
					continue;
				}
				catch(LdapException e) 
				{
					Logger.Error("Error: " + e.LdapErrorMessage);
					// Exception is thrown, go for next entry
					continue;
				}
			}
				
			
			return d;
		}		

		/// <summary>
		/// Performs a search from the base specified
		/// The ldap search must be specified e.g (&(objectClass=user)(cn=jared*))
		/// </summary>
		/// <param name="ldapSearchSyntax">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<LDAPUser>"/>
		/// </returns>
		public List<LDAPZFDApp> FindZFDApps(string cn, string baseDN, LDAPConnOpts lco)
		{	
			List<LDAPZFDApp> apps = new List<LDAPZFDApp>(); 
			if (StringExtensions.IsEmpty(cn))
			    cn = "*";
			    
			LdapSearchResults lsc=lc.Search(baseDN,
				   LdapConnection.SCOPE_SUB,
				   "(&(objectClass=appApplication)(cn=" + cn +"))", //e.g. (&(objectClass=user)(cn=jared*))
				   null,
				   false);
			
			while (lsc.hasMore()) // we found some matches in the search and have results to parse
			{
				LDAPZFDApp app;
				LdapEntry nextEntry = null;
				
				try 
				{
					nextEntry = lsc.next();
					Logger.Debug("Next Entry {0}", nextEntry.DN);
					app = AttributeUtil.iterZFDAppAttrs(nextEntry.getAttributeSet(), nextEntry.DN);
					apps.Add(app);
				}
				catch(LdapException e) 
				{
					Logger.Error("Error: " + e.LdapErrorMessage); // Exception is thrown, go for next entry
					continue;
				}
			}		
			return apps;
		}
		
		/// <summary>
		/// Returns a List<string> of all the members of a specific group.
		/// The string value is the members DN
		/// </summary>
		/// <param name="vals">
		/// A <see cref="List<System.String>"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<System.String>"/>
		/// </returns>
		public List<string> getGroupMembers(List<string> vals) {
			List<string> members = null;
			LdapConnection ldapConn = CreateConnection();
			Logger.Debug("Total number of associations to read {0}", vals.Count);
			
			foreach (string x in vals) {
				Logger.Debug("read dn {0}", x);
				LdapEntry le = ldapConn.Read(x);
				Logger.Debug("LDAP Entry Read {0}", le.getAttribute("cn").StringValue);
				Logger.Debug("LDAP Entry Read Attribute count {0}", le.getAttributeSet().Count); 
				members = AttributeUtil.getListofAttr(le.getAttributeSet(), ATTRNAME.MEMBER);
			}
			return members;
		}						
		
		/// <summary>
		/// Do we have all the required values to make a successfull LDAP conneciton
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		private bool ReqLDAPAuthVAL() {
			if (StringExtensions.IsEmpty(authUser.PASSWORD) 
			    || (StringExtensions.IsEmpty(authUser.LDAP_HOST)) 
			    || (StringExtensions.IsEmpty(authUser.USERNAME)))
			    return false;
			return true;
		}			    
						
		/// <summary>
		/// Creates the LDAP connection
		/// This method connects via LDAPS or LDAP based on the parameter setting of secureLDAP
		/// </summary>
		/// <returns>
		/// A <see cref="LdapConnection"/>
		/// </returns>
		private LdapConnection CreateConnection()
		{
			bHowToProceed = true; // Is set to true if the user did not accept the certificate to the truststore

			// Creating an LdapConnection instance 
			if (lc == null) {
				Logger.Debug("No pre-existing LDAP connection, creating new");
				lc = new LdapConnection();
			}
			
			if (ReqLDAPAuthVAL() == false)
				throw new Exception("Not all the required LDAP parameters exist. " +
					"We need a username, password and address");
			
			if (authUser.LDAP_SECURE) { // connect via SSL
				
				bindCount++;
				lc.SecureSocketLayer = true;
				Logger.Debug("setting CertificateValidationCallback(MySSLHandler)");
				lc.UserDefinedServerCertValidationDelegate += new CertificateValidationCallback(MySSLHandler);
			
				if (bHowToProceed == false) {
					Logger.Error("bHowToProceed is false");
					lc.Disconnect();
					throw new Exception("user does not trust SSL Certificates");
				}
				if ((lc.Connected == false) && (bHowToProceed)) {
					Logger.Info("starting secure LDAP connection to {0}:{1}", authUser.LDAP_HOST, authUser.LDAP_PORT);
					//Connect function will create a socket connection to the server
					lc.Connect(authUser.LDAP_HOST,authUser.LDAP_PORT);
	
					Logger.Debug("binding to {0}:{1}", authUser.LDAP_HOST, authUser.LDAP_PORT);
					//Bind function will Bind the user object Credentials to the Server
					lc.Bind(authUser.USERNAME,authUser.PASSWORD);	
					Logger.Info( " SSL Bind Successful " );
				}
			}
			else {
				if (lc.Connected == false) {
					
					Logger.Debug("Opening unsecure LDAP connection {0}:{1}", authUser.LDAP_HOST , authUser.LDAP_PORT);
					Logger.Debug("Attempting Bind");
					//Connect function will create a socket connection to the server
					lc.Connect(authUser.LDAP_HOST , authUser.LDAP_PORT);
					
					Logger.Debug("Successfully made a connection to {0}:{1}", lc.Host , lc.Port);
	
					//Bind function will Bind the user object credentials to the Server
					lc.Bind(authUser.USERNAME, authUser.PASSWORD);	
					Logger.Info("non-ssl LDAP bind successful to {0}:{1}",lc.Host,lc.Port);
				}				
			}
			return lc;
		}
		/// <summary>
		/// Delegete handler to process SSL Communications over LDAP.
		/// This allows the user to accept or reject a certificate chain
		/// Thus the certificate Auth Authority does not have to pre-exist in the truststore
		/// </summary>
		/// <param name="certificate">
		/// A <see cref="Syscert.X509Certificate"/>
		/// </param>
		/// <param name="certificateErrors">
		/// A <see cref="System.Int32[]"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		private bool MySSLHandler(Syscert.X509Certificate certificate, int[] certificateErrors)
		{
			Logger.Debug("calling MySSLHandler()");
			X509Store store = null;
			X509Stores stores = X509StoreManager.CurrentUser;
			String input;
			store = stores.TrustedRoot;
			
			//Import the details of the certificate from the server.
			
			X509Certificate x509 = null;
			X509CertificateCollection coll = new X509CertificateCollection ();
			Logger.Debug("calling GetRawCertData()");
			byte[] data = certificate.GetRawCertData();
			if (data != null)			
				x509 = new X509Certificate (data);
			
			//List the details of the Server
			
			//check for ceritficate in store
			X509CertificateCollection check = store.Certificates;
			if(!check.Contains(x509))
			{
				if(bindCount == 1)
				{
					Console.WriteLine ( " \n\nCERTIFICATE DETAILS: \n" );
					Console.WriteLine ( " {0}X.509 v{1} Certificate", (x509.IsSelfSigned ? "Self-signed " : 
					                                                   String.Empty), x509.Version);
					Console.WriteLine ( "  Serial Number: {0}", CryptoConvert.ToHex (x509.SerialNumber));
					Console.WriteLine ( "  Issuer Name:   {0}", x509.IssuerName);
					Console.WriteLine ( "  Subject Name:  {0}", x509.SubjectName);
					Console.WriteLine ( "  Valid From:    {0}", x509.ValidFrom);
					Console.WriteLine ( "  Valid Until:   {0}", x509.ValidUntil);
					Console.WriteLine ( "  Unique Hash:   {0}", CryptoConvert.ToHex (x509.Hash));
					
				}	
				
				//Get the response from the Client
				do
				{
					Console.WriteLine("\nDo you want to proceed with the connection (y/n)?");
					input = Console.ReadLine();
					
					if(input=="y" || input == "Y")
						bHowToProceed = true;
					
					if(input=="n" || input == "N")
						bHowToProceed = false;			
					
				}while(input!="y" && input != "Y" && input !="n" && input != "N");
			}	
			else
			{
				if(bHowToProceed == true)
				{
					//Add the certificate to the store.
				
					if (x509 != null)
						coll.Add (x509);
					store.Import (x509);
					if(bindCount == 1)
						removeFlag = true;			
				}
			}
			
			if(bHowToProceed == false)
			{
				//Remove the certificate added from the store.
				
				if(removeFlag == true && bindCount > 1)
				{				
					foreach (X509Certificate xt509 in store.Certificates) {
						if (CryptoConvert.ToHex (xt509.Hash) == CryptoConvert.ToHex (x509.Hash)) {
							store.Remove (x509);
						}				
					}
				}	
				Console.WriteLine("SSL Bind Failed.");
			}	
			return bHowToProceed;
		}	
		
		///<summary>Returns the specified object. Objects are specified by the DN.</summary>
		public LDAPZFDApp getZFDApp(string dn) {
			LdapEntry nextEntry = lc.Read(dn);
			LDAPZFDApp app = AttributeUtil.iterZFDAppAttrs(nextEntry.getAttributeSet(), nextEntry.DN);
			Logger.Debug("getZFDApp read entry {0}", nextEntry.DN);
			return app;
		}
		
		/// <summary>
		/// Saves changes made to existing ZFD 7 Application objects
		/// </summary>
		/// <param name="apps">
		/// A <see cref="List<LDAPZFDApp>"/>
		/// </param>
		public void modifyZFDApp(List<LDAPZFDApp> apps) {
			foreach (LDAPZFDApp app in apps)
				modifyZFDApp(app);
		}
		/// <summary>
		/// Saves changes made to an existing ZFD 7 Application object
		/// </summary>
		/// <param name="app">
		/// A <see cref="LDAPZFDApp"/>
		/// </param>
		public void modifyZFDApp(LDAPZFDApp app) {
			ArrayList modList;		
			LDAPZFDApp existingApp = getZFDApp(app.getDN());
			modList = ZFDAppUtils.BuildZFDAppModifications(app, existingApp);
			LdapModification[] mods = new LdapModification[modList.Count]; 	
			Type mtype=Type.GetType("Novell.Directory.LdapModification");
			mods = (LdapModification[])modList.ToArray(typeof(LdapModification));
		}
		
		/// <summary>
		/// Write list of LdapModifications to eDirectory
		/// </summary>
		private void WriteLdapChanges(LdapModification[] mods, string dn) {
			//Modify the entry in the directory
			lc.Modify ( dn, mods );	
		}
		
	}
}