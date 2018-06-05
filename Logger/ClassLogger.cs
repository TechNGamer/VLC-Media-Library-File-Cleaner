using System;

using Logger.Singleton;

namespace Logger {
	public class ClassLogger {

		public string ClassName {
			get;
		}

		public string LogDirectory {
			get => manager.LogDirectory;
		}

		public bool PrintLog {
			get;
			set;
		}

		private LogManager manager;

		public ClassLogger( string className ) : this( className, false ) {}

		public ClassLogger( string className, bool printLog ) {
			ClassName = className;
			PrintLog = printLog;

			manager = LogManager.Singleton;
		}

		#region Write methods

		public void Write( object message, LogType logType = LogType.NormalLog ) {
			Write( message.ToString(), logType );
		}

		public void Write( string message, LogType logType = LogType.NormalLog ) {
			manager.WriteLog( message, logType );

			if( PrintLog ) {
				Console.Write( message );
			}
		}

		public void Write( byte message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( sbyte message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( short message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( ushort message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( int message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( uint message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( long message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( ulong message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( float message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( double message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		public void Write( decimal message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}", logType );
		}

		#endregion

		#region WriteLine Methods

		public void WriteLine( object message, LogType logType = LogType.NormalLog ) {
			WriteLine( message.ToString(), logType );
		}

		public void WriteLine( string message, LogType logType = LogType.NormalLog ) {
			Write( message + "\n", logType );
		}

		public void WriteLine( byte message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( sbyte message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( short message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( ushort message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( int message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( uint message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( long message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( ulong message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( float message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( double message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		public void WriteLine( decimal message, LogType logType = LogType.NormalLog ) {
			Write( $"{message}\n", logType );
		}

		#endregion

		public override string ToString() {
			return $"{base.ToString()} {{\n\tClassName {{\n\t\t{ClassName}\n\t}}\n\tLogDirectory {{\n\t\t{LogDirectory}\n\t}}\n\tPrintLog {{\n\t\t{PrintLog}\n\t}}\n}}\n";
		}
	}
}