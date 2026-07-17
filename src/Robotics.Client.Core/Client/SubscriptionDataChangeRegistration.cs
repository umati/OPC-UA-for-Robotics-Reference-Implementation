using Opc.Ua;
using Opc.Ua.Client;

namespace Robotics.Client.Core.Client;

public sealed class SubscriptionDataChangeRegistration(Subscription subscription) : IAsyncDisposable
{
    public Subscription Subscription { get; } = subscription;

    public async ValueTask DisposeAsync()
    {
        await DeleteAsync(CancellationToken.None);
        Subscription.Dispose();
    }

    public async Task DeleteAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Subscription.DeleteAsync(true, cancellationToken);
        }
        catch (Exception ex) when (ex is ServiceResultException or InvalidOperationException)
        {
            // The caller may already be handling connection/session loss. Disposal remains best-effort.
        }
    }
}
