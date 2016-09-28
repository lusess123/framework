using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
   public interface ISnsClubCardService
    {
        IEnumerable<SnsClubCard> GetClubCardList(IEnumerable<string> ClubIds);
    }

    public class SnsClubCard
    {
        public string ClubId
        {
            get;
            set;
        }
        public string Logo
        {
            get;
            set;
        }
        public string ClubName
        {
            get;
            set;
        }
        public string Introduction
        {
            get;
            set;
        }
    }
}
