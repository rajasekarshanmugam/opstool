namespace OpsTool;

using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;

/// <summary>
///
/// </summary>
[Description("apply transformation rules on the reference data to generate output")]
public partial class TransformCommand : Command<TransformCommand.Settings>
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
		/// <value>
		/// The database.
		/// </value>
		[Description("path to the reference database file")]
		[CommandOption("-d|--database <DATABASE>")]
		public string? Database { get; init; }

		/// <summary>
		/// Gets or sets the input rules file.
		/// </summary>
		/// <value>
		/// The input rules file.
		/// </value>
		[Description("path to the input rules JSON file")]
		[CommandOption("-i|--inputrulesfile <INPUTRULESFILE>")]
		public string? InputRulesFile { get; init; }

		/// <summary>
		/// Gets or sets the output data file.
		/// </summary>
		/// <value>
		/// The output data file.
		/// </value>
		[Description("path to the output data file")]
		[CommandOption("-o|--outputdatafile <OUTPUTDATAFILE>")]
		public string? OutputDataFile { get; init; }
	}

	/// <summary>
	/// Validates the specified context.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="settings">The settings.</param>
	/// <returns></returns>
	public override ValidationResult Validate([NotNull] CommandContext context, [NotNull] TransformCommand.Settings settings)
	{
		if (settings.Database.IsNullOrEmpty())
		{
			return ValidationResult.Error("Required argument 'Database' has not been specified. Use --help to see possible options.");
		}
		if (settings.InputRulesFile.IsNullOrEmpty())
		{
			return ValidationResult.Error("Required argument 'InputRulesFile' has not been specified. Use --help to see possible options.");
		}
		if (settings.OutputDataFile.IsNullOrEmpty())
		{
			return ValidationResult.Error("Required argument 'OutputDataFile' has not been specified. Use --help to see possible options.");
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
			var (database, outputdatafile, inputrulesfile) = (settings.Database, settings.OutputDataFile, settings.InputRulesFile);
			if (!File.Exists(database))
			{
				throw new Exception($"reference database file does not exist - File={database}");
			}
			if (!File.Exists(inputrulesfile))
			{
				throw new Exception($"input rules file does not exist - File={database}");
			}

			inputrulesfile = Path.GetFullPath(inputrulesfile);
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
			{ });

			// Done
			AnsiConsole.MarkupLine("[green]Done![/]");
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

