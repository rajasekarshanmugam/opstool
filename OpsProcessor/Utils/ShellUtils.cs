using System.Diagnostics;

/// <summary>
/// The System namespace.
/// </summary>
namespace System;

/// <summary>
/// Class ShellUtils.
/// </summary>
public static class ShellUtils
{
	/// <summary>
	/// Opens the file using shell.
	/// </summary>
	/// <param name="fileName">Name of the file.</param>
	public static void OpenFileUsingShell(this string fileName)
	{
		Process.Start(new ProcessStartInfo
		{
			FileName = fileName,
			UseShellExecute = true,
			Verb = "open"
		});
	}
}