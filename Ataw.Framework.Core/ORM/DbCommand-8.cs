
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
	internal partial class DbCommand
	{
		public IDbCommand Parameters(params object[] parameters)
		{
			for (var i = 0; i < parameters.Count(); i++)
				Parameter(i.ToString(), parameters[i]);
			return this;
		}

		public IDbCommand Parameter(string name, object value, DataTypes parameterType, ParameterDirection direction, int size = 0)
		{
			if (ReflectionHelper.IsList(value))
				AddListParameterToInnerCommand(name, value);
			else
				AddParameterToInnerCommand(name, value, parameterType, direction, size);

			return this;
		}

		private void AddListParameterToInnerCommand(string name, object value)
		{
			var list = (IEnumerable) value;

			var newInStatement = new StringBuilder();

			var k = -1;
			foreach (var item in list)
			{
				k++;
				if (k == 0)
					newInStatement.Append(" in(");
				else
					newInStatement.Append(",");
				
				var parameter = AddParameterToInnerCommand("p" + name + "p" + k.ToString(), item);

				newInStatement.Append(parameter.ParameterName);
			}
			newInStatement.Append(")");

			var oldInStatement = string.Format(" in({0})", _data.ContextData.Provider.GetParameterName(name));
			_data.Sql.Replace(oldInStatement, newInStatement.ToString());
		}

		private IDbDataParameter AddParameterToInnerCommand(string name, object value, DataTypes parameterType = DataTypes.Object, ParameterDirection direction = ParameterDirection.Input, int size = 0)
		{
			if (value == null)
				value = DBNull.Value;

			if (value.GetType().IsEnum)
				value = (int) value;

			var dbParameter = _data.InnerCommand.CreateParameter();
			if (parameterType == DataTypes.Object)
				dbParameter.DbType = (System.Data.DbType) _data.ContextData.Provider.GetDbTypeForClrType(value.GetType());
			else
				dbParameter.DbType = (System.Data.DbType) parameterType;

			dbParameter.ParameterName = _data.ContextData.Provider.GetParameterName(name);
			dbParameter.Direction = (System.Data.ParameterDirection) direction;
			dbParameter.Value = value;
			if (size > 0)
				dbParameter.Size = size;
			_data.InnerCommand.Parameters.Add(dbParameter);

			return dbParameter;
		}

		public IDbCommand Parameter(string name, object value)
		{
			Parameter(name, value, DataTypes.Object, ParameterDirection.Input);
			return this;
		}

		public IDbCommand ParameterOut(string name, DataTypes parameterType, int size)
		{
			if (!_data.ContextData.Provider.SupportsOutputParameters)
				throw new FluentDataException("The selected database does not support output parameters");
			Parameter(name, null, parameterType, ParameterDirection.Output, size);
			return this;
		}

		public TParameterType ParameterValue<TParameterType>(string outputParameterName)
		{
			outputParameterName = _data.ContextData.Provider.GetParameterName(outputParameterName);
			if (!_data.InnerCommand.Parameters.Contains(outputParameterName))
				throw new FluentDataException(string.Format("Parameter {0} not found", outputParameterName));

			var value = (_data.InnerCommand.Parameters[outputParameterName] as System.Data.IDataParameter).Value;

			if (value == null)
				return default(TParameterType);

			if (value.GetType() == typeof(TParameterType))
				return (TParameterType) value;

			return (TParameterType) Convert.ChangeType(value, typeof(TParameterType));
		}
	}
}