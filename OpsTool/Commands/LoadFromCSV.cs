/// <summary>
/// The OpsTool namespace.
/// </summary>
namespace OpsTool;

using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;

/// <summary>
/// </summary>
[Description("loads the reference database with data from a csv file")]
public partial class LoadFromCSVCommand : Command<LoadFromCSVCommand.Settings>
{
	/// <summary>
	///
	/// </summary>
	/// <seealso cref="BaseSettings" />
	public partial class Settings : BaseSettings
	{
		/// <summary>
		/// Gets or sets the database.
		/// </summary>
		/// <value>The database.</value>
		[Description("path to the reference database file")]
		[CommandOption("-d|--database <DATABASE>")]
		public string? Database { get; init; }

		/// <summary>
		/// Gets or sets the input CSV.
		/// </summary>
		/// <value>The input CSV.</value>
		[Description("path to the input CSV file")]
		[CommandOption("-i|--inputcsv <INPUTCSVFILE>")]
		public string? InputCSV { get; init; }
	}

	/// <summary>
	/// Validates the specified context.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="settings">The settings.</param>
	/// <returns>The validation result.</returns>
	public override ValidationResult Validate([NotNull] CommandContext context, [NotNull] LoadFromCSVCommand.Settings settings)
	{
		if (settings.Database.IsNullOrEmpty())
		{
			return ValidationResult.Error("Required argument 'Database' has not been specified. Use --help to see possible options.");
		}
		if (settings.InputCSV.IsNullOrEmpty())
		{
			return ValidationResult.Error("Required argument 'InputCSV' has not been specified. Use --help to see possible options.");
		}

		return ValidationResult.Success();
	}

	/// <summary>
	/// Executes the command.
	/// </summary>
	/// <param name="context">The command context.</param>
	/// <param name="settings">The settings.</param>
	/// <returns>
	/// An integer indicating whether or not the command executed successfully.
	/// </returns>
	/// <exception cref="System.Exception">input csv file does not exist - File={inputcsv}</exception>
	public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
	{
		var returnValue = -1;
		try
		{
			var (inputcsv, database) = (settings.InputCSV, settings.Database);
			if (!File.Exists(inputcsv))
			{
				throw new Exception($"input csv file does not exist - File={inputcsv}");
			}

			inputcsv = Path.GetFullPath(inputcsv);
			database = Path.GetFullPath(database);

			AnsiConsole.Progress()
			.AutoClear(false)
			.Columns(new ProgressColumn[]
			{
					new TaskDescriptionColumn(),
					new ProgressBarColumn(),
					new PercentageColumn(),
					new RemainingTimeColumn(),
					new SpinnerColumn(),
			})
			.StartAsync(async ctx =>
			{
			});
			return 0;
		}
		catch (AggregateException ae)
		{
			ae.Handle((hre) =>
			{
				AnsiConsole.WriteException(hre);
				return true;
			});
		}
		catch (Exception e)
		{
			AnsiConsole.WriteException(e);
		}
		return returnValue;
	}
}
