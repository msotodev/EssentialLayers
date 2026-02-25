using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EssentialLayers.Helpers.Extension
{
	public static class IEnumerableExtension
	{
		public static bool IsAny<T>(
			this IEnumerable<T> self, IEnumerable<T> values
		)
		{
			return self.Intersect(values).Any();
		}

		public static bool NotAny<T>(
			this IEnumerable<T> self, IEnumerable<T> values
		)
		{
			return self.Intersect(values).Any().False();
		}

		public static bool IsAny<T>(
			this IEnumerable<T> self, params T[] values
		)
		{
			return self.Intersect(values).Any();
		}

		public static bool NotAny<T>(
			this IEnumerable<T> self, params T[] values
		)
		{
			return self.Intersect(values).Any().False();
		}

		public static bool IsEmpty<T>(
			this IEnumerable<T> list
		)
		{
			if (list == null) return true;

			return list.Any().False();
		}

		public static bool NotEmpty<T>(
			this IEnumerable<T> list
		)
		{
			if (list == null) return false;

			return list.Any();
		}

		public static bool SingleOne<T>(
			this IEnumerable<T> list
		)
		{
			if (list == null) return false;

			return list.Count() == 1;
		}

		public static bool AddIf<T>(
			this IList<T> lst, T data, Func<T, bool> check
		)
		{
			if (lst.All(check).False()) return false;

			lst.Add(data);

			return true;
		}

		public static bool AreEquals<T, K>(
			this IEnumerable<T> self, IEnumerable<K> other
		)
		{
			string serializedSelf = self.Serialize();
			string serializedOther = other.Serialize();

			return serializedSelf.Equals(serializedOther);
		}

		public static bool NotAny<T>(
			this IEnumerable<T> items, Func<T, bool> predicate
		)
		{
			return items.Any(predicate).False();
		}

		public static string ToStringJoin<T>(
			this IEnumerable<T> self
		)
		{
			return string.Join(", ", self);
		}

		public static string Join<T>(
			this IEnumerable<T> self, string separator = ", "
		)
		{
			return string.Join(separator, self);
		}

		public static ObservableCollection<T> ToObservableCollection<T>(
			this IEnumerable<T> self
		)
		{
			return new ObservableCollection<T>(self);
		}

		public static bool TryFirst<T>(this IEnumerable<T> self, out T? value)
		{
			T first = self.FirstOrDefault();

			if (first != null)
			{
				value = first;

				return true;
			}

			value = default;

			return false;
		}

		public static bool TryFirst<T>(
			this IEnumerable<T> self, Func<T, bool> predicate, out T? value
		)
		{
			T first = self.FirstOrDefault(predicate);

			if (first != null)
			{
				value = first;

				return true;
			}

			value = default;

			return false;
		}

		public static bool TrySingle<T>(this IEnumerable<T> self, out T? value)
		{
			T first = self.Single();

			if (first != null)
			{
				value = first;

				return true;
			}

			value = default;

			return false;
		}

		public static bool TrySingle<T>(
			this IEnumerable<T> self, Func<T, bool> predicate, out T? value
		)
		{
			T first = self.Single(predicate);

			if (first != null)
			{
				value = first;

				return true;
			}

			value = default;

			return false;
		}
	}
}