
using System;
using System.Data;
namespace Ataw.Framework.Core
{
    public class DataTypeUtil
    {
        private readonly static Type BlobType = typeof(byte[]);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability",
         "CA1502:AvoidExcessiveComplexity")]
        public static SysDataType ConvertDbTypeToSysDataType(DbType type)
        {
            switch (type)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return SysDataType.String;
                case DbType.Binary:
                    return SysDataType.Binary;
                case DbType.Boolean:
                    return SysDataType.Bit;
                case DbType.Byte:
                case DbType.SByte:
                    return SysDataType.Byte;
                case DbType.Currency:
                    return SysDataType.Money;
                case DbType.Date:
                    return SysDataType.Date;
                case DbType.DateTime:
                case DbType.Time:
                    return SysDataType.DateTime;
                case DbType.Decimal:
                case DbType.VarNumeric:
                    return SysDataType.Decimal;
                case DbType.Double:
                case DbType.Single:
                    return SysDataType.Double;
                case DbType.Guid:
                    return SysDataType.Guid;
                case DbType.Int16:
                case DbType.UInt16:
                    return SysDataType.Short;
                case DbType.Int32:
                case DbType.UInt32:
                    return SysDataType.Int;
                case DbType.Int64:
                case DbType.UInt64:
                    return SysDataType.Long;
                case DbType.Xml:
                    return SysDataType.Xml;
                default:
                    return SysDataType.String;
            }
        }

        public static SysDataType ConvertTypeToSysDataType(Type type)
        {
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    return SysDataType.Bit;
                case TypeCode.Char:
                    return SysDataType.String;
                case TypeCode.SByte:
                case TypeCode.Byte:
                    return SysDataType.Byte;
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    return SysDataType.Short;
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return SysDataType.Int;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return SysDataType.Long;
                case TypeCode.Single:
                case TypeCode.Double:
                    return SysDataType.Double;
                case TypeCode.Decimal:
                    return SysDataType.Decimal;
                case TypeCode.DateTime:
                    return SysDataType.DateTime;
                case TypeCode.String:
                    return SysDataType.String;
                case TypeCode.Object:
                    if (type == BlobType)
                        return SysDataType.Blob;
                    else
                        return SysDataType.String;
            }
            return SysDataType.String;
        }

        public static DbType ConvertSysDataTypeToDbType(SysDataType type)
        {
            DbType result = DbType.AnsiString;

            switch (type)
            {
                case SysDataType.String:
                case SysDataType.Text:
                    result = DbType.AnsiString;
                    break;
                case SysDataType.Int:
                    result = DbType.Int32;
                    break;
                case SysDataType.Date:
                    result = DbType.Date;
                    break;
                case SysDataType.DateTime:
                    result = DbType.DateTime;
                    break;
                case SysDataType.Double:
                    result = DbType.Double;
                    break;
                case SysDataType.Money:
                    result = DbType.Currency;
                    break;
                case SysDataType.Blob:
                case SysDataType.Binary:
                    result = DbType.Binary;
                    break;
                case SysDataType.Guid:
                    result = DbType.Guid;
                    break;
                case SysDataType.Xml:
                    result = DbType.AnsiString;
                    break;
                case SysDataType.Bit:
                    result = DbType.Boolean;
                    break;
                case SysDataType.Byte:
                    result = DbType.Byte;
                    break;
                case SysDataType.Short:
                    result = DbType.Int16;
                    break;
                case SysDataType.Long:
                    result = DbType.Int64;
                    break;
                case SysDataType.Decimal:
                    result = DbType.Decimal;
                    break;

            }
            return result;
        }

        public static Type ConvertSysDataTypeToType(SysDataType type)
        {
            switch (type)
            {
                case SysDataType.Binary:
                case SysDataType.Blob:
                    return typeof(byte[]);
                case SysDataType.Bit:
                    return typeof(bool);
                case SysDataType.Byte:
                    return typeof(byte);
                case SysDataType.Date:
                case SysDataType.DateTime:
                    return typeof(DateTime);
                case SysDataType.Decimal:
                case SysDataType.Money:
                    return typeof(decimal);
                case SysDataType.Double:
                    return typeof(double);
                case SysDataType.Guid:
                    return typeof(Guid);
                case SysDataType.Int:
                    return typeof(int);
                case SysDataType.Long:
                    return typeof(long);
                case SysDataType.Short:
                    return typeof(short);
                case SysDataType.String:
                case SysDataType.Text:
                case SysDataType.Xml:
                    return typeof(string);
            }
            return typeof(string);
        }
    }
}
