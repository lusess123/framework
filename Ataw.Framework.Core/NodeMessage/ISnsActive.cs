using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Ataw.Framework.Core
{
    interface ISnsActive
    {
        IEnumerable<ActiveModel> GetAllActivePageByUserID(Pagination page, ActiveModel whereBean);
        IEnumerable<ActiveModel> GetComingActiveByUserID(string userId);
        void InsertActive(ActiveModel bean);
    }

    public class ActiveModel
    {
        public string FID
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string ItemType
        {
            get;
            set;
        }

        public string ItemAction
        {
            get;
            set;
        }
        public string SourceId
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string CreateID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string SubContent
        {
            get;
            set;
        }

        public string ReferenceID
        {
            get;
            set;
        }

        public string ReferenceName
        {
            get;
            set;
        }

        public string ActivityFrom
        {
            get;
            set;
        }

        public string PrivacyStatus
        {
            get;
            set;
        }

        public string OwnerID
        {
            get;
            set;
        }

        public string FControlUnitID
        {
            get;
            set;
        }

        public int ActivityStatus
        {
            get;
            set;
        }
      
    }

}
