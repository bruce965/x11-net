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
 * Copyright 1985, 1986, 1987, 1991, 1998  The Open Group
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
 */

using System;
using System.Runtime.InteropServices;

using static X11.X;

namespace X11
{
	public static unsafe class Xlib
	{
		[StructLayout(LayoutKind.Sequential)]
		public readonly struct XPointer
		{
			readonly IntPtr ptr;

			XPointer(IntPtr ptr)
			{
				this.ptr = ptr;
			}

			public static implicit operator byte*(XPointer ptr)
				=> (byte*)ptr.ptr.ToPointer();

			public static implicit operator XPointer(byte* ptr)
				=> new XPointer(new IntPtr(ptr));

			public static implicit operator IntPtr(XPointer ptr)
				=> ptr.ptr;

			public static implicit operator XPointer(IntPtr ptr)
				=> new XPointer(ptr);
		}

		[StructLayout(LayoutKind.Sequential)]
		internal readonly struct Bool
		{
			readonly int val;

			Bool(int val)
			{
				this.val = val;
			}

			public static implicit operator bool(Bool val)
				=> val.val != 0;

			public static implicit operator Bool(bool val)
				=> new Bool(val ? 1 : 0);
		}

		public enum Status : int
		{
			Error = 0,
		}

		/// <summary>
		/// Pointer to display datatype maintaining display specific data.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public readonly struct Display
		{
			// NOTE: this is a pointer in the original Xlib.

			public static Display None => default;

			readonly IntPtr pointer;

			Display(IntPtr pointer)
			{
				this.pointer = pointer;
			}

			public override bool Equals(object obj)
				=> obj is Display other && this == other;

			public override int GetHashCode()
				=> pointer.GetHashCode();

			public static implicit operator IntPtr(Display display)
				=> display.pointer;

			public static implicit operator Display(IntPtr pointer)
				=> new Display(pointer);

			public static bool operator ==(Display a, Display b)
				=> a.pointer == b.pointer;

			public static bool operator !=(Display a, Display b)
				=> !(a == b);
		}


		[StructLayout(LayoutKind.Explicit)]
		public struct XEvent
		{
			[FieldOffset(0)] public EventType Type;
			// [FieldOffset(0)] public XAnyEvent xany;
			[FieldOffset(0)] public XKeyEvent Key;
			// [FieldOffset(0)] public XButtonEvent Button;
			// [FieldOffset(0)] public XMotionEvent Motion;
			// [FieldOffset(0)] public XCrossingEvent Crossing;
			// [FieldOffset(0)] public XFocusChangeEvent Focus;
			[FieldOffset(0)] public XExposeEvent Expose;
			// [FieldOffset(0)] public XGraphicsExposeEvent GraphicsExpose;
			// [FieldOffset(0)] public XNoExposeEvent NoExpose;
			// [FieldOffset(0)] public XVisibilityEvent Visibility;
			// [FieldOffset(0)] public XCreateWindowEvent CreateWindow;
			// [FieldOffset(0)] public XDestroyWindowEvent DestroyWindow;
			// [FieldOffset(0)] public XUnmapEvent Unmap;
			// [FieldOffset(0)] public XMapEvent Map;
			// [FieldOffset(0)] public XMapRequestEvent MapRequest;
			// [FieldOffset(0)] public XReparentEvent Reparent;
			// [FieldOffset(0)] public XConfigureEvent Configure;
			// [FieldOffset(0)] public XGravityEvent Gravity;
			// [FieldOffset(0)] public XResizeRequestEvent ResizeRequest;
			// [FieldOffset(0)] public XConfigureRequestEvent ConfigureRequest;
			// [FieldOffset(0)] public XCirculateEvent Circulate;
			// [FieldOffset(0)] public XCirculateRequestEvent CirculateRequest;
			// [FieldOffset(0)] public XPropertyEvent Property;
			// [FieldOffset(0)] public XSelectionClearEvent SelectionClear;
			// [FieldOffset(0)] public XSelectionRequestEvent SelectionRequest;
			// [FieldOffset(0)] public XSelectionEvent Selection;
			// [FieldOffset(0)] public XColormapEvent Colormap;
			[FieldOffset(0)] public XClientMessageEvent Client;
			// [FieldOffset(0)] public XMappingEvent Mapping;
			// [FieldOffset(0)] public XErrorEvent Error;
			// [FieldOffset(0)] public XKeymapEvent Keymap;
			// [FieldOffset(0)] public XGenericEvent Generic;
			// [FieldOffset(0)] public XGenericEventCookie Cookie;
			[FieldOffset(0)] internal fixed long Padding[24];
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XKeyEvent
		{
			public EventType Type;

			/// <summary>
			/// # of last request processed by server.
			/// </summary>
			public ulong Serial;
			
			Bool sendEvent;
			/// <summary>
			/// <c>true</c> if this came from a SendEvent request
			/// </summary>
			public bool SendEvent
			{
				get => sendEvent;
				set => sendEvent = value;
			}
			
			/// <summary>
			/// Display the event was read from.
			/// </summary>
			public Display Display;
			
			/// <summary>
			/// "event" window it is reported relative to.
			/// </summary>
			public Window Window;
			
			/// <summary>
			/// Root window that the event occurred on.
			/// </summary>
			public Window Root;
			
			/// <summary>
			/// Child window.
			/// </summary>
			public Window subwindow;
			
			Time time;
			/// <summary>
			/// Milliseconds.
			/// </summary>
			public TimeSpan Time  // TODO: TimeSpan or DateTime?
			{
				get => time;
				set => time = value;
			}
			
			public int X, Y;		/* pointer x, y coordinates in event window */
			
			public int XRoot, YRoot;	/* coordinates relative to root */
			
			/// <summary>
			/// Key or button mask.
			/// </summary>
			public KeyMask State;
			
			/// <summary>
			/// Detail.
			/// </summary>
			public KeyCode KeyCode;  // TODO: uint or byte?
			
			Bool sameScreen;
			/// <summary>
			/// Same screen flag.
			/// </summary>
			public bool SameScreen
			{
				get => sameScreen;
				set => sameScreen = value;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XExposeEvent
		{
			public EventType Type;

			/// <summary>
			/// # of last request processed by server.
			/// </summary>
			public ulong Serial;

			Bool sendEvent;
			/// <summary>
			/// <c>true</c> if this came from a SendEvent request.
			/// </summary>
			public bool SendEvent
			{
				get => sendEvent;
				set => sendEvent = value;
			}

			/// <summary>
			/// Display the event was read from.
			/// </summary>
			public Display Display;
			
			public Window Window;
			
			public int X, Y;
			
			public int Width, Height;
			
			/// <summary>
			/// If non-zero, at least this many more.
			/// </summary>
			public int Count;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XClientMessageEvent
		{

			[StructLayout(LayoutKind.Explicit)]
			public struct EventData
			{
				[StructLayout(LayoutKind.Sequential)]
				public struct AsBytes
				{
					fixed byte b[20];

					public byte this[int index]
					{
						get => b[index];
						set => b[index] = value;
					}
				}

				[StructLayout(LayoutKind.Sequential)]
				public struct AsShorts
				{
					fixed short s[10];

					public short this[int index]
					{
						get => s[index];
						set => s[index] = value;
					}
				}

				[StructLayout(LayoutKind.Sequential)]
				public struct AsInt32s
				{
					public IntPtr l0;
					public IntPtr l1;
					public IntPtr l2;
					public IntPtr l3;
					public IntPtr l4;

					public int this[int index]
					{
						get => index switch {
							0 => (int)l0,
							1 => (int)l1,
							2 => (int)l2,
							3 => (int)l3,
							4 => (int)l4,
							_ => throw new ArgumentOutOfRangeException(nameof(index))
						};
						set => _ = index switch {
							0 => l0 = new IntPtr(value),
							1 => l1 = new IntPtr(value),
							2 => l2 = new IntPtr(value),
							3 => l3 = new IntPtr(value),
							4 => l4 = new IntPtr(value),
							_ => throw new ArgumentOutOfRangeException(nameof(index))
						};
					}
				}
			
				[FieldOffset(0)] public AsBytes Byte;
				[FieldOffset(0)] public AsShorts Short;
				[FieldOffset(0)] public AsInt32s Int32;
			}

			public EventType Type;

			/// <summary>
			/// # of last request processed by server.
			/// </summary>
			public ulong Serial;

			Bool sendEvent;
			/// <summary>
			/// <c>true</c> if this came from a SendEvent request.
			/// </summary>
			public bool SendEvent
			{
				get => sendEvent;
				set => sendEvent = value;
			}

			/// <summary>
			/// Display the event was read from.
			/// </summary>
			public Display Display;
			
			public Window Window;

			public Atom MessageType;

			public int Format;

			public EventData Data;
		}

		[DllImport(DllNames.X11)]
		public static extern Display XOpenDisplay(string displayName);

		[DllImport(DllNames.X11)]
		public static extern Status XInitThreads();

		[DllImport(DllNames.X11)]
		public static extern Window XCreateSimpleWindow(Display display, Window parent, int x, int y, uint width, uint height, uint borderWidth, ulong border, ulong background);

		[DllImport(DllNames.X11)]
		public static extern ulong XBlackPixel(Display display, int screenNumber);

		[DllImport(DllNames.X11)]
		public static extern ulong XWhitePixel(Display display, int screenNumber);

		[DllImport(DllNames.X11)]
		public static extern Window XRootWindow(Display display, int screenNumber);

		#region XSetWMProtocols

		[DllImport(DllNames.X11)]
		public static extern Status XSetWMProtocols(Display display, Window window, Atom* protocols, int count);

		// // TODO: https://github.com/dotnet/csharplang/issues/1757
		// public static Status XSetWMProtocols(Display display, Window window, params ReadOnlySpan<Atom> protocols)
		// {
		// 	fixed (Atom* protocolsPtr = protocols)
		// 		return XSetWMProtocols(display, window, protocolsPtr, protocols.Length);
		// }

		public static Status XSetWMProtocols(Display display, Window window, Atom protocol1) {  // HACK: temporary until #1757 is implemented.
			Atom* protocolsPtr = stackalloc Atom[] { protocol1 };
			return XSetWMProtocols(display, window, protocolsPtr, 1);
		}

		public static Status XSetWMProtocols(Display display, Window window, Atom protocol1, Atom protocol2) {  // HACK: temporary until #1757 is implemented.
			Atom* protocolsPtr = stackalloc Atom[] { protocol1, protocol2 };
			return XSetWMProtocols(display, window, protocolsPtr, 2);
		}

		public static Status XSetWMProtocols(Display display, Window window, params Atom[] protocols) {  // HACK: temporary until #1757 is implemented.
			fixed (Atom* protocolsPtr = protocols)
				return XSetWMProtocols(display, window, protocolsPtr, protocols.Length);
		}

		#endregion
	
		[DllImport(DllNames.X11)]
		public static extern int XDefaultScreen(Display display);

		[DllImport(DllNames.X11)]
		public static extern int XMapWindow(Display display, Window window);

		[DllImport(DllNames.X11)]
		public static extern int XSelectInput(Display display, Window window, EventMask eventMask);

		[DllImport(DllNames.X11)]
		public static extern int XStoreName(Display display, Window window, string windowName);

		[DllImport(DllNames.X11)]
		public static extern Atom XInternAtom(Display display, string atomName, bool onlyIfExists);

		[DllImport(DllNames.X11)]
		public static extern int XPending(Display display);

		[DllImport(DllNames.X11)]
		public static extern int XNextEvent(Display display, out XEvent evt);

		[DllImport(DllNames.X11)]
		public static extern int XDestroyWindow(Display display, Window window);

		[DllImport(DllNames.X11)]
		public static extern int XCloseDisplay(Display display);
	}
}
