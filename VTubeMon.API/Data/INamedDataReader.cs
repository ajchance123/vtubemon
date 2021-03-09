using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VTubeMon.API
{
    public interface INamedDataReader : IDisposable
    {
        //
        // Summary:
        //     Gets a value indicating the depth of nesting for the current row. This method
        //     is not supported currently and always returns 0.
        int Depth { get; }
        //
        // Summary:
        //     Gets the number of rows changed, inserted, or deleted by execution of the SQL
        //     statement.
        int RecordsAffected { get; }
        //
        // Summary:
        //     Gets a value indicating whether the data reader is closed.
        bool IsClosed { get; }
        //
        // Summary:
        //     Gets a value indicating whether the MySqlDataReader contains one or more rows.
        bool HasRows { get; }
        //
        // Summary:
        //     Gets the number of columns in the current row.
        int FieldCount { get; }

        //
        // Summary:
        //     Closes the MySqlDataReader object.
        void Close();
        void Dispose();
        string GetBodyDefinition(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a Boolean.
        //
        // Parameters:
        //   name:
        bool GetBoolean(string name);
        //
        // Summary:
        //     Gets the value of the specified column as a Boolean.
        //
        // Parameters:
        //   i:
        bool GetBoolean(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a byte.
        //
        // Parameters:
        //   i:
        byte GetByte(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a byte.
        //
        // Parameters:
        //   name:
        byte GetByte(string name);
        //
        // Summary:
        //     Reads a stream of bytes from the specified column offset into the buffer an array
        //     starting at the given buffer offset.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        //   fieldOffset:
        //     The index within the field from which to begin the read operation.
        //
        //   buffer:
        //     The buffer into which to read the stream of bytes.
        //
        //   bufferoffset:
        //     The index for buffer to begin the read operation.
        //
        //   length:
        //     The maximum length to copy into the buffer.
        //
        // Returns:
        //     The actual number of bytes read.
        long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);
        //
        // Summary:
        //     Gets the value of the specified column as a single character.
        //
        // Parameters:
        //   i:
        char GetChar(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a single character.
        //
        // Parameters:
        //   name:
        char GetChar(string name);
        //
        // Summary:
        //     Reads a stream of characters from the specified column offset into the buffer
        //     as an array starting at the given buffer offset.
        //
        // Parameters:
        //   i:
        //
        //   fieldoffset:
        //
        //   buffer:
        //
        //   bufferoffset:
        //
        //   length:
        long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);
        //
        // Summary:
        //     Gets the name of the source data type.
        //
        // Parameters:
        //   i:
        string GetDataTypeName(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a System.DateTime object.
        //
        // Parameters:
        //   column:
        //     The column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     DateTime object.
        //     Call IsDBNull to check for null values before calling this method.
        //     MySql allows date columns to contain the value '0000-00-00' and datetime columns
        //     to contain the value '0000-00-00 00:00:00'. The DateTime structure cannot contain
        //     or represent these values. To read a datetime value from a column that might
        //     contain zero values, use MySql.Data.MySqlClient.MySqlDataReader.GetMySqlDateTime(System.Int32).
        //     The behavior of reading a zero datetime column using this method is defined by
        //     the ZeroDateTimeBehavior connection string option. For more information on this
        //     option, please refer to MySql.Data.MySqlClient.MySqlConnection.ConnectionString.
        DateTime GetDateTime(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a System.DateTime object.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     DateTime object.
        //     Call IsDBNull to check for null values before calling this method.
        //     MySql allows date columns to contain the value '0000-00-00' and datetime columns
        //     to contain the value '0000-00-00 00:00:00'. The DateTime structure cannot contain
        //     or represent these values. To read a datetime value from a column that might
        //     contain zero values, use MySql.Data.MySqlClient.MySqlDataReader.GetMySqlDateTime(System.Int32).
        //     The behavior of reading a zero datetime column using this method is defined by
        //     the ZeroDateTimeBehavior connection string option. For more information on this
        //     option, please refer to MySql.Data.MySqlClient.MySqlConnection.ConnectionString.
        DateTime GetDateTime(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a System.Decimal object.
        //
        // Parameters:
        //   column:
        //     The column name
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Decimal object.
        //     Call IsDBNull to check for null values before calling this method.
        decimal GetDecimal(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a System.Decimal object.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Decimal object.
        //     Call IsDBNull to check for null values before calling this method.
        decimal GetDecimal(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a double-precision floating point number.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Double object.
        //     Call IsDBNull to check for null values before calling this method.
        double GetDouble(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a double-precision floating point number.
        //
        // Parameters:
        //   column:
        //     The column name
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Double object.
        //     Call IsDBNull to check for null values before calling this method.
        double GetDouble(string column);
        //
        // Summary:
        //     Returns an System.Collections.IEnumerator that iterates through the MySql.Data.MySqlClient.MySqlDataReader.
        IEnumerator GetEnumerator();
        //
        // Summary:
        //     Gets the Type that is the data type of the object.
        //
        // Parameters:
        //   i:
        Type GetFieldType(int i);
        Type GetFieldType(string column);
        T GetFieldValue<T>(int ordinal);
        //
        // Summary:
        //     Gets the value of the specified column as a single-precision floating point number.
        //
        // Parameters:
        //   column:
        //     The column name
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Float object.
        //     Call IsDBNull to check for null values before calling this method.
        float GetFloat(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a single-precision floating point number.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Float object.
        //     Call IsDBNull to check for null values before calling this method.
        float GetFloat(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a globally-unique identifier(GUID).
        //
        // Parameters:
        //   column:
        //     The name of the column.
        Guid GetGuid(string column);
        Guid GetGuid(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a 16-bit signed integer.
        //
        // Parameters:
        //   column:
        //     The column name
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; threfore, the data retrieved must already be a
        //     16 bit integer value.
        //     Call IsDBNull to check for null values before calling this method.
        short GetInt16(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a 16-bit signed integer.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     16 bit integer value.
        //     Call IsDBNull to check for null values before calling this method.
        short GetInt16(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a 32-bit signed integer.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     32 bit integer value.
        //     Call IsDBNull to check for null values before calling this method.
        int GetInt32(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a 32-bit signed integer.
        //
        // Parameters:
        //   column:
        //     The column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     32 bit integer value.
        //     Call IsDBNull to check for null values before calling this method.
        int GetInt32(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a 64-bit signed integer.
        //
        // Parameters:
        //   column:
        //     The column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     64 bit integer value.
        //     Call IsDBNull to check for null values before calling this method.
        long GetInt64(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a 64-bit signed integer.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     64 bit integer value.
        //     Call IsDBNull to check for null values before calling this method.
        long GetInt64(int i);
        
        //
        // Summary:
        //     Gets the name of the specified column.
        //
        // Parameters:
        //   i:
        string GetName(int i);
        //
        // Summary:
        //     Gets the column ordinal, given the name of the column.
        //
        // Parameters:
        //   name:
        int GetOrdinal(string name);
        //
        // Summary:
        //     Gets the value of the specified column as a sbyte.
        //
        // Parameters:
        //   name:
        sbyte GetSByte(string name);
        //
        // Summary:
        //     Gets the value of the specified column as a sbyte.
        //
        // Parameters:
        //   i:
        sbyte GetSByte(int i);
        //
        // Summary:
        //     Returns a DataTable that describes the column metadata of the MySqlDataReader.
        DataTable GetSchemaTable();
        //
        // Summary:
        //     Gets the value of the specified column as a System.String object.
        //
        // Parameters:
        //   column:
        //     The column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     String object.
        //     Call IsDBNull to check for null values before calling this method.
        string GetString(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a System.String object.
        //
        // Parameters:
        //   i:
        //     The zero-based column ordinal.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     String object.
        //     Call IsDBNull to check for null values before calling this method.
        string GetString(int i);
        //
        // Summary:
        //     Gets the value of the specified column as a System.TimeSpan object.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Time value.
        //     Call IsDBNull to check for null values before calling this method.
        TimeSpan GetTimeSpan(int column);
        //
        // Summary:
        //     Gets the value of the specified column as a System.TimeSpan object.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     Time value.
        //     Call IsDBNull to check for null values before calling this method.
        TimeSpan GetTimeSpan(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a 16-bit unsigned integer.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     16 bit unsigned integer value.
        //     Call IsDBNull to check for null values before calling this method.
        ushort GetUInt16(int column);
        //
        // Summary:
        //     Gets the value of the specified column as a 16-bit unsigned integer.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     16 bit unsigned integer value.
        //     Call IsDBNull to check for null values before calling this method.
        ushort GetUInt16(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a 32-bit unsigned integer.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     32 bit unsigned integer value.
        //     Call IsDBNull to check for null values before calling this method.
        uint GetUInt32(int column);
        //
        // Summary:
        //     Gets the value of the specified column as a 32-bit unsigned integer.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     32 bit unsigned integer value.
        //     Call IsDBNull to check for null values before calling this method.
        uint GetUInt32(string column);
        //
        // Summary:
        //     Gets the value of the specified column as a 64-bit unsigned integer.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     64 bit unsigned integer value.
        //     Call IsDBNull to check for null values before calling this method.
        ulong GetUInt64(int column);
        //
        // Summary:
        //     Gets the value of the specified column as a 64-bit unsigned integer.
        //
        // Parameters:
        //   column:
        //     The zero-based column ordinal or column name.
        //
        // Returns:
        //     The value of the specified column.
        //
        // Remarks:
        //     No conversions are performed; therefore, the data retrieved must already be a
        //     64 bit unsigned integer value.
        //     Call IsDBNull to check for null values before calling this method.
        ulong GetUInt64(string column);
        //
        // Summary:
        //     Gets the value of the specified column in its native format.
        //
        // Parameters:
        //   i:
        object GetValue(int i);
        //
        // Summary:
        //     Gets all attribute columns in the collection for the current row.
        //
        // Parameters:
        //   values:
        int GetValues(object[] values);
        //
        // Summary:
        //     Gets a value indicating whether the column contains non-existent or missing values.
        //
        // Parameters:
        //   i:
        bool IsDBNull(int i);
        //
        // Summary:
        //     Advances the data reader to the next result, when reading the results of batch
        //     SQL statements.
        bool NextResult();
        //
        // Summary:
        //     Advances the MySqlDataReader to the next record.
        bool Read();
    }
}
