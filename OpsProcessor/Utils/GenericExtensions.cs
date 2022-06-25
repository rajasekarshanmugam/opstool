using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using MD5Hasher = System.Security.Cryptography.MD5;

/// <summary>
/// The System namespace.
/// </summary>
namespace System;

/// <summary>
/// Class GenericExtensions.
/// </summary>
public static class GenericExtensions
{
	/// <summary>
	/// md5 hash a string
	/// </summary>
	/// <param name="source">The source.</param>
	/// <returns>System.String.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MD5(this string source)
	{
		using var md5 = MD5Hasher.Create();
		return BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes(source)));
	}

	/// <summary>
	/// ForEach enumeration.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source">The source.</param>
	/// <param name="action">The action.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ForEachEx<T>(this IEnumerable<T>? source, Action<T> action)
	{
		if (source is not null && source is not null)
		{
			foreach (var item in source)
			{
				action(item);
			}
		}
	}

	/// <summary>
	/// escape single quote as per sqlite
	/// </summary>
	/// <param name="source">The source.</param>
	/// <returns>string.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? EscapeSingleQuote(this string? source)
	{
		if (source.IsNullOrEmpty())
		{
			return source;
		}

		if (source.StartsWith('\'') && source.EndsWith('\''))
		{
			source = source[1..^1]; // strip quotes..
			if (source.Length > 0)
			{
				source = source.Replace("'", "''");  // alredy quoted, so quote only inner content...
			}
		}
		else
		{
			source = source.Replace("'", "''");
		}
		return source;
	}

	/// <summary>
	/// Checks if given array has source as one of the values
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="targets">The targets.</param>
	/// <returns>string.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool In(this string source, params string[]? targets)
		=> source is not null && targets is not null && targets.Length > 0 && targets.Contains(source);

	/// <summary>
	/// Coalesces the specified strings.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="source2">The source2.</param>
	/// <returns>string.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? Coalesce(this string? source, string? source2)
		=> !source.IsNullOrEmpty() ? source : source2;

	/// <summary>
	/// Coalesces the specified strings.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="source2">The source2.</param>
	/// <param name="source3">The source3.</param>
	/// <returns>string.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? Coalesce(this string? source, string? source2, string? source3)
		=> !source.IsNullOrEmpty() ? source : !source2.IsNullOrEmpty() ? source2 : source3;

	/// <summary>
	/// Joins the string with a splitter.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="splitter">The splitter.</param>
	/// <returns>string?.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? JoinWith(this IEnumerable<string?> source, string splitter = ",")
		=> source is null ? null : string.Join(splitter, source);

	/// <summary>
	/// Determines whether source is null or empty.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <returns><c>true</c> if [is null or empty] [the specified source]; othDerwise, <c>false</c>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsNullOrEmpty([NotNullWhen(false)] this string? source) => string.IsNullOrEmpty(source);

	/// <summary>
	/// Determines whether source is null or white space.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <returns><c>true</c> if [is null or white space] [the specified source]; otherwise, <c>false</c>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? source) => string.IsNullOrWhiteSpace(source);

	/// <summary>
	/// case insensitive comparison
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="target">The target.</param>
	/// <returns>bool.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool CaseInsensitiveEquals(this string? source, string? target) => string.Equals(source, target, StringComparison.OrdinalIgnoreCase);

	/// <summary>
	/// Gets the custom attribute of type TAttribute on TSource.
	/// </summary>
	/// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
	/// <param name="sourceType">Type of the source.</param>
	/// <returns>System.Nullable&lt;TAttribute&gt;.</returns>
	public static TAttribute? GetCustomAttribute<TAttribute>(this Type sourceType)
		where TAttribute : Attribute
	{
		return sourceType.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
	}

	/// <summary>
	/// Gets the custom attribute of type TAttribute on source member info.
	/// </summary>
	/// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
	/// <param name="memberInfo">The member information.</param>
	/// <returns>System.Nullable&lt;TAttribute&gt;.</returns>
	public static TAttribute? GetCustomAttribute<TAttribute>(this MemberInfo memberInfo)
		where TAttribute : Attribute
	{
		return memberInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
	}
}