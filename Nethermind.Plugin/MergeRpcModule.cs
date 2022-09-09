using Nethermind.Core.Crypto;
using Nethermind.JsonRpc;

namespace Nethermind.Plugin;

public class MergeRpcModule : IMergeRpcModule
{
    private readonly SubscriptionBlockTree _blockTree;

    public MergeRpcModule(SubscriptionBlockTree blockTree)
    {
        _blockTree = blockTree;
    }

    public ResultWrapper<Keccak?> eth_getSafeBlock()
    {
        return ResultWrapper<Keccak?>.Success(_blockTree.SafeBlockHash);
    }
}