using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

/// <summary>
/// The Console namespace.
/// </summary>
namespace Spectre.Console;

/// <summary>
/// Class TypeResolver. This class cannot be inherited.
/// </summary>
public sealed class TypeResolver : ITypeResolver, IAsyncDisposable
{
	/// <summary>
	/// The provider
	/// </summary>
	private readonly IServiceProvider _provider;

	/// <summary>
	/// Initializes a new instance of the <see cref="TypeResolver" /> class.
	/// </summary>
	/// <param name="provider">The provider.</param>
	/// <exception cref="System.ArgumentNullException">provider</exception>
	public TypeResolver(IServiceProvider provider)
	{
		_provider = provider ?? throw new ArgumentNullException(nameof(provider));
	}

	/// <summary>
	/// Resolves an instance of the specified type.
	/// </summary>
	/// <param name="type">The type to resolve.</param>
	/// <returns>An instance of the specified type.</returns>
	public object Resolve(Type type)
	{
		return _provider.GetRequiredService(type);
	}

	///// <summary>
	///// Disposes this instance.
	///// </summary>
	//public void Dispose()
	//{
	//	if (_provider is IDisposable disposable)
	//	{
	//		disposable.Dispose();
	//	}
	//}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous dispose operation.</returns>
	public async ValueTask DisposeAsync()
	{
		if (_provider is IAsyncDisposable disposable)
		{
			await disposable.DisposeAsync();
		}
	}
}