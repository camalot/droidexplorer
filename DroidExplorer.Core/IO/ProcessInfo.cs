using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	public class ProcessInfo {
		internal ProcessInfo ( int id, string name, int ppid, long size, string user ) {
			this.PID = id;
			this.PPID = ppid;
			this.Size = size;
			this.User = user;
			this.Name = name;
		}

    public ProcessInfo ( int id, string name, int thread, long vss, long rss, string user, int cpu ) {
      this.PID = id;
      this.User = user;
      this.Name = name;
      this.Thread = thread;
      this.Vss = vss;
      this.Rss = rss;
      this.Cpu = cpu;
    }

    public int Thread { get; set; }
    public long Vss { get; set; }
    public long  Rss { get; set; }
    public int Cpu { get; set; }
    public string Name { get; set; }
    public string User { get; set; }
		public long Size{ get; set; }
		public int PPID{ get; set; }	
		public int PID { get; set; }
	}
}
