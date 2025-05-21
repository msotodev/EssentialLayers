using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EssentialLayers.Helpers.Extension
{
	public static class StringExtension
	{
		public static bool IsEmpty(this string self, bool includeWhiteSpaces = false)
		{
			if (self == null) return true;

			if (includeWhiteSpaces) return string.IsNullOrWhiteSpace(self);

			return string.IsNullOrEmpty(self);
		}

		public static bool NotEmpty(this string self, bool includeWhiteSpaces = false)
		{
			if (self == null) return false;

			if (includeWhiteSpaces) return !string.IsNullOrWhiteSpace(self);

			return !string.IsNullOrEmpty(self);
		}

		public static bool Match(this string self, string pattern)
		{
			if (self.IsEmpty()) return false;

			return new Regex(pattern).IsMatch(self);
		}

		public static bool NoMatch(this string self, string pattern)
		{
			if (self.IsEmpty()) return false;

			return !new Regex(pattern).IsMatch(self);
		}

		public static bool NotEquals(this string self, string another)
		{
			if (self != null) return !self.Equals(another);

			return false;
		}

		public static string RegexReplace(this string self, string pattern, string replacement)
		{
			if (self.IsEmpty()) return string.Empty;

			return new Regex(pattern).Replace(self, replacement);
		}

		public static string Capitalize(this string self, string separator = " ")
		{
			StringBuilder builder = new();

			if (self.NotEmpty())
			{
				string[] words = self.Split(separator);

				foreach (string word in words)
				{
					builder.Append($"{char.ToUpper(word[0])}{word[1..].ToLower()}{separator}");
				}
			}

			return builder.ToString();
		}

		public static bool HasDigits(this string self)
		{
			if (self.IsEmpty()) return false;

			return self.Any(char.IsDigit);
		}

		public static bool HasLetters(this string self)
		{
			if (self.IsEmpty()) return false;

			return self.Any(char.IsLetter);
		}

		public static bool MinLength(this string self, int length)
		{
			if (self.IsEmpty()) return false;

			return self.Length >= length;
		}

		public static bool Length(this string self, int length)
		{
			if (self.IsEmpty()) return false;

			return self.Length == length;
		}

		public static bool MaxLength(this string self, int length)
		{
			if (self.IsEmpty()) return false;

			return self.Length <= length;
		}

		public static string FirstUpperWord(this string self)
		{
			if (self.IsEmpty()) return string.Empty;

			string first = string.Empty;

			foreach (char s in self)
			{
				if (s.ToString().Match("^[A-Z]*$")) break;

				first += s;
			}

			return first;
		}

		public static string WrapText(this string self, int n, bool wrap = true)
		{
			if (self.IsEmpty()) return string.Empty;

			string value = self;

			if (wrap && self.NotNull() && self.Length > n) value = self[..n] + "...";

			return value;
		}

		public static string LeftSpaces(this string self, int length)
		{
			if (self.IsEmpty()) return string.Empty.PadLeft(length);

			return self.Length <= length ? self.PadLeft(length) : self[..length];
		}

		public static string RightSpaces(this string self, int length)
		{
			if (self.IsEmpty()) return string.Empty.PadRight(length);

			return self.Length <= length ? self.PadLeft(length) : self[..length];
		}

		public static string RemoveDiacritics(this string self)
		{
			if (string.IsNullOrWhiteSpace(self)) return self;

			string normalized = self.Normalize(NormalizationForm.FormD);

			StringBuilder stringBuilder = new();

			foreach (char character in normalized)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(character);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}

		public static Stream ToStream(this string self)
		{
			if (self.IsEmpty()) return new MemoryStream();

			byte[] bytes = Encoding.UTF8.GetBytes(self);

			return new MemoryStream(bytes);
		}

		public static T ConvertToType<T>(string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
	}
}