using Humanizer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpsTool;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics;
using System.Reflection;

var APP_NAME = "OPS";
var APP_PREFIX = "OPS";
var APP_CONFIGPREFIX = "--OPS:";

// console specific
Console.OutputEncoding = System.Text.Encoding.UTF8;

// TEXT LOGO
static bool ISLOGO(string a) => a == "/nologo" || a == "--nologo";
var noLogo = args.Any(ISLOGO);
if (!noLogo)
{
	AnsiConsole.Write(
		new FigletText(APP_NAME)
			.LeftAligned()
			.Color(Color.Red1));
	ConsoleExtensionsEx.PrintEntryAssemblyVersion("red1");
}
args = args.Where(a => !ISLOGO(a)).ToArray();

// excel reader specific
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

// bits folder
var INSTALLDIRECTORY = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var services = new ServiceCollection();
var environment = Environment.GetEnvironmentVariable($"{APP_PREFIX}_ENVIRONMENT");
var config = new ConfigurationBuilder()
	.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
	.AddJsonFile(LookupFile("appsettings.Overrides.json", INSTALLDIRECTORY), optional: true, reloadOnChange: true)
	.AddEnvironmentVariables(APP_PREFIX)
	.AddCommandLine(args.Where(a => a.StartsWith(APP_CONFIGPREFIX)).Select(a => a.Replace(APP_CONFIGPREFIX, "--")).ToArray())
	.Build();
services.AddSingleton<IConfiguration>(config);
services.AddSingleton<OpsService>();
//services.AddOptions<APIOptions>().Bind(config.GetRequiredSection("API"));

// Register all commands + settings
var types = new[]
{
	Assembly.GetExecutingAssembly(),
	//typeof(APIOptions).Assembly
}.SelectMany(a => a.ExportedTypes);

types.Where(t => !t.IsAbstract && t.IsClass &&
	(
		t.IsSubclassOf(typeof(global::BaseSettings)) || t.IsSubclassOf(typeof(AsyncCommand)) || t.IsSubclassOf(typeof(AsyncCommand<>))
		|| t.IsSubclassOf(typeof(Command)) || t.IsSubclassOf(typeof(Command<>))
	))
	.ForEachEx(t => services.AddSingleton(t));

await using var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);
app.Configure(config =>
{
	config.SetApplicationName(Path.GetFileName(Environment.ProcessPath));

	config.SetExceptionHandler(ex =>
	{
		if (ex is AggregateException ae)
		{
			ae.Handle((hre) =>
			{
				AnsiConsole.WriteException(hre);
				return true;
			});
		}
		else
		{
			AnsiConsole.WriteException(ex);
		}
	});

	config.AddCommand<ShowHelpCommand>("showhelp")
		.WithDescription("shows tool and api help");

	//config.AddCommand<ExportToRawTextCommand>("exportrawtext")
	//	.WithDescription("exports the PDF as Raw TEXT with co-ordinates")
	//	.WithExample(new[] { "exportrawtext", @"-i", @"""samples\extract\PO12-08-2021.pdf""", @"-o", @"""PO12-08-2021_default.txt""", @"--granularity", @"""lines""", @"--verbose" })
	//	.WithExample(new[] { "exportrawtext", @"-i", @"""samples\extract\PO12-08-2021.pdf""", @"-o", @"""PO12-08-2021_default.txt""", @"--granularity", @"""words""", @"--verbose" })
	//	.WithExample(new[] { "exportrawtext", @"-i", @"""samples\extract\PO12-08-2021.pdf""", @"-o", @"""PO12-08-2021_default.txt""", @"--granularity", @"""letters""", @"--verbose" })
	//	;


#if DEBUG
	config.ValidateExamples();
#endif
});

var started = DateTime.Now;
var sw = Stopwatch.StartNew();
var exitCode = app.Run(args.Where(arg => !arg.StartsWith(APP_CONFIGPREFIX)));
sw.Stop();

AnsiConsole.MarkupLine("\n\n");
AnsiConsole.MarkupLine($"[green]⌚ Started=[/][yellow]{started}[/] [green]Ended=[/][yellow]{DateTime.Now}[/] [green]~Time=[/][yellow]{sw.Elapsed.Humanize(1, minUnit: Humanizer.Localisation.TimeUnit.Millisecond)}({sw.ElapsedMilliseconds} MS)[/][orange1] ERRORCODE={exitCode}[/]");
AnsiConsole.MarkupLine("[yellow]😊 Done.[/]");
return exitCode;

// Lookup the file in the startup directory, then in the install directory
static string? LookupFile(string? fileName, string installDirectory)
{
	var currentDirectory = Environment.CurrentDirectory;
	if (currentDirectory != installDirectory)
	{
		var file1 = Path.Combine(currentDirectory, fileName);
		if (File.Exists(file1))
		{
			fileName = file1;
		}
	}

	//ConsoleLogger.WriteRawLogMessage($"[green]using settings overrides file -[/] {fileName}");
	return fileName;
}