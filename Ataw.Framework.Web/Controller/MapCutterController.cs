using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
    public class MapCutterController : AtawBaseController
    {

        public string RotateBitmap(string src, string angle)
        {
            var res = new Callback().RotateBitmap(src, angle.Value<float>());
            return ReturnJson(new { size = res });

        }

        public string GenerateBitmap(string src, string zoom, int x, int y, int width, int height)
        {
            var res = new Callback().GenerateBitmap(src, zoom, x, y, width, height);
            return ReturnJson(res);
        }


    }
}
