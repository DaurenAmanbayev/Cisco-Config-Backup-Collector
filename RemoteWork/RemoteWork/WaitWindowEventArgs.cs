using System;
using System.Collections.Generic;

namespace RemoteWork
{
	/// <summary>
	/// Provides data for the WaitWindow events.
	/// </summary>
	public class WaitWindowEventArgs : EventArgs
	{

		/// <summary>
		/// Initialises a new intance of the WaitWindowEventArgs class.
		/// </summary>
		/// <param name="GUI">The associated WaitWindow instance.</param>
		/// <param name="args">A list of arguments to be passed.</param>
		public WaitWindowEventArgs(WaitWindow GUI, List<object> args) : base() {
			this._Window = GUI;
			this._Arguments = args;
		}

		private WaitWindow _Window;
		private List<object> _Arguments;
		private object _Result;
		
		public WaitWindow Window {
			get { return _Window; }
		}
		
		public List<object> Arguments {
			get { return _Arguments; }
		}

		public object Result {
			get { return _Result; }
			set { _Result = value; }
		}
	}
}
