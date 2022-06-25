using Spectre.Console;
using Spectre.Console.Cli;

/// <summary>
/// Class BaseSyncCommand.
/// </summary>
/// <typeparam name="TService">The type of the tservice.</typeparam>
/// <typeparam name="TSettings">The type of the tsettings.</typeparam>
public abstract partial class BaseSyncCommand<TService, TSettings> : Command<TSettings>
	where TSettings : BaseSettings
{
	/// <summary>
	/// The ops service
	/// </summary>
	readonly protected TService _opsService;

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseCommand{T}" /> class.
	/// </summary>
	/// <param name="opsService">The ops service.</param>
	protected BaseSyncCommand(TService opsService)
	{
		_opsService = opsService;
	}

	/// <summary>
	/// Writes the exception.
	/// </summary>
	/// <param name="ex">The ex.</param>
	protected void WriteException(Exception ex)
	{
		AnsiConsole.WriteException(ex);
	}
}