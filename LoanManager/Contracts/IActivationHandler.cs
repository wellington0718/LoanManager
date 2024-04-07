using System.Threading.Tasks;

namespace LoanManager.Contracts;

public interface IActivationHandler
{
    bool CanHandle(object args);
    Task HandleAsync(object args);
}
