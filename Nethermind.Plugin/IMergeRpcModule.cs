using Nethermind.Core.Crypto;
using Nethermind.JsonRpc;
using Nethermind.JsonRpc.Modules;

namespace Nethermind.Plugin;

[RpcModule("Merge")]
public interface IMergeRpcModule : IRpcModule
{
    ResultWrapper<Keccak?> eth_getSafeBlock();
}