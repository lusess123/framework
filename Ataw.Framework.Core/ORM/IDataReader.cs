
// FluentData version 2.3.0.0.
// Copyright ©  2012 - Lars-Erik Kindblad (http://www.kindblad.com).
// See http://fluentdata.codeplex.com for more information and licensing terms.

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace FluentData
{
	public interface IDataReader
	{
		void Close();
		int Depth { get; }
		void Dispose();
		int FieldCount { get; }
		bool GetBoolean(int i);
		bool GetBoolean(string name);
		byte GetByte(int i);
		byte GetByte(string name);
		long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);
		long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferoffset, int length);
		char GetChar(int i);
		char GetChar(string name);
		long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);
		long GetChars(string name, long fieldoffset, char[] buffer, int bufferoffset, int length);
		global::System.Data.IDataReader GetData(int i);
		global::System.Data.IDataReader GetData(string name);
		string GetDataTypeName(int i);
		string GetDataTypeName(string name);
		DateTime GetDateTime(int i);
		DateTime GetDateTime(string name);
		decimal GetDecimal(int i);
		decimal GetDecimal(string name);
		double GetDouble(int i);
		double GetDouble(string name);
		Type GetFieldType(int i);
		Type GetFieldType(string name);
		float GetFloat(int i);
		float GetFloat(string name);
		Guid GetGuid(int i);
		Guid GetGuid(string name);
		short GetInt16(int i);
		short GetInt16(string name);
		int GetInt32(int i);
		int GetInt32(string name);
		long GetInt64(int i);
		long GetInt64(string name);
		string GetName(int i);
		string GetName(string name);
		int GetOrdinal(string name);
		global::System.Data.DataTable GetSchemaTable();
		string GetString(int i);
		string GetString(string name);
		object GetValue(int i);
		object GetValue(string name);
		int GetValues(object[] values);
		bool IsClosed { get; }
		bool IsDBNull(int i);
		bool IsDBNull(string name);
		bool NextResult();
		bool Read();
		int RecordsAffected { get; }
		object this[int i] { get; }
		object this[string name] { get; }
	}

}