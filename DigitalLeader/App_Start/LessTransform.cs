using System.Web.Optimization;
using System.IO;

namespace DigitalLeader
{
    public class LessTransform : IBundleTransform
    {
        private string _path;

        public LessTransform(string path)
        {
            _path = path;
        }
        public void Process(BundleContext context, BundleResponse response)
        {
            Directory.SetCurrentDirectory(_path);

            response.Content = dotless.Core.Less.Parse(response.Content);
            response.ContentType = "text/css";
        }
    }
}