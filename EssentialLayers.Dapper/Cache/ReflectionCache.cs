using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace EssentialLayers.Dapper.Cache
{
	internal static class ReflectionCache
	{
		private static readonly ConcurrentDictionary<Type, PropertyInfo[]> Cache = new();

		internal static PropertyInfo[] GetProperties(
			Type type
		) => Cache.GetOrAdd(type, static t => t.GetProperties());

		internal static PropertyInfo[] GetProperties<T>() => GetProperties(typeof(T));
	}
}