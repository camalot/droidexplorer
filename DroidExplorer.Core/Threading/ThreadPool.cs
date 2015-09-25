using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using log4net;
using System.Reflection;

namespace DroidExplorer.Core.Threading {
	/// <summary>
	/// 
	/// </summary>
	public delegate void ThreadPoolRunDelegate ( object arg );
	/// <summary>
	/// 
	/// </summary>
	public class ThreadPool {
		/// <summary>
		/// 
		/// </summary>
		protected static readonly ILog Log = LogManager.GetLogger ( MethodBase.GetCurrentMethod ( ).DeclaringType );


		private int _concurrentThreads = 5;
		private Queue<ThreadPoolRunDelegate> _threads;
		private Queue<object> _arguments;
		private List<Thread> _runningThreads;
		private int threadCount = 0;
		private bool running = false;
		private string _poolName = string.Empty;


		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadPool"/> class.
		/// </summary>
		public ThreadPool ( )
			: this ( "UNNAMED", 5 ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadPool"/> class.
		/// </summary>
		/// <param name="concurrentThreads">The concurrent threads.</param>
		public ThreadPool ( int concurrentThreads )
			: this ( "UNNAMED", concurrentThreads ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadPool"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public ThreadPool ( string name )
			: this ( name, 5 ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadPool"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="concurrentThreads">The concurrent threads.</param>
		public ThreadPool ( string name, int concurrentThreads ) {
			this.ConcurrentThreads = concurrentThreads;
			RunningThreads = new List<Thread> ( );
			Threads = new Queue<ThreadPoolRunDelegate> ( );
			this.Arguments = new Queue<object> ( );
			this.Name = name;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get { return this._poolName; }
			private set { this._poolName = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ThreadPool"/> is running.
		/// </summary>
		/// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
		public bool Running {
			get { return this.running; }
			private set { this.running = value; }
		}

		/// <summary>
		/// Gets or sets the running threads.
		/// </summary>
		/// <value>The running threads.</value>
		private List<Thread> RunningThreads {
			get { return _runningThreads; }
			set { _runningThreads = value; }
		}

		/// <summary>
		/// Gets or sets the arguments.
		/// </summary>
		/// <value>The arguments.</value>
		private Queue<object> Arguments {
			get { return this._arguments; }
			set { this._arguments = value; }
		}

		/// <summary>
		/// Gets or sets the threads.
		/// </summary>
		/// <value>The threads.</value>
		private Queue<ThreadPoolRunDelegate> Threads {
			get { return _threads; }
			set { _threads = value; }
		}

		/// <summary>
		/// Gets or sets the concurrent threads.
		/// </summary>
		/// <value>The concurrent threads.</value>
		public int ConcurrentThreads {
			get { return _concurrentThreads; }
			private set { _concurrentThreads = value; }
		}

		/// <summary>
		/// Queues the specified run callback.
		/// </summary>
		/// <param name="runCallback">The run callback.</param>
		public void Queue ( ThreadPoolRunDelegate runCallback ) {
			Queue ( runCallback, new object ( ) );
		}

		/// <summary>
		/// Queues the specified run callback.
		/// </summary>
		/// <param name="runCallback">The run callback.</param>
		/// <param name="obj">The obj.</param>
		public void Queue ( ThreadPoolRunDelegate runCallback, object obj ) {
			Queue<object> ( runCallback, obj );
		}

		/// <summary>
		/// Queues the specified run callback.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="runCallback">The run callback.</param>
		/// <param name="arg">The arg.</param>
		public void Queue<T> ( ThreadPoolRunDelegate runCallback, T arg ) {
			this.Threads.Enqueue ( runCallback );
			this.Arguments.Enqueue ( arg );
		}

		public void Start ( ) {
			if ( !Running ) {
				if ( Threads.Count == 0 ) {
					Running = false;
					return;
				}
				Running = true;
				Log.Debug ( string.Format ( "[{0}] Starting Thread Pool", this.Name ) );
				for ( int i = 0; i < Threads.Count && i < this.ConcurrentThreads; i++ ) {
					ThreadRun ( );
				}
			} else {
				Log.Debug ( string.Format ( "[{0}] Tried to start a thread pool that is already running", this.Name ) );
			}
		}

		/// <summary>
		/// Threads the run.
		/// </summary>
		private void ThreadRun ( ) {
			if ( threadCount < this.ConcurrentThreads && Running && Threads.Count > 0 && Arguments.Count > 0 ) {
				Thread t = new Thread ( new ThreadStart ( delegate {
					--threadCount;
					if ( Threads.Count > 0 && Arguments.Count > 0 ) {
						try {
							TimeSpan ttss = DateTime.Now.TimeOfDay;
							Threads.Dequeue ( ) ( Arguments.Dequeue ( ) );
							TimeSpan ttse = DateTime.Now.TimeOfDay;
							TimeSpan tdiff = ttse.Subtract ( ttss );
							Log.Debug ( string.Format ( "[{0}] Thread time: {1}", this.Name, tdiff ) );
							// sometimes the same thread starts multiple times.
							ThreadRun ( );
						} catch ( Exception ex ) {
							--threadCount;
							Log.Warn ( string.Format ( "[{0}] {1}", this.Name, ex.Message ), ex );
						} finally {

						}
					}
				} ) );
				Log.Debug ( string.Format ( "[{0}] Starting Thread: {1}", this.Name, ++threadCount ) );
				RunningThreads.Add ( t );
				t.Start ( );
			} else {
				if ( Threads.Count == 0 && threadCount <= 0 ) {
					Running = false;
				}
			}

		}

		/// <summary>
		/// Stops the specified abort.
		/// </summary>
		/// <param name="abort">if set to <c>true</c> [abort].</param>
		public void Stop ( bool abort ) {
			if ( abort ) {
				foreach ( Thread t in RunningThreads ) {
					if ( t != null && t.IsAlive ) {
						t.Abort ( );
					}
				}
			}
			Running = false;
		}

	}
}
