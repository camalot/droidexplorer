using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	public class ProcessInfo {
		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessInfo"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="ppid">The ppid.</param>
		/// <param name="size">The size.</param>
		/// <param name="user">The user.</param>
		internal ProcessInfo ( int id, string name, int ppid, long size, string user ) {
			this.PID = id;
			this.PPID = ppid;
			this.Size = size;
			this.User = user;
			this.Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessInfo"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="thread">The thread.</param>
		/// <param name="vss">The VSS.</param>
		/// <param name="rss">The RSS.</param>
		/// <param name="user">The user.</param>
		/// <param name="cpu">The cpu.</param>
		public ProcessInfo ( int id, string name, int thread, long vss, long rss, string user, int cpu ) {
      this.PID = id;
      this.User = user;
      this.Name = name;
      this.Thread = thread;
      this.Vss = vss;
      this.Rss = rss;
      this.Cpu = cpu;
    }

		/// <summary>
		/// Gets or sets the thread.
		/// </summary>
		/// <value>
		/// The thread.
		/// </value>
		public int Thread { get; set; }
		/// <summary>
		/// </summary>
		/// <value>
		/// The VSS.
		/// </value>
		public long Vss { get; set; }
		/// <summary>
		/// </summary>
		/// <value>
		/// The RSS.
		/// </value>
		public long  Rss { get; set; }
		/// <summary>
		/// Gets or sets the cpu.
		/// </summary>
		/// <value>
		/// The cpu.
		/// </value>
		public int Cpu { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		public string User { get; set; }
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public long Size{ get; set; }
		/// <summary>
		/// Gets or sets the ppid.
		/// </summary>
		/// <value>
		/// The ppid.
		/// </value>
		public int PPID{ get; set; }
		/// <summary>
		/// Gets or sets the pid.
		/// </summary>
		/// <value>
		/// The pid.
		/// </value>
		public int PID { get; set; }
	}
}
