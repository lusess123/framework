using System;
namespace Ataw.Framework.Core
{
    public static class BlockUtil
    {
        public static ResponseResult Block(this ResponseResult result, Action<ResponseResult> action)
        {
            //ResponseResult result = new ResponseResult();
            try
            {
                action(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "执行命令定义时出错。有关详细信息，请参阅内部异常。")
                    ex = ex.InnerException;
                //Log4netContent.Log.Info(ex.Message);
                result.Result = -1;
                result.Error = ex.Message;
                // throw ex;
            }
            //throw new NotImplementedException();
            return result;

        }
    }
}
