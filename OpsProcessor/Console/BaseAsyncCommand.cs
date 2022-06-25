using Spectre.Console;
using Spectre.Console.Cli;

/// <summary>
/// Class BaseSettings.
/// Implements the <see cref="Spectre.Console.Cli.CommandSettings" />
/// </summary>
/// <seealso cref="Spectre.Console.Cli.CommandSettings" />
public partial class BaseSettings : CommandSettings
{
}

/// <summary>
/// Class BaseAsyncCommand.
/// </summary>
/// <typeparam name="TSettings">The type of the t settings.</typeparam>
public abstract partial class BaseAsyncCommand<TSettings> : AsyncCommand<TSettings>
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

/// <summary>
/// Class BaseAsyncCommand.
/// </summary>
/// <typeparam name="TService">The type of the t service.</typeparam>
/// <typeparam name="TSettings">The type of the t settings.</typeparam>
public abstract partial class BaseAsyncCommand<TService, TSettings> : AsyncCommand<TSettings>
	where TSettings : BaseSettings
{
	/// <summary>
	/// The ops service
	/// </summary>
	readonly protected TService _opsService;

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseAsyncCommand{T}" /> class.
	/// </summary>
	/// <param name="opsService">The ops service.</param>
	protected BaseAsyncCommand(TService opsService)
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