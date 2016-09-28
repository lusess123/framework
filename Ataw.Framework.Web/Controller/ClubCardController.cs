using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
  public  class ClubCardController : AtawBaseController
    {
      public string Show(string id)
      {
          SnsClubCard card = null;
          try
          {
      
              card = AtawAppContext.Current.AtawCache.GetAndSet("SNS-CLUB-CARD" + id, () =>
               {
                   var list = "CoreClub".CodePlugIn<ISnsClubCardService>().GetClubCardList(new string[] { id });
                   return list.First();
               },
               "SNS-CLUB" + id);
          }
          catch
          {
              card = new SnsClubCard();
          }
          return ReturnJson(card);
      }
    }
}
