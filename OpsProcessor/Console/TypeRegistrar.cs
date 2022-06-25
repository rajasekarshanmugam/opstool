using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

/// <summary>
/// The Console namespace.
/// </summary>
namespace Spectre.Console;

/// <summary>
/// Class TypeRegistrar. This class cannot be inherited.
/// Implements the <see cref="Spectre.Console.Cli.ITypeRegistrar" />
/// </summary>
/// <seealso cref="Spectre.Console.Cli.ITypeRegistrar" />
public sealed class TypeRegistrar : ITypeRegistrar, IDisposable, IAsyncDisposable
{
	/// <summary>
	/// The builder
	/// </summary>
	private readonly IServiceCollection _builder;

	/// <summary>
	/// The provider
	/// </summary>
	private ServiceProvider? _provider;

	/// <summary>
	/// Initializes a new instance of the <see cref="TypeRegistrar" /> class.
	/// </summary>
	/// <param name="builder">The builder.</param>
	public TypeRegistrar(IServiceCollection builder)
	{
		_builder = builder;
	}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Dispose as an asynchronous operation.
	/// </summary>
	/// <returns>A Task&lt;ValueTask&gt; representing the asynchronous operation.</returns>
	public async ValueTask DisposeAsync()
	{
		await DisposeAsyncCore();

		Dispose(disposing: false);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
		GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
	}

	/// <summary>
	/// Releases unmanaged and - optionally - managed resources.
	/// </summary>
	/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
	private void Dispose(bool disposing)
	{
		if (disposing)
		{
			_provider?.Dispose();
			_provider = null;
		}
	}

	/// <summary>
	/// Disposes the asynchronous core.
	/// </summary>
	/// <returns>ValueTask.</returns>
	private async ValueTask DisposeAsyncCore()
	{
		if (_provider is not null)
		{
			await _provider.DisposeAsync().ConfigureAwait(false);
		}
		_provider = null;
	}

	/// <summary>
	/// Builds the type resolver representing the registrations
	/// specified in the current instance.
	/// </summary>
	/// <returns>A type resolver.</returns>
	public ITypeResolver Build()
	{
		return new TypeResolver(_provider = _builder.BuildServiceProvider());
	}

	/// <summary>
	/// Registers the specified service.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="implementation">The implementation.</param>
	public void Register(Type service, Type implementation)
	{
		_builder.AddSingleton(service, implementation);
	}

	/// <summary>
	/// Registers the specified instance.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="implementation">The implementation.</param>
	public void RegisterInstance(Type service, object implementation)
	{
		_builder.AddSingleton(service, implementation);
	}

	/// <summary>
	/// Registers the lazy.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="func">The function.</param>
	/// <exception cref="System.ArgumentNullException">func</exception>
	public void RegisterLazy(Type service, Func<object> func)
	{
		if (func is null)
		{
			throw new ArgumentNullException(nameof(func));
		}

		_builder.AddSingleton(service, (provider) => func());
	}
}