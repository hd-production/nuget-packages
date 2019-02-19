using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace HdProduction.Npgsql.Orm
{
  public static class TypeMappings
  {
    public static NpgsqlDbType ToSqlDbType<T>()
    {
      return Mapping[typeof(T)];
    }

    internal static readonly Dictionary<Type, NpgsqlDbType> Mapping = new Dictionary<Type, NpgsqlDbType>
    {
      {typeof(byte[]), NpgsqlDbType.Bytea},
      {typeof(bool), NpgsqlDbType.Boolean},
      {typeof(bool?), NpgsqlDbType.Boolean},
      {typeof(string), NpgsqlDbType.Varchar},
      {typeof(DateTime), NpgsqlDbType.Timestamp},
      {typeof(DateTime?), NpgsqlDbType.Timestamp},
      {typeof(double), NpgsqlDbType.Double},
      {typeof(double?), NpgsqlDbType.Double},
      {typeof(float), NpgsqlDbType.Double},
      {typeof(float?), NpgsqlDbType.Double},
      {typeof(long), NpgsqlDbType.Bigint},
      {typeof(long?), NpgsqlDbType.Bigint},
      {typeof(ulong), NpgsqlDbType.Bigint},
      {typeof(ulong?), NpgsqlDbType.Bigint},
      {typeof(int), NpgsqlDbType.Integer},
      {typeof(int?), NpgsqlDbType.Integer},
      {typeof(uint), NpgsqlDbType.Integer},
      {typeof(uint?), NpgsqlDbType.Integer},
      {typeof(Guid), NpgsqlDbType.Varchar},
      {typeof(object), NpgsqlDbType.Jsonb},
    };
  }
}