using System.Threading.Tasks;

namespace PugTrace.Dashboard
{
    public interface IRequestDispatcher
    {
        Task Dispatch(RequestDispatcherContext context);
    }
}
