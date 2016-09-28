using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ataw.Framework.Core;
using System.Data;
using System.Data.SqlClient;

namespace Ataw.Framework.Core
{
    [CodePlug("DefaultMessageFilter", BaseClass = typeof(IMessageFilter),
   CreateDate = "2014-04-03", Author = "sj", Description = "消息过滤插件")]
    public class DefaultMessageFilter : IMessageFilter
    {
        public bool IsFiltered(MessageModel model, IUnitOfData unitOfData)
        {
            bool isFiltered = false;
            string userID = AtawAppContext.Current.UserId;
            if (model.ReceiveID == userID)
                return isFiltered;
            if (!model.Privacy.IsEmpty())
            {
                switch (model.Privacy.ToEnum<PrivacyType>())
                {
                    case PrivacyType.ToDepartment:
                        string sql = "SELECT COUNT(*) FROM Ataw_UsersDetail WHERE USERID=@UserID AND FDepartmentID=@DepartmentID";
                        var count = unitOfData.QueryObject(sql, new SqlParameter("@UserID", userID),
                                 new SqlParameter("@DepartmentID", model.ReceiveID)).Value<int>();
                        if (count == 0)
                        {
                            isFiltered = true;
                        }
                        break;
                    case PrivacyType.ToUser:
                        if (model.ReceiveID != userID)
                            isFiltered = true;
                        break;

                }
            }
            return isFiltered;
        }
    }
}