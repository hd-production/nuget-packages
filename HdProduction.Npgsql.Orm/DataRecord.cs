using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace HdProduction.Npgsql.Orm
{
  public class DataRecord
  {
    public Dictionary<string, int> FieldNameLookup { get; private set; }
    public object[] Values { get; private set; }

    public T Get<T>(string name, T defaultValue = default(T))
    {
      if (!FieldNameLookup.ContainsKey(name))
        return default(T);

      var index = FieldNameLookup[name];
      return Get(index, defaultValue);
    }

    /// <summary>
    /// Note: do not use this method with bool? type parameter. Use GetNullableBoolean instead.
    /// </summary>
    public T Get<T>(int index, T defaultValue = default(T))
    {
      var value = Values[index];
      return DBNull.Value.Equals(value) || value == null ? defaultValue : (T)value;
    }

    /// <summary>
    /// Method Get&lt;bool?&gt; doesn't work properly, use this method to get value of nullable boolean field.
    /// Additional info: http://thecache.trov.com/mysql-and-boolean-values-2/
    /// </summary>
    public bool? GetNullableBoolean(string name)
    {
      var value = Get<object>(name);
      switch (value)
      {
        case bool _:
          return (bool)value;
        case sbyte _:
          return Convert.ToBoolean((sbyte)value);
        default:
          return null;
      }
    }

    public virtual bool Read(NpgsqlDataReader reader)
    {
      if (!reader.Read())
        return false;

      if (InitFieldNameLookup(reader))
        Values = new object[FieldNameLookup.Count];
      reader.GetValues(Values);

      return true;
    }

    public virtual async Task<bool> ReadAsync(NpgsqlDataReader reader)
    {
      return await ReadAsync(reader, CancellationToken.None);
    }

    public virtual async Task<bool> ReadAsync(NpgsqlDataReader reader, CancellationToken cancellationToken)
    {
      if (!await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        return false;

      if (InitFieldNameLookup(reader))
        Values = new object[FieldNameLookup.Count];
      reader.GetValues(Values);

      return true;
    }

    protected bool InitFieldNameLookup(NpgsqlDataReader reader)
    {
      if (FieldNameLookup != null)
        return false;

      var count = reader.FieldCount;
      FieldNameLookup = new Dictionary<string, int>(count, StringComparer.InvariantCultureIgnoreCase);
      for (int index = 0; index < count; index++)
      {
        var name = reader.GetName(index);
        FieldNameLookup.Add(name, index);
      }

      return true;
    }
  }
}