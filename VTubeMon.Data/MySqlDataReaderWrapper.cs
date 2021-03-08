using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTubeMon.API;

namespace VTubeMon.Data
{
    public class MySqlDataReaderWrapper : INamedDataReader
    {

        public MySqlDataReaderWrapper(MySqlDataReader mySqlDataReader)
        {
            _mySqlDataReader = mySqlDataReader;
        }

        private MySqlDataReader _mySqlDataReader;


        public int Depth => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public bool HasRows => throw new NotImplementedException();

        public int FieldCount => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string GetBodyDefinition(string column)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string name)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(string name)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public char GetChar(string name)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(string column)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(string column)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(string column)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(string column)
        {
            throw new NotImplementedException();
        }

        public T GetFieldValue<T>(int ordinal)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(string column)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(string column)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(string column)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(string column)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(string column)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public sbyte GetSByte(string name)
        {
            throw new NotImplementedException();
        }

        public sbyte GetSByte(int i)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(string column)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public TimeSpan GetTimeSpan(int column)
        {
            throw new NotImplementedException();
        }

        public TimeSpan GetTimeSpan(string column)
        {
            throw new NotImplementedException();
        }

        public ushort GetUInt16(int column)
        {
            throw new NotImplementedException();
        }

        public ushort GetUInt16(string column)
        {
            throw new NotImplementedException();
        }

        public uint GetUInt32(int column)
        {
            throw new NotImplementedException();
        }

        public uint GetUInt32(string column)
        {
            throw new NotImplementedException();
        }

        public ulong GetUInt64(int column)
        {
            throw new NotImplementedException();
        }

        public ulong GetUInt64(string column)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }
    }
}
