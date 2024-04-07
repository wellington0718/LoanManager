using LoanManager.Contracts;
using System.Threading.Tasks;

namespace LoanManager.Activataion;

public abstract class ActivationHandler<T> : IActivationHandler
    where T : class
{
    protected virtual bool CanHandleInternal(T arg) => true;
    protected abstract Task HandleInternalAsync(T arg);

    public bool CanHandle(object args) => args is T && CanHandleInternal((args as T)!);

    public async Task HandleAsync(object args) => await HandleInternalAsync((args as T)!);
}
