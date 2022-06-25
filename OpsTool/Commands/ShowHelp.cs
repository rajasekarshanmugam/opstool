/// <summary>
/// The OpsTool namespace.
/// </summary>
namespace OpsTool;

using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Class HelpCommand.
/// </summary>
[Description("opens the help in the browser")]
public sealed partial class ShowHelpCommand : BaseAsyncCommand<ShowHelpCommand.Settings>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ShowHelpCommand" /> class.
	/// </summary>
	public ShowHelpCommand()
	{
	}

	/// <summary>
	/// Class Settings.
	/// </summary>
	public sealed partial class Settings : BaseSettings
	{
	}

	/// <summary>
	/// Execute command as an asynchronous operation.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="settings">The settings.</param>
	/// <returns>A Task&lt;System.Int32&gt; representing the asynchronous operation.</returns>
	public async override Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] Settings settings)
	{
		Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "Help", "index.html")
			.OpenFileUsingShell();
		return 0;
	}
}