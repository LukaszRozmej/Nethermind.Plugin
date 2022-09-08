using Nethermind.Blockchain;
using Nethermind.Blockchain.Visitors;
using Nethermind.Core;
using Nethermind.Core.Crypto;
using Nethermind.Int256;

namespace Nethermind.Plugin;

public class SubscriptionBlockTree : IBlockTree
{
    private readonly IBlockTree _blockTreeImplementation;
    private Keccak? _safeBlockHash;

    public Keccak? SafeBlockHash
    {
        get => _safeBlockHash;
        private set
        {
            if (_safeBlockHash != value && value is not null)
            {
                _safeBlockHash = value;
                SafeBlockHashChanged?.Invoke(this, new HashEventArgs(_safeBlockHash));
            }
        }
    }

    public event EventHandler<HashEventArgs>? SafeBlockHashChanged;

    public SubscriptionBlockTree(IBlockTree blockTreeImplementation)
    {
        _blockTreeImplementation = blockTreeImplementation;
    }

    public void ForkChoiceUpdated(Keccak? finalizedBlockHash, Keccak? safeBlockBlockHash)
    {
        _blockTreeImplementation.ForkChoiceUpdated(finalizedBlockHash, safeBlockBlockHash);
        SafeBlockHash = safeBlockBlockHash;
    }

    #region Hide

    public Block? FindBlock(Keccak blockHash, BlockTreeLookupOptions options)
    {
        return _blockTreeImplementation.FindBlock(blockHash, options);
    }

    public Block? FindBlock(long blockNumber, BlockTreeLookupOptions options)
    {
        return _blockTreeImplementation.FindBlock(blockNumber, options);
    }

    public BlockHeader? FindHeader(Keccak blockHash, BlockTreeLookupOptions options)
    {
        return _blockTreeImplementation.FindHeader(blockHash, options);
    }

    public BlockHeader? FindHeader(long blockNumber, BlockTreeLookupOptions options)
    {
        return _blockTreeImplementation.FindHeader(blockNumber, options);
    }

    public Keccak? FindBlockHash(long blockNumber)
    {
        return _blockTreeImplementation.FindBlockHash(blockNumber);
    }

    public bool IsMainChain(BlockHeader blockHeader)
    {
        return _blockTreeImplementation.IsMainChain(blockHeader);
    }

    public bool IsMainChain(Keccak blockHash)
    {
        return _blockTreeImplementation.IsMainChain(blockHash);
    }

    public BlockHeader FindBestSuggestedHeader()
    {
        return _blockTreeImplementation.FindBestSuggestedHeader();
    }

    public Keccak HeadHash => _blockTreeImplementation.HeadHash;

    public Keccak GenesisHash => _blockTreeImplementation.GenesisHash;

    public Keccak? PendingHash => _blockTreeImplementation.PendingHash;

    public Keccak? FinalizedHash => _blockTreeImplementation.FinalizedHash;

    public Keccak? SafeHash => _blockTreeImplementation.SafeHash;

    public Block? Head => _blockTreeImplementation.Head;

    public long? BestPersistedState
    {
        get => _blockTreeImplementation.BestPersistedState;
        set => _blockTreeImplementation.BestPersistedState = value;
    }

    public AddBlockResult Insert(BlockHeader header, BlockTreeInsertHeaderOptions headerOptions = BlockTreeInsertHeaderOptions.None)
    {
        return _blockTreeImplementation.Insert(header, headerOptions);
    }

    public AddBlockResult Insert(Block block, BlockTreeInsertBlockOptions insertBlockOptions = BlockTreeInsertBlockOptions.None, BlockTreeInsertHeaderOptions insertHeaderOptions = BlockTreeInsertHeaderOptions.None)
    {
        return _blockTreeImplementation.Insert(block, insertBlockOptions, insertHeaderOptions);
    }

    public void Insert(IEnumerable<Block> blocks)
    {
        _blockTreeImplementation.Insert(blocks);
    }

    public void UpdateHeadBlock(Keccak blockHash)
    {
        _blockTreeImplementation.UpdateHeadBlock(blockHash);
    }

    public AddBlockResult SuggestBlock(Block block, BlockTreeSuggestOptions options = BlockTreeSuggestOptions.ShouldProcess)
    {
        return _blockTreeImplementation.SuggestBlock(block, options);
    }

    public ValueTask<AddBlockResult> SuggestBlockAsync(Block block, BlockTreeSuggestOptions options = BlockTreeSuggestOptions.ShouldProcess)
    {
        return _blockTreeImplementation.SuggestBlockAsync(block, options);
    }

    public AddBlockResult SuggestHeader(BlockHeader header)
    {
        return _blockTreeImplementation.SuggestHeader(header);
    }

    public bool IsKnownBlock(long number, Keccak blockHash)
    {
        return _blockTreeImplementation.IsKnownBlock(number, blockHash);
    }

    public bool IsKnownBeaconBlock(long number, Keccak blockHash)
    {
        return _blockTreeImplementation.IsKnownBeaconBlock(number, blockHash);
    }

    public bool WasProcessed(long number, Keccak blockHash)
    {
        return _blockTreeImplementation.WasProcessed(number, blockHash);
    }

    public void UpdateMainChain(IReadOnlyList<Block> blocks, bool wereProcessed, bool forceHeadBlock = false)
    {
        _blockTreeImplementation.UpdateMainChain(blocks, wereProcessed, forceHeadBlock);
    }

    public void MarkChainAsProcessed(IReadOnlyList<Block> blocks)
    {
        _blockTreeImplementation.MarkChainAsProcessed(blocks);
    }

    public Task Accept(IBlockTreeVisitor blockTreeVisitor, CancellationToken cancellationToken)
    {
        return _blockTreeImplementation.Accept(blockTreeVisitor, cancellationToken);
    }

    public UInt256? BackFillTotalDifficulty(long startNumber, long endNumber, long batchSize, UInt256? startingTotalDifficulty = null)
    {
        return _blockTreeImplementation.BackFillTotalDifficulty(startNumber, endNumber, batchSize, startingTotalDifficulty);
    }

    public (BlockInfo? Info, ChainLevelInfo? Level) GetInfo(long number, Keccak blockHash)
    {
        return _blockTreeImplementation.GetInfo(number, blockHash);
    }

    public ChainLevelInfo? FindLevel(long number)
    {
        return _blockTreeImplementation.FindLevel(number);
    }

    public BlockInfo FindCanonicalBlockInfo(long blockNumber)
    {
        return _blockTreeImplementation.FindCanonicalBlockInfo(blockNumber);
    }

    public Keccak FindHash(long blockNumber)
    {
        return _blockTreeImplementation.FindHash(blockNumber);
    }

    public BlockHeader[] FindHeaders(Keccak hash, int numberOfBlocks, int skip, bool reverse)
    {
        return _blockTreeImplementation.FindHeaders(hash, numberOfBlocks, skip, reverse);
    }

    public BlockHeader FindLowestCommonAncestor(BlockHeader firstDescendant, BlockHeader secondDescendant, long maxSearchDepth)
    {
        return _blockTreeImplementation.FindLowestCommonAncestor(firstDescendant, secondDescendant, maxSearchDepth);
    }

    public void DeleteInvalidBlock(Block invalidBlock)
    {
        _blockTreeImplementation.DeleteInvalidBlock(invalidBlock);
    }

    public void LoadLowestInsertedBeaconHeader()
    {
        _blockTreeImplementation.LoadLowestInsertedBeaconHeader();
    }

    public int DeleteChainSlice(in long startNumber, long? endNumber = null)
    {
        return _blockTreeImplementation.DeleteChainSlice(in startNumber, endNumber);
    }

    public bool IsBetterThanHead(BlockHeader? header)
    {
        return _blockTreeImplementation.IsBetterThanHead(header);
    }

    public void UpdateBeaconMainChain(BlockInfo[]? blockInfos, long clearBeaconMainChainStartPoint)
    {
        _blockTreeImplementation.UpdateBeaconMainChain(blockInfos, clearBeaconMainChainStartPoint);
    }

    public ulong ChainId => _blockTreeImplementation.ChainId;

    public BlockHeader? Genesis => _blockTreeImplementation.Genesis;

    public BlockHeader? BestSuggestedHeader => _blockTreeImplementation.BestSuggestedHeader;

    public Block? BestSuggestedBody => _blockTreeImplementation.BestSuggestedBody;

    public BlockHeader? BestSuggestedBeaconHeader => _blockTreeImplementation.BestSuggestedBeaconHeader;

    public BlockHeader? LowestInsertedHeader => _blockTreeImplementation.LowestInsertedHeader;

    public long? LowestInsertedBodyNumber
    {
        get => _blockTreeImplementation.LowestInsertedBodyNumber;
        set => _blockTreeImplementation.LowestInsertedBodyNumber = value;
    }

    public BlockHeader? LowestInsertedBeaconHeader
    {
        get => _blockTreeImplementation.LowestInsertedBeaconHeader;
        set => _blockTreeImplementation.LowestInsertedBeaconHeader = value;
    }

    public long BestKnownNumber => _blockTreeImplementation.BestKnownNumber;

    public long BestKnownBeaconNumber => _blockTreeImplementation.BestKnownBeaconNumber;

    public bool CanAcceptNewBlocks => _blockTreeImplementation.CanAcceptNewBlocks;

    public event EventHandler<BlockEventArgs>? NewBestSuggestedBlock
    {
        add => _blockTreeImplementation.NewBestSuggestedBlock += value;
        remove => _blockTreeImplementation.NewBestSuggestedBlock -= value;
    }

    public event EventHandler<BlockEventArgs>? NewSuggestedBlock
    {
        add => _blockTreeImplementation.NewSuggestedBlock += value;
        remove => _blockTreeImplementation.NewSuggestedBlock -= value;
    }

    public event EventHandler<BlockReplacementEventArgs>? BlockAddedToMain
    {
        add => _blockTreeImplementation.BlockAddedToMain += value;
        remove => _blockTreeImplementation.BlockAddedToMain -= value;
    }

    public event EventHandler<BlockEventArgs>? NewHeadBlock
    {
        add => _blockTreeImplementation.NewHeadBlock += value;
        remove => _blockTreeImplementation.NewHeadBlock -= value;
    }

    #endregion
}