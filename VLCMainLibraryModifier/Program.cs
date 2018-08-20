using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace VLCMainLibraryModifier {
	public static class Program {
		private static string mlFile = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ); // Creates the jump off point for finding the VLC ml.xspf
		private static bool verbose; // Verbose will print things to the console.

		public static void Main( string[] args ) {
			string[] lines = null;

			CheckArgs( args );
			GetMediaLibraryFile();

			lines = File.ReadAllLines( mlFile );

			CleanMediaLibrary( ref lines );
			WriteToFile( lines );

#if DEBUG
			Console.ReadKey();
#endif
		}

		private static void CheckArgs( string[] args ) {
			foreach( string arg in args ) {
				if( arg == "-v" || arg == "--verbose" ) {
					verbose = true;
				}
			}
		}

		private static void CleanMediaLibrary( ref string[] lines ) {
			List<string> newArray = new List<string>();

			for( int i = 0; i < lines.Length; ++i ) {
				if( verbose ) {
					Console.WriteLine( $"Checking if line {i + 1} is an image. Line says, '{lines[ i ]}'." );
				}

				if( !lines[ i ].Trim().ToLower().StartsWith( "<image>" ) ) {
					if( verbose ) {
						Console.WriteLine( "Line isn't an image line, keeping." );
					}

					newArray.Add( lines[ i ] );
				} else if(verbose) {
					Console.WriteLine( "Line is an image, removing." );
				}
			}

			lines = newArray.ToArray();
		}

		private static void WriteToFile( string[] lines ) {
			Stream writer = File.Create( mlFile );
			Encoding encoding = Encoding.UTF8;

			if( verbose ) {
				Console.WriteLine( $"Rewriting file." );
			}

			foreach( string line in lines ) {
				byte[] bytes = encoding.GetBytes( $"{line}\n" );

				if( verbose ) {
					Console.WriteLine( $"Writing {line} to file.\nRaw view: {OutputByteArray( bytes )}" );
				}

				writer.Write( bytes, 0, bytes.Length );
			}

			writer.Flush();
			writer.Close();
		}

		/// <summary>
		/// Get's the media file location.
		/// </summary>
		private static void GetMediaLibraryFile() {
			// Checks to see if platform is Windows, macOS/Mac OS X, or Linux.
			if( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ) {
				if( verbose ) {
					Console.WriteLine( "Windows detected." ); // UwU As expected, Windows.
				}

				mlFile = Path.Combine( mlFile, "AppData", "Roaming", "vlc" ); // Adds the folders to the path.
			} else if( RuntimeInformation.IsOSPlatform( OSPlatform.OSX ) ) {
				if( verbose ) {
					Console.WriteLine( "shitOS detected." ); // Seriously, macOS is shit and I do not understand why people would use it. Windows and Linux are better. -w-
				}

				mlFile = Path.Combine( mlFile, "Library", "Application Support", "org.videolan.vlc" ); // Adds the folders to the path.
			} else if( RuntimeInformation.IsOSPlatform( OSPlatform.Linux ) ) {
				if( verbose ) {
					Console.WriteLine( "Linux detected." ); // OwO Hewwo fewwow Winux Usew.
				}

				mlFile = Path.Combine( mlFile, ".local", "share", "vlc" ); // Adds the folders to the path.
			} else {
				Console.WriteLine( "OwO You should not be seeing this message. But knowing how you are, you ran this program on an unkown/underterminable system. An exception will be thrown and you'll see a stacktraces. Just be aware that it is caused because this program is running on an unkwon system." );
				Thread.Sleep( 500 );

				throw new Exception( "Unkown Operating System." ); // How the fuck did you manage to get this code running on an unkown system?
			}

			mlFile = Path.Combine( mlFile, "ml.xspf" ); // Adds the file to the path.
		}

		private static string OutputByteArray( byte[] bytes ) {
			StringBuilder builder = new StringBuilder();

			foreach( byte @byte in bytes ) {
				builder.Append( $"0x{@byte.ToString( "X" )} " );
			}

			return builder.ToString();
		}
	}
}