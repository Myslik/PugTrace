using System.Collections.Generic;

namespace PugTrace
{
    public interface IAuthorizationFilter
    {
        bool Authorize(IDictionary<string, object> owinEnvironment);
    }
}
