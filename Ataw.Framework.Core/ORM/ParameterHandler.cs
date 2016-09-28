
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
	internal class ParameterHandler
	{
		internal void FixParameterType(DbCommandData data)
		{
		//    foreach (var parameter in data.Parameters)
		//    {
		//        if (parameter.Direction == ParameterDirection.Input
		//            && parameter.DataType == DataTypes.Object)
		//        {
		//            if (parameter.Value == null)
		//            {
		//                parameter.DataType = DataTypes.String;
		//            }
		//            else
		//            {
		//                var type = parameter.Value.GetType();

		//                parameter.DataType = data.ContextData.Provider.GetDbTypeForClrType(type);
		//                if (parameter.DataType == DataTypes.Object)
		//                    throw new FluentDataException(string.Format("The parameter {0} is of a type that is not supported.", parameter.ParameterName));
		//            }
		//        }

		//        if (parameter.Value == null)
		//            parameter.Value = DBNull.Value;

		//        var dbParameter = data.InnerCommand.CreateParameter();
		//        dbParameter.DbType = (System.Data.DbType) parameter.DataType;
		//        dbParameter.ParameterName = data.ContextData.Provider.GetParameterName(parameter.ParameterName);
		//        dbParameter.Direction = (System.Data.ParameterDirection) parameter.Direction;
		//        dbParameter.Value = parameter.Value;
		//        if (parameter.Size > 0)
		//            dbParameter.Size = parameter.Size;
		//        data.InnerCommand.Parameters.Add(dbParameter);
		//    }
		}
	}
}