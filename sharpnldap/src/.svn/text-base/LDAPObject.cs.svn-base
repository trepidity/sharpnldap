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
using System.Collections.Generic;
using sharpnldap.util;

namespace sharpnldap
{
	/// <summary>
	/// Abstract class representing eDirectory objects.
	/// The class is intented to be exited and the constructor of the implementing class
	/// should take a single param which is the objects DN
	/// </summary>	
	abstract public class LDAPObject
	{
		private const string objectclass = "LDAPObject";
		/// <summary>
		/// Returns the LDAP class type of an object.
		/// </summary>
		public abstract string OBJECTCLASS {
			get;
		}
		
		/// <summary>
		/// Objects Common Name
		/// </summary>
		protected string cn;
		
		/// <summary>
		/// Objects last name
		/// </summary>
		protected string sn;
		
		/// <summary>
		/// Objects first name
		/// </summary>
		protected string givenName;
		
		/// <summary>
		/// Objects destinquished name i.e. cn=user, ou=container,o=container
		/// </summary>
		protected string dn;
		
		/// <summary>
		/// returns the objects DN. This should only be set through the constructor
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getDN() {
			return this.dn;	
		}
		
		/// <summary>
		/// ZENworks application objects that are directly assigned to the object
		/// </summary>
		protected List<string> zenAppAssociations;
		
		public void setCN(string s) {
			this.cn = s;
		}
		public string getCN() {
			if (StringExtensions.IsEmpty(this.cn))
				return AttributeUtil.ParseCNfromDN(this.dn);
				
			return this.cn;
		}
		
		public void setSN(string s) {
			this.sn = s;
		}
		public string getSN() {
			return this.sn;
		}
		
		public void setGivenName(string s) {
			this.givenName = s;
		}
		public string getGivenName() {
			return this.givenName;
		}
		
		/// <summary>
		/// This method removes any previous list of associated ZENworks application objects
		/// with the params that are passed to the method.
		/// 
		/// To add to an existing list of association app objects that are directly associated to the method
		/// use the addZENAppsAssocations method
		/// </summary>
		/// <param name="apps">
		/// A <see cref="List<System.String>"/>
		/// </param>
		public void setZENAppAssociations(List<string> apps) {
			this.zenAppAssociations = apps;
		}
		
		public List<string> getZENAppAssociations() {
			return this.zenAppAssociations;
		}
		
		/// <summary>
		/// Adds to the already existing list of association zenworks applicaiton objects. 
		/// If the list is empty a new list is created.
		/// </summary>
		/// <param name="app">
		/// A <see cref="System.String"/>
		/// </param>
		public void addZENAppAssociations(string app) {
			if (this.zenAppAssociations == null)
				this.zenAppAssociations = new List<string>();
			else
				this.zenAppAssociations.Add(app);
		}
	}
}
