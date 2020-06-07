/*
 * Copyright (c) 2020 Fabio Iotti
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 * Copyright 1987, 1998  The Open Group
 * 
 * Permission to use, copy, modify, distribute, and sell this software and its
 * documentation for any purpose is hereby granted without fee, provided that
 * the above copyright notice appear in all copies and that both that
 * copyright notice and this permission notice appear in supporting
 * documentation.
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
 * OPEN GROUP BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * Except as contained in this notice, the name of The Open Group shall not be
 * used in advertising or otherwise to promote the sale, use or other dealings
 * in this Software without prior written authorization from The Open Group.
 * 
 * 
 * Copyright 1987 by Digital Equipment Corporation, Maynard, Massachusetts.
 * 
 *                         All Rights Reserved
 * 
 * Permission to use, copy, modify, and distribute this software and its 
 * documentation for any purpose and without fee is hereby granted, 
 * provided that the above copyright notice appear in all copies and that
 * both that copyright notice and this permission notice appear in 
 * supporting documentation, and that the name of Digital not be
 * used in advertising or publicity pertaining to distribution of the
 * software without specific, written prior permission.  
 * 
 * DIGITAL DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE, INCLUDING
 * ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS, IN NO EVENT SHALL
 * DIGITAL BE LIABLE FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR
 * ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS,
 * WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION,
 * ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS
 * SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;

namespace X11
{
	public static unsafe class X
	{
		[StructLayout(LayoutKind.Sequential)]
		public readonly struct Atom
		{
			public static Atom None => default;

			readonly ulong id;

			Atom(ulong id)
			{
				this.id = id;
			}

			public override bool Equals(object obj)
				=> obj is Atom other && this == other;

			public override int GetHashCode()
				=> id.GetHashCode();

			public static implicit operator ulong(Atom atom)
				=> atom.id;

			public static implicit operator Atom(ulong id)
				=> new Atom(id);

			public static bool operator ==(Atom a, Atom b)
				=> a.id == b.id;

			public static bool operator !=(Atom a, Atom b)
				=> !(a == b);
		}

		[StructLayout(LayoutKind.Sequential)]
		internal readonly struct Time
		{
			readonly ulong val;

			Time(ulong val)
			{
				this.val = val;
			}

			public static implicit operator ulong(Time time)
				=> time.val;

			public static implicit operator Time(ulong val)
				=> new Time(val);

			public static implicit operator TimeSpan(Time time)
				=> TimeSpan.FromTicks((long)time.val * TimeSpan.TicksPerMillisecond);

			public static implicit operator Time(TimeSpan val)
				=> new Time((ulong)(val.Ticks / TimeSpan.TicksPerMillisecond));
		}

		[StructLayout(LayoutKind.Sequential)]
		public readonly struct KeySym
		{
			public static KeySym None => default;

			readonly ulong id;

			KeySym(ulong id)
			{
				this.id = id;
			}

			public override bool Equals(object obj)
				=> obj is KeySym other && this == other;

			public override int GetHashCode()
				=> id.GetHashCode();

			public static implicit operator ulong(KeySym keySym)
				=> keySym.id;

			public static implicit operator KeySym(ulong id)
				=> new KeySym(id);

			public static bool operator ==(KeySym a, KeySym b)
				=> a.id == b.id;

			public static bool operator !=(KeySym a, KeySym b)
				=> !(a == b);
		}

		public enum KeyCode : uint  // TODO: uint or byte?
		{
			
		}

		[StructLayout(LayoutKind.Sequential)]
		public readonly struct Window
		{
			public static Window None => default;

			readonly ulong id;

			Window(ulong id)
			{
				this.id = id;
			}

			public override bool Equals(object obj)
				=> obj is Window other && this == other;

			public override int GetHashCode()
				=> id.GetHashCode();

			public static implicit operator ulong(Window window)
				=> window.id;

			public static implicit operator Window(ulong id)
				=> new Window(id);

			public static bool operator ==(Window a, Window b)
				=> a.id == b.id;

			public static bool operator !=(Window a, Window b)
				=> !(a == b);
		}

		/// <summary>
		/// Input Event Masks.
		/// Used as event-mask window attribute and as arguments to Grab requests.
		/// </summary>
		[Flags]
		public enum EventMask : long
		{
			None = 0,
			KeyPress = 1 << 0,
			KeyRelease = 1 << 1,
			ButtonPress = 1 << 2,
			ButtonRelease = 1 << 3,
			EnterWindow = 1 << 4,
			LeaveWindow = 1 << 5,
			PointerMotion = 1 << 6,
			PointerMotionHint = 1 << 7,
			Button1Motion = 1 << 8,
			Button2Motion = 1 << 9,
			Button3Motion = 1 << 10,
			Button4Motion = 1 << 11,
			Button5Motion = 1 << 12,
			ButtonMotion = 1 << 13,
			KeymapState = 1 << 14,
			Exposure = 1 << 15,
			VisibilityChange = 1 << 16,
			StructureNotify = 1 << 17,
			ResizeRedirect = 1 << 18,
			SubstructureNotify = 1 << 19,
			SubstructureRedirect = 1 << 20,
			FocusChange = 1 << 21,
			PropertyChange = 1 << 22,
			ColormapChange = 1 << 23,
			OwnerGrabButton = 1 << 24,
		}
	
		/// <summary>
		/// Event names.
		/// They start from 2 because 0 and 1 are reserved in the protocol for errors and replies.
		/// </summary>
		public enum EventType : int
		{
			KeyPress = 2,
			KeyRelease = 3,
			ButtonPress = 4,
			ButtonRelease = 5,
			MotionNotify = 6,
			EnterNotify = 7,
			LeaveNotify = 8,
			FocusIn = 9,
			FocusOut = 10,
			KeymapNotify = 11,
			Expose = 12,
			GraphicsExpose = 13,
			NoExpose = 14,
			VisibilityNotify = 15,
			CreateNotify = 16,
			DestroyNotify = 17,
			UnmapNotify = 18,
			MapNotify = 19,
			MapRequest = 20,
			ReparentNotify = 21,
			ConfigureNotify = 22,
			ConfigureRequest = 23,
			GravityNotify = 24,
			ResizeRequest = 25,
			CirculateNotify = 26,
			CirculateRequest = 27,
			PropertyNotify = 28,
			SelectionClear = 29,
			SelectionRequest = 30,
			SelectionNotify = 31,
			ColormapNotify = 32,
			ClientMessage = 33,
			MappingNotify = 34,
			Generic = 35,
		}
	
		/// <summary>
		/// Key masks.
		/// Used as modifiers to GrabButton and GrabKey, results of QueryPointer,
		/// state in various key-, mouse-, and button-related events.
		/// </summary>
		[Flags]
		public enum KeyMask : uint
		{
			None = 0,
			Shift = 1 << 0,
			Lock = 1 << 1,
			Control = 1 << 2,
			Mod1 = 1 << 3,
			Mod2 = 1 << 4,
			Mod3 = 1 << 5,
			Mod4 = 1 << 6,
			Mod5 = 1 << 7,
		}
	}
}
