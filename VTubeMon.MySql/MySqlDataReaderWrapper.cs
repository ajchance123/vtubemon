using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using VTubeMon.API;

namespace VTubeMon.MySql
{
    public class MySqlDataReaderWrapper : INamedDataReader
    {

        public MySqlDataReaderWrapper(MySqlDataReader mySqlDataReader)
        {
            _mySqlDataReader = mySqlDataReader;
        }

        private MySqlDataReader _mySqlDataReader;


        public int Depth => _mySqlDataReader.Depth;

        public int RecordsAffected => _mySqlDataReader.RecordsAffected;

        public bool IsClosed => _mySqlDataReader.IsClosed;

        public bool HasRows => _mySqlDataReader.HasRows;

        public int FieldCount => _mySqlDataReader.FieldCount;

        public void Close()
        {
            _mySqlDataReader.Close();
        }

        public void Dispose()
        {
            _mySqlDataReader.Dispose();
        }

        public string GetBodyDefinition(string column)
        {
            return _mySqlDataReader.GetBodyDefinition(column);
        }

        public bool GetBoolean(string name)
        {
            return _mySqlDataReader.GetBoolean(name);
        }

        public bool GetBoolean(int i)
        {
            return _mySqlDataReader.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _mySqlDataReader.GetByte(i);
        }

        public byte GetByte(string name)
        {
            return _mySqlDataReader.GetByte(name);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _mySqlDataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _mySqlDataReader.GetChar(i);
        }

        public char GetChar(string name)
        {
            return _mySqlDataReader.GetChar(name);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _mySqlDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public string GetDataTypeName(int i)
        {
            return _mySqlDataReader.GetDataTypeName(i);
        }

        public DateTime GetDateTime(string column)
        {
            return _mySqlDataReader.GetDateTime(column);
        }

        public DateTime GetDateTime(int i)
        {
            return _mySqlDataReader.GetDateTime(i);
        }

        public decimal GetDecimal(string column)
        {
            return _mySqlDataReader.GetDecimal(column);
        }

        public decimal GetDecimal(int i)
        {
            return _mySqlDataReader.GetDecimal(i);
        }

        public double GetDouble(int i)
        {
            return _mySqlDataReader.GetDouble(i);
        }

        public double GetDouble(string column)
        {
            return _mySqlDataReader.GetDouble(column);
        }

        public IEnumerator GetEnumerator()
        {
            return _mySqlDataReader.GetEnumerator();
        }

        public Type GetFieldType(int i)
        {
            return _mySqlDataReader.GetFieldType(i);
        }

        public Type GetFieldType(string column)
        {
            return _mySqlDataReader.GetFieldType(column);
        }

        public T GetFieldValue<T>(int ordinal)
        {
            return _mySqlDataReader.GetFieldValue<T>(ordinal);
        }

        public float GetFloat(string column)
        {
            return _mySqlDataReader.GetFloat(column);
        }

        public float GetFloat(int i)
        {
            return _mySqlDataReader.GetFloat(i);
        }

        public Guid GetGuid(string column)
        {
            return _mySqlDataReader.GetGuid(column);
        }

        public Guid GetGuid(int i)
        {
            return _mySqlDataReader.GetGuid(i);
        }

        public short GetInt16(string column)
        {
            return _mySqlDataReader.GetInt16(column);
        }

        public short GetInt16(int i)
        {
            return _mySqlDataReader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _mySqlDataReader.GetInt32(i);
        }

        public int GetInt32(string column)
        {
            return _mySqlDataReader.GetInt32(column);
        }

        public long GetInt64(string column)
        {
            return _mySqlDataReader.GetInt64(column);
        }

        public long GetInt64(int i)
        {
            return _mySqlDataReader.GetInt64(i);
        }

        public string GetName(int i)
        {
            return _mySqlDataReader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return _mySqlDataReader.GetOrdinal(name);
        }

        public sbyte GetSByte(string name)
        {
            return _mySqlDataReader.GetSByte(name);
        }

        public sbyte GetSByte(int i)
        {
            return _mySqlDataReader.GetSByte(i);
        }

        public DataTable GetSchemaTable()
        {
            return _mySqlDataReader.GetSchemaTable();
        }

        public string GetString(string column)
        {
            return _mySqlDataReader.GetString(column);
        }

        public string GetString(int i)
        {
            return _mySqlDataReader.GetString(i);
        }

        public TimeSpan GetTimeSpan(int column)
        {
            return _mySqlDataReader.GetTimeSpan(column);
        }

        public TimeSpan GetTimeSpan(string column)
        {
            return _mySqlDataReader.GetTimeSpan(column);
        }

        public ushort GetUInt16(int column)
        {
            return _mySqlDataReader.GetUInt16(column);
        }

        public ushort GetUInt16(string column)
        {
            return _mySqlDataReader.GetUInt16(column);
        }

        public uint GetUInt32(int column)
        {
            return _mySqlDataReader.GetUInt32(column);
        }

        public uint GetUInt32(string column)
        {
            return _mySqlDataReader.GetUInt32(column);
        }

        public ulong GetUInt64(int column)
        {
            return _mySqlDataReader.GetUInt64(column);
        }

        public ulong GetUInt64(string column)
        {
            return _mySqlDataReader.GetUInt64(column);
        }

        public object GetValue(int i)
        {
            return _mySqlDataReader.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            return _mySqlDataReader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return _mySqlDataReader.IsDBNull(i);
        }

        public bool NextResult()
        {
            return _mySqlDataReader.NextResult();
        }

        public bool Read()
        {
            return _mySqlDataReader.Read();
        }
    }
}
