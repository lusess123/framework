using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public interface ISnsUserCardService
    {
        IEnumerable<SnsUserCard> GetUserCardList(IEnumerable<string> userIds);
    }

    public class SnsUserCard
    {
        /// <summary>
        /// 是否是本人
        /// </summary>
        public bool IsSelf
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string Logo
        {
            get;
            set;
        }
        public string NickName
        {
            get;
            set;
        }
        public string PositionJobId
        {
            get;
            set;
        }
        public string PositionJob
        {
            get;
            set;
        }
        public string Department
        {
            get;
            set;
        }
        public string DepartmentId
        {
            get;
            set;
        }
        public string Signatures
        {
            get;
            set;
        }
    }
}
