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

namespace sharpnldap
{
	/// <summary>
	/// This object class acts as the admin user for the whole package
	/// This class contains all the needed information that is required for a
	/// ldap connection to eDirectory
	/// 
	/// The class is sealed, a singleton and thread safe
	/// </summary>
	public class AuthUser
	{
		
		public string 	USERNAME 	{ get;set; }
		public string 	PASSWORD 	{ get;set; }
		public string 	LDAP_HOST 	{ get;set; }
		public int 		LDAP_PORT 	{ get;set; }
		public string 	BASE_DN		{ get;set; }
		public bool 	LDAP_SECURE		{ get;set; }
		
		/// <summary>
		/// Constructor
		/// </summary>
		public AuthUser ()
		{

		}
	}
}