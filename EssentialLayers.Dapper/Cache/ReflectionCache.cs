using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace EssentialLayers.Dapper.Cache
{
	internal static class ReflectionCache
	{
		private static readonly ConcurrentDictionary<Type, PropertyInfo[]> Cache = new();

		internal static PropertyInfo[] GetCachedProperties<T>(
			this T type
		) => Cache.GetOrAdd(type.GetType(), static t => t.GetProperties());		
	}
}