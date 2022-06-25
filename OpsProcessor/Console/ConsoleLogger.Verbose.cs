using Spectre.Console;

/// <summary>
/// The System namespace.
/// </summary>
namespace System;

/// <summary>
/// Class ConsoleLogger.
/// </summary>
public static partial class ConsoleLogger
{
	/// <summary>
	/// Gets or sets the verbose flag that controls - Console.WriteVerbose*....
	/// </summary>
	/// <value>The silent.</value>
	public static bool Verbose { get; set; }

	/// <summary>
	/// Writes the raw message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteVerboseRawMessage(string message, string? emoji = null)
	{
		if (!Verbose)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:gear:[/]";
		AnsiConsole.MarkupLine($" {emoji} {message} [grey]...[/]");
	}

	/// <summary>
	/// Writes the raw log message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteVerboseRawLogMessage(string message, string? emoji = null)
	{
		if (!Verbose)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:gear:[/]";
		AnsiConsole.MarkupLine($" {emoji} {message} [grey]...[/]");
	}

	/// <summary>
	/// Writes the log message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteVerboseLogMessage(string message, string? emoji = null)
	{
		if (!Verbose)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:gear:[/]";
		AnsiConsole.MarkupLine($" {emoji} {message.EscapeMarkup()} [grey]...[/]");
	}
}