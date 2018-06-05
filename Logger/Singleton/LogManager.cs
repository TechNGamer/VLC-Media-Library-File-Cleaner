using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Logger.Singleton {

	public enum LogType {
		NormalLog, ErrorLog, CriticalLog
	}

	internal class LogManager {
		// Static fields

		public static LogManager Singleton {
			get {
				if( singleton is null ) {
					singleton = new LogManager();
				}

				return singleton;
			}
		}

		private static LogManager singleton;

		// End static fields
		// Non-static fields

		public string LogDirectory {
			get;
		}

		private FileStream normalLog, errorLog, criticalLog;
		private Queue<byte[]> normalLogQueue, errorLogQueue;
		private Thread writingThread;

		// End non-static fields

		private LogManager() {
			LogDirectory = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "KuroNeko Software", "VLC Main Library Cleaner" );

			if( !Directory.Exists( LogDirectory ) ) {
				Directory.CreateDirectory( LogDirectory );
			}

			normalLog = File.Create( Path.Combine( LogDirectory, "normalLog.txt" ) );
			errorLog = File.Create( Path.Combine( LogDirectory, "errorLog.txt" ) );
			criticalLog = File.Create( Path.Combine( LogDirectory, "criticalLog.txt" ) );
			normalLogQueue = new Queue<byte[]>();
			errorLogQueue = new Queue<byte[]>();
			writingThread = new Thread( () => WriteToLogFile() );

			writingThread.Start();
			writingThread.IsBackground = true;
			writingThread.Priority = ThreadPriority.Lowest;

			AppDomain.CurrentDomain.ProcessExit += AppExit;
		}

		public void WriteLog( byte[] message, LogType logType = LogType.NormalLog ) {
			switch( logType ) {
				case LogType.NormalLog:
					//if( normalLogQueue.Count == 0 ) {
					//	normalLog.BeginWrite( message, 0, message.Length, AsyncNormalWriteDone, message );
					//} else {
					//	normalLogQueue.Enqueue( message );
					//}
					lock( normalLogQueue ) {
						normalLogQueue.Enqueue( message );
					}
					break;
				case LogType.ErrorLog:
					//if( errorLogQueue.Count == 0 ) {
					//	errorLog.BeginWrite( message, 0, message.Length, AsyncErrorWriteDone, message );
					//}
					lock( errorLogQueue ) {
						errorLogQueue.Enqueue( message );
					}
					break;
				case LogType.CriticalLog:
				default:
					criticalLog.Write( message, 0, message.Length );
					criticalLog.Flush();
					break;
			}
		}

		public void WriteLog( string message, LogType logType = LogType.NormalLog ) {
			byte[] byteMessage = Encoding.UTF8.GetBytes( message );

			WriteLog( byteMessage, logType );
		}

		//private void AsyncNormalWriteDone( IAsyncResult ar ) {
		//	byte[] message;

		//	normalLog.Flush();

		//	if( normalLogQueue.Count > 0 ) {
		//		message = normalLogQueue.Dequeue();

		//		normalLog.BeginWrite( message, 0, message.Length, AsyncNormalWriteDone, message );
		//	}
		//}

		//private void AsyncErrorWriteDone( IAsyncResult ar ) {
		//	byte[] message;

		//	errorLog.Flush();

		//	if( errorLogQueue.Count > 0 ) {
		//		message = errorLogQueue.Dequeue();

		//		errorLog.BeginWrite( message, 0, message.Length, AsyncErrorWriteDone, message );
		//	}
		//}

		private void WriteToLogFile() {
			while( true ) {
				if( normalLogQueue.Count > 0 ) {
					byte[] message;

					lock( normalLogQueue ) {
						message = normalLogQueue.Dequeue();
					}

					lock( normalLog ) {
						normalLog.Write( message, 0, message.Length );
						normalLog.Flush();
					}
				}

				if( errorLogQueue.Count > 0 ) {
					byte[] message;

					lock( errorLogQueue ) {
						message = errorLogQueue.Dequeue();
					}

					lock( errorLog ) {
						errorLog.Write( message, 0, message.Length );
						errorLog.Flush();
					}
				}
			}
		}

		private void AppExit( object sender, EventArgs e ) {
			while(normalLogQueue.Count > 0 || errorLogQueue.Count > 0 ) {
				Console.WriteLine( $"Waiting for writing thread with TID: {writingThread.ManagedThreadId}." );
				Thread.Sleep( 250 );
			}

			writingThread.Abort();

			if( writingThread.IsAlive ) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine( $"The writing thread has appeared to have no stopped. The program is going to force stop the thread now!" );
				Console.ResetColor();
			}
		}
	}
}