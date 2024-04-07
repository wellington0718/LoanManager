using System.Threading.Tasks;

namespace LoanManager.Contracts;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
