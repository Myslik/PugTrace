
namespace PugTrace
{
    public class GlobalConfiguration : IGlobalConfiguration
    {
        private static readonly IGlobalConfiguration _configuration = new GlobalConfiguration();

        public static IGlobalConfiguration Configuration
        {
            get { return _configuration; }
        }

        internal GlobalConfiguration()
        {
        }
    }
}