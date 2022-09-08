using Nethermind.Api;
using Nethermind.Api.Extensions;

namespace Nethermind.Plugin;

public class Plugin : INethermindPlugin
{
    private INethermindApi _api = null!;
    private SubscriptionBlockTree _subscriptionBlockTree = null!;

    public ValueTask DisposeAsync()
    {
        return default;
    }

    public Task Init(INethermindApi nethermindApi)
    {
        _api = nethermindApi;
        _subscriptionBlockTree = new SubscriptionBlockTree(_api.BlockTree!);
        _api.BlockTree = _subscriptionBlockTree;
        return Task.CompletedTask;
    }

    public Task InitNetworkProtocol()
    {
        return Task.CompletedTask;
    }

    public Task InitRpcModules()
    {
        _api.SubscriptionFactory.RegisterSubscriptionType("safeBlockNumber", c => new SafeBlockNumberSubscription(c, _subscriptionBlockTree));
        return Task.CompletedTask;
    }

    public string Name => "Subscription plugin";
    public string Description => "Showcase plugin";
    public string Author => "Łukasz Rozmej";
}