using Nethermind.Core.Crypto;

namespace Nethermind.Plugin;

public class HashEventArgs
{
    public Keccak Hash { get; }

    public HashEventArgs(Keccak hash)
    {
        Hash = hash;
    }
}