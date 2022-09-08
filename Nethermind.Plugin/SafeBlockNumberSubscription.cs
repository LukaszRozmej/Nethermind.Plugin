using Nethermind.JsonRpc;
using Nethermind.JsonRpc.Modules.Subscribe;

namespace Nethermind.Plugin;

public class SafeBlockNumberSubscription : Subscription
{
    private readonly SubscriptionBlockTree _subscriptionBlockTree;

    public SafeBlockNumberSubscription(IJsonRpcDuplexClient jsonRpcDuplexClient, SubscriptionBlockTree subscriptionBlockTree) : base(jsonRpcDuplexClient)
    {
        _subscriptionBlockTree = subscriptionBlockTree;
        _subscriptionBlockTree.SafeBlockHashChanged += OnSafeBlockHashChanged;
    }

    private void OnSafeBlockHashChanged(object? sender, HashEventArgs e)
    {
        ScheduleAction(() => TryPublishEvent(e));
    }

    private void TryPublishEvent(HashEventArgs e)
    {
        JsonRpcResult jsonRpcResult = CreateSubscriptionMessage(e.Hash);
        JsonRpcDuplexClient.SendJsonRpcResult(jsonRpcResult);
    }

    public override string Type => "safeBlockNumber";

    public override void Dispose()
    {
        _subscriptionBlockTree.SafeBlockHashChanged -= OnSafeBlockHashChanged;
        base.Dispose();
    }
}