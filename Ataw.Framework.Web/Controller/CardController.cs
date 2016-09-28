using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
  public  class CardController : AtawBaseController
    {
      public string Show(string id)
      {
          SnsUserCard card = null;
          try
          {
      
              card = AtawAppContext.Current.AtawCache.GetAndSet("SNS-USER-CARD" + id, () =>
               {
                   var list = "Core".CodePlugIn<ISnsUserCardService>().GetUserCardList(new string[] { id });
                   return list.First();
               },
               "SNS-USER" + id);
          }
          catch
          {
              card = new SnsUserCard();
          }
          card.IsSelf = card.UserId == AtawAppContext.Current.UserId;
          return ReturnJson(card);
      }
    }
}
