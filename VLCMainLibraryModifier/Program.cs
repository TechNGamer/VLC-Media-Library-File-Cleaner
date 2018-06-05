using System;
using System.IO;
using System.Runtime.InteropServices;

namespace VLCMainLibraryModifier {
	public static class Program {
		private static string mlFile = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile );

		public static void Main( string[] args ) {
			GetMediaLibraryFile();
		}

		private static void GetMediaLibraryFile() {
			if( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ) {
				mlFile = Path.Combine( mlFile, "AppData", "Roaming", "vlc" );
			} else if( RuntimeInformation.IsOSPlatform( OSPlatform.OSX ) ) {
				
			} else if( RuntimeInformation.IsOSPlatform( OSPlatform.Linux ) ) {
				mlFile = Path.Combine( mlFile, ".local", "share", "vlc" );
			} else {
				throw new Exception( "Unkown Operating System." );
			}

			mlFile = Path.Combine( mlFile, "ml.xspf" );
		}


	}
}