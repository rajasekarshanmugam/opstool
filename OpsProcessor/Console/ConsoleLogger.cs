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
	/// Gets or sets the silent.
	/// </summary>
	/// <value>The silent.</value>
	public static bool Silent { get; set; }

	/// <summary>
	/// Escape string markups..
	/// </summary>
	/// <param name="message">The message.</param>
	/// <returns>string?.</returns>
	public static string? EscapeMarkup(string? message)
	{
		return StringExtensions.EscapeMarkup(message);
	}

	/// <summary>
	/// Writes the raw message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteRawMessage(string message, string? emoji = null)
	{
		if (Silent)
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
	public static void WriteRawLogMessage(string message, string? emoji = null)
	{
		if (Silent)
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
	public static void WriteLogMessage(string message, string? emoji = null)
	{
		if (Silent)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:gear:[/]";
		AnsiConsole.MarkupLine($" {emoji} {message.EscapeMarkup()} [grey]...[/]");
	}

	/// <summary>
	/// Writes the success message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteSuccessMessage(string message, string? emoji = null)
	{
		if (Silent)
		{
			return;
		}

		emoji ??= "[bold slowblink]:check_mark_button:[/]";
		AnsiConsole.MarkupLine($" {emoji} [green]LOG:[/] {message.EscapeMarkup()} [grey]...[/]");
	}

	/// <summary>
	/// Writes the error message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteErrorMessage(string message, string? emoji = null)
	{
		Console.Error.WriteLine($"ERROR: an error occurred...\nMESSAGE: {message}");
		if (Silent)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:red_circle:[/]";
		AnsiConsole.MarkupLine($" {emoji} [red]ERROR:[/] {message.EscapeMarkup()} [grey]...[/]");
	}

	/// <summary>
	/// Writes the warning message.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteWarningMessage(string message, string? emoji = null)
	{
		if (Silent)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:warning:[/]";
		AnsiConsole.MarkupLine($" {emoji} [orange1]WARNING:[/] {message.EscapeMarkup()} [grey]...[/]");
	}

	/// <summary>
	/// Writes the error message.
	/// </summary>
	/// <param name="ex">The ex.</param>
	/// <param name="message">The message.</param>
	/// <param name="emoji">The emoji.</param>
	public static void WriteExceptionMessage(Exception ex, string message, string? emoji = null)
	{
		Console.Error.WriteLine($"EXCEPTION: an exception occurred in processing...\nMESSAGE: {ex.Message}\nSTACKTRACE: {ex.StackTrace}");

		if (Silent)
		{
			return;
		}

		emoji ??= "[bold rapidblink]:red_circle:[/]";
		AnsiConsole.MarkupLine($" {emoji} [red]ERROR:[/] {message.EscapeMarkup()} [grey]...[/]");
		AnsiConsole.WriteException(ex);
	}
}