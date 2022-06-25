using Spectre.Console;
using Spectre.Console.Cli;

/// <summary>
/// Class BaseCommand.
/// </summary>
/// <typeparam name="TSettings">The type of the tsettings.</typeparam>
public abstract partial class BaseCommand<TSettings> : Command<TSettings>
	where TSettings : BaseSettings
{
	/// <summary>
	/// Writes the exception.
	/// </summary>
	/// <param name="ex">The ex.</param>
	protected void WriteException(Exception ex)
	{
		AnsiConsole.WriteException(ex);
	}
}
