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

namespace sharpnldap
{
	/// <summary>
	/// implementing class of the <see cref="ZENReports.LDAPObject"/>
	/// This class represents and eDirectory Group Object
	/// </summary>
	public class LDAPGroup : LDAPObject
	{
		public LDAPGroup (string x) {
			dn = x;
		}

		private List<string> members;
		
		public void setGroupMembers(List<string> mbrs) {
			this.members = mbrs;
		}
		public void addGroupMembers(string mbr) {
			if (this.members == null)
				members = new List<string>();
			else
				this.members.Add (mbr);
		}
		
		public List<string> getGroupMembers() {
			return this.members;
		}		
		
		private const string objectclass = "group";
		public override string OBJECTCLASS
		{
			get{ 
				return objectclass;
			}
		}
	}
}
