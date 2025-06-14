﻿using EssentialLayers.Helpers.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EssentialLayers.Helpers.Extension
{
	public static class TExtension
	{
		public static bool IsAny<T>(
			this T self, IEnumerable<T> values
		)
		{
			if (self == null) return false;

			return new HashSet<T>(values).Contains(self);
		}

		public static bool NotAny<T>(
			this T self, IEnumerable<T> values
		)
		{
			if (self == null) return false;

			return new HashSet<T>(values).Contains(self);
		}

		public static bool IsAny<T>(
			this T self, params T[] values
		)
		{
			if (self == null) return false;

			return new HashSet<T>(values).Contains(self);
		}

		public static bool NotAny<T>(
			this T self, params T[] values
		)
		{
			if (self == null) return false;

			return new HashSet<T>(values).Contains(self).False();
		}

		public static T DeepCopy<T>(this T self)
		{
			try
			{
				string serialized = JsonConvert.SerializeObject(self);

				return JsonConvert.DeserializeObject<T>(serialized)!;
			}
			catch (Exception e)
			{
				throw new Exception("Error on make the deep copy from the object", e);
			}
		}

		public static string Serialize<T>(
			this T self, bool indented = false, bool insensitive = false
		)
		{
			try
			{
				JsonSerializerSettings settings = SerializerSettings(indented, insensitive);
				string deserialized = JsonConvert.SerializeObject(self, settings);

				return deserialized;
			}
			catch (Exception e)
			{
				throw new Exception("Error on serialize object", e);
			}
		}

		public static T Deserialize<T>(
			this string self, bool indented = false, bool insensitive = false
		)
		{
			try
			{
				JsonSerializerSettings settings = SerializerSettings(indented, insensitive);
				T deserialized = JsonConvert.DeserializeObject<T>(self, settings);

				return deserialized;
			}
			catch (Exception e)
			{
				throw new Exception("Error on deserialize object", e);
			}
		}

		public static ResultHelper<string> SerializeResult<T>(
			this T self, bool indented = false, bool insensitive = false
		)
		{
			try
			{
				JsonSerializerSettings settings = SerializerSettings(indented, insensitive);
				string serialized = JsonConvert.SerializeObject(self, settings);

				return ResultHelper<string>.Success(serialized);
			}
			catch (Exception e)
			{
				return ResultHelper<string>.Fail(e);
			}
		}

		public static ResultHelper<T> DeserializeResult<T>(
			this string self, bool indented, bool insensitive = false
		)
		{
			try
			{
				JsonSerializerSettings settings = SerializerSettings(indented, insensitive);
				T deserialized = JsonConvert.DeserializeObject<T>(self, settings);

				return ResultHelper<T>.Success(deserialized);
			}
			catch (Exception e)
			{
				return ResultHelper<T>.Fail(e);
			}
		}

		public static bool IsSimpleType<T>(this T self)
		{
			if (self is null)
			{
				Type type = self!.GetType();

				return type.IsPrimitive ||
					new Type[] {
					typeof(int),
					typeof(double),
					typeof(string),
					typeof(decimal),
					typeof(DateTime),
					typeof(DateTimeOffset),
					typeof(TimeSpan),
					typeof(Guid)
				}.Contains(type);
			}

			return false;
		}

		public static T SearchProperty<T>(this object obj, params string[] propertyNames)
		{
			try
			{
				PropertyInfo[] properties = obj.GetType().GetProperties();

				foreach (string propertyName in propertyNames)
				{
					PropertyInfo property = properties.FirstOrDefault(p => p.Name == propertyName);

					if (!string.IsNullOrEmpty(propertyName)) return (T)property!.GetValue(obj)!;
				}
			}
			catch (Exception e)
			{
				throw new Exception("The property doesn't exists in the object", e);
			}

			return default!;
		}

		public static bool NotEquals<T, K>(this T self, K other)
		{
			string serializedSelf = self.Serialize();
			string serializedOther = other.Serialize();

			return !serializedSelf.Equals(serializedOther);
		}

		public static bool AreEquals<T, K>(this T self, K other)
		{
			string serializedSelf = self.Serialize();
			string serializedOther = other.Serialize();

			return serializedSelf.Equals(serializedOther);
		}

		/**/

		private static JsonSerializerSettings SerializerSettings(
			bool indented, bool insensitive
		)
		{
			DefaultContractResolver resolver = new() { NamingStrategy = new CamelCaseNamingStrategy() };

			return new()
			{
				ContractResolver = insensitive ? resolver : null,
				Formatting = indented ? Formatting.Indented : Formatting.None,
			};
		}
	}
}