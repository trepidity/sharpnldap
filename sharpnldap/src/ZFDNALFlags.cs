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
	/// A enumeration of the different ZFD Application flags.
	/// The flags include, Application launcher, force run, start menu, desktop, system tray, quick launch, force cache.
	/// 
	/// The values are a and'ed valuation.
	/// </summary>
	public enum ZFDNALFlags
	{
		/// <summary>
		/// value of 1
		/// </summary>
		APPLAUNCHER = 1	,
		/// <summary>
		/// value of 2
		/// </summary>
		FORCERUN = 2,
		/// <summary>
		/// value of 4
		/// </summary>
		STARTMENU = 4,
		/// <summary>
		/// value of 8
		/// </summary>
		DESKTOP = 8,
		/// <summary>
		/// value of 32
		/// </summary>
		SYSTRAY = 32,
		/// <summary>
		/// value of 128
		/// </summary>
		QUICKLAUNCH = 128,
		/// <summary>
		/// value of 256
		/// </summary>
		FORCECACHE = 256
	}
}
