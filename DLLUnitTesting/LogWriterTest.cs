using System.Diagnostics;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DLLUnitTesting {
	[TestClass]
	public class LogWriterTest {

		/// <summary>
		/// This unit test tests the ClassLogger
		/// </summary>
		[TestMethod]
		public void LogWriteLineTest1() {
			ClassLogger logger = new ClassLogger( "LogWriterTest" );
			logger.WriteLine( "This message is a line test." );

			for( int i = 0; i < ushort.MaxValue / 2; ++i ) {
				logger.WriteLine( $"This is test message #{i}." );
			}
		}

		[TestMethod]
		public void LogWriteTest1() {
			ClassLogger logger = new ClassLogger( "LogWriterTest" );

			logger.Write( "This message is a test.\n" );

			for( int i = 0; i < ushort.MaxValue / 2; ++i ) {
				logger.Write( $"This is line test message #{i}.\n" );
			}
		}

		[TestMethod]
		public void LogWriteObjectTest1() {
			ClassLogger logger = new ClassLogger( "LogWriterTest" );

			logger.Write( this );
			logger.WriteLine( this );

			logger.Write( logger );
			logger.WriteLine( logger );
		}
	}
}
