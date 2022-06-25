using Spectre.Console.Cli;
using System.Reflection;

/// <summary>
/// The Console namespace.
/// </summary>
namespace Spectre.Console;

/// <summary>
/// Class ConsoleExtensionsEx.
/// </summary>
public static class ConsoleExtensionsEx
{
	/// <summary>
	/// Prints the entry assembly version.
	/// </summary>
	/// <param name="color">The color.</param>
	public static void PrintEntryAssemblyVersion(string color = "orange3")
	{
		AnsiConsole.MarkupLine($" [{color} italic]Version: {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion}[/]\n\n");
	}

	/// <summary>
	/// Gets the value or default.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value">The value.</param>
	/// <param name="defaultValue">The default value.</param>
	/// <returns>T.</returns>
	public static T GetValueOrDefault<T>(this FlagValue<T> value, T defaultValue = default)
	{
		return value.IsSet ? value.Value : defaultValue;
	}

	/// <summary>
	/// Sets the refresh rate.
	/// </summary>
	/// <param name="progres">The console.</param>
	/// <param name="ts">The ts.</param>
	/// <returns>Spectre.Console.Progress?.</returns>
	public static Progress? RefreshRate(this Progress? progres, TimeSpan ts)
	{
		if (progres is not null)
		{
			progres.RefreshRate = ts;
		}
		return progres;
	}

	/// <summary>
	/// Sets the value.
	/// </summary>
	/// <param name="progress">The console.</param>
	/// <param name="value">The value.</param>
	public static void SetValue(this ProgressTask progress, double value)
	{
		if (progress is not null)
		{
			progress.Value = value;
		}
	}
}