using System;
using System.Collections.Generic;
using System.Data;

namespace EssentialLayers.Dapper.Mappers
{
	/// <summary>
	/// Provides methods for mapping .NET types to database types.
	/// </summary>
	public static class DbTypeMapper
	{
		private static readonly Dictionary<string, DbType> DbTypeMappings = new()
		{
			{ "Byte[]", DbType.Binary },
			{ "Byte", DbType.Byte },
			{ "Boolean", DbType.Boolean },
			{ "DateTime", DbType.DateTime },
			{ "Decimal", DbType.Decimal },
			{ "Double", DbType.Double },
			{ "Guid", DbType.Guid },
			{ "Int16", DbType.Int16 },
			{ "Int32", DbType.Int32 },
			{ "Int64", DbType.Int64 },
			{ "Object", DbType.Object },
			{ "IList`1", DbType.Object },
			{ "List`1", DbType.Object },
			{ "SByte", DbType.SByte },
			{ "Single", DbType.Single },
			{ "String", DbType.String },
			{ "TimeSpan", DbType.Time },
			{ "UInt16", DbType.UInt16 },
			{ "UInt32", DbType.UInt32 },
			{ "UInt64", DbType.UInt64 },
			{ "DateTimeOffset", DbType.DateTimeOffset }
		};

		private static readonly Dictionary<string, SqlDbType> SqlDbTypeMappings = new()
		{
			{ "Int64", SqlDbType.BigInt },
			{ "Boolean", SqlDbType.Bit },
			{ "Char", SqlDbType.Char },
			{ "DateTime", SqlDbType.DateTime },
			{ "Decimal", SqlDbType.Decimal },
			{ "Double", SqlDbType.Float },
			{ "Int32", SqlDbType.Int },
			{ "Single", SqlDbType.Real },
			{ "Guid", SqlDbType.UniqueIdentifier },
			{ "Int16", SqlDbType.SmallInt },
			{ "Byte", SqlDbType.TinyInt },
			{ "Byte[]", SqlDbType.VarBinary },
			{ "String", SqlDbType.VarChar },
			{ "Object", SqlDbType.Structured },
			{ "IList`1", SqlDbType.Structured },
			{ "List`1", SqlDbType.Structured },
			{ "TimeSpan", SqlDbType.Time },
			{ "DateTimeOffset", SqlDbType.DateTimeOffset }
		};

		/// <summary>
		/// Converts a .NET Type to DbType.
		/// </summary>
		/// <param name="type">The .NET Type to convert.</param>
		/// <returns>The corresponding DbType.</returns>
		public static DbType ToDbType(this Type type)
		{
			if (type == null) return DbType.Object;

			return DbTypeMappings[type.Name];
		}

		/// <summary>
		/// Converts a .NET Type to SqlDbType.
		/// </summary>
		/// <param name="type">The .NET Type to convert.</param>
		/// <returns>The corresponding SqlDbType.</returns>
		public static SqlDbType ToSqlDbType(this Type type)
		{
			if (type == null) return SqlDbType.Structured;

			return SqlDbTypeMappings[type.Name];
		}
	}
}