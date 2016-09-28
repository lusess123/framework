using System;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public static class RightUtil
    {
        public static void FormartString(string str, List<FunRightItem> menuFuns, List<string> menus)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    //MenuFuns = new List<FunRightItem>();
                    //Menus = new List<string>();
                    FunRightItem menuFun;
                    string[] splitStr = str.Split(new string[] { "||" }, StringSplitOptions.None);
                    for (var i = 0; i < splitStr.Length; i++)
                    {
                        menuFun = new FunRightItem();
                        var index = splitStr[i].LastIndexOf('.');
                        int length = splitStr[i].Length;
                        bool isXml = false;
                        if (length > index + 3)
                        {
                            string _xml = splitStr[i].Substring(index + 1, 3);
                            isXml = _xml.ToUpper(ObjectUtil.SysCulture) == "XML";
                        }
                        if (index != -1 && !isXml)
                        {
                            string menu = splitStr[i].Substring(0, index);
                            string fun = splitStr[i].Substring(index + 1, splitStr[i].Length - index - 1);
                            if (!menuFuns.Exists(a => a.Name == fun && a.Menu == menu))
                            {
                                menuFun.Menu = menu;
                                menuFun.Name = fun;
                                menuFuns.Add(menuFun);
                            }
                            if (!menus.Exists(a => a == menu))
                            {
                                menus.Add(menu);
                            }
                        }
                        else
                        {
                            if (!menus.Exists(a => a == splitStr[i]))
                            {
                                menus.Add(splitStr[i]);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new AtawException("权限表达式错误", ex);
            }

        }
        public static RightFilterType RightVerification(string expressString)
        {
            return RightVerification(expressString, new RegNameList<FunRightItem>());
        }
        public static RightFilterType RightVerification(string expressString, RegNameList<FunRightItem> FunResultItems)
        {
            List<string> menus = new List<string>();
            List<FunRightItem> menuFuns = new List<FunRightItem>();
            FormartString(expressString, menuFuns, menus);
            if (menus == null || menus.Count == 0) { return RightFilterType.Success; }//只验证登陆
            var builder = AtawAppContext.Current.AtawRightBuilder.Value;
            //firstNoMatch 第一个不匹配，isMath 是否都不匹配 0都不匹配，1至少有一个匹配
            for (int i = 0, firstNoMatch = 0, isMatch = 0; i < menus.Count; i++)
            {
                string menu = menus[i];
                var status = builder.MenuRightCheck(menu);

                if (status == 1)
                {
                    var items = builder.CreateFunRight(menu);
                    FunResultItems.AddRange(items);
                    isMatch = 1;
                }
                if (status != 1 && firstNoMatch == 0)
                {
                    firstNoMatch = status;
                }
                if (i == menus.Count - 1 && isMatch == 0)
                {
                    if (firstNoMatch == -1)
                    {
                        return RightFilterType.UnRenew;
                    }
                    else if (firstNoMatch == -2)
                    {
                        return RightFilterType.DenyPermission;
                    }
                }
            }

            //判断功能权限
            for (int i = 0; i < menuFuns.Count; i++)
            {
                var item = FunResultItems[menuFuns[i].RegName.ToUpper()];
                if (item != null && item.IsAllow)
                {
                    return RightFilterType.Success;
                }
                if (i == menuFuns.Count - 1)
                {
                    return RightFilterType.DenyPermission;
                }
            }
            return RightFilterType.Success;
        }
    }
}
