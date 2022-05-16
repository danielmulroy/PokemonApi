using PokemonApi.PokemonDetailsProvider.ApiCache;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PokemonApi.Test;

public class CacheTests
{
    private LeastRecentlyUsedCache<int, string> _cacheUnderTest;

    [Fact]
    public void CacheWhenFull_RemovesOldest()
    {
        _cacheUnderTest = new LeastRecentlyUsedCache<int, string>(2);

        _cacheUnderTest.Put(1, "One");
        _cacheUnderTest.Put(2, "Two");
        _cacheUnderTest.Put(3, "Three");

        Assert.True(_cacheUnderTest.TryGet(3, out _));
        Assert.True(_cacheUnderTest.TryGet(2, out _));
        Assert.False(_cacheUnderTest.TryGet(1, out _));
    }

    [Fact]
    public void CacheOverwrites_IfExists()
    {
        _cacheUnderTest = new LeastRecentlyUsedCache<int, string>(1);

        _cacheUnderTest.Put(1, "One");
        var oneButFancy = "JustTheSingleOfAGivenUnit";
        _cacheUnderTest.Put(1, oneButFancy);

        _cacheUnderTest.TryGet(1, out var one);
        Assert.Equal(oneButFancy, one);
    }

    [Fact]
    public void Cache_HandlesParallelThreadsSafely()
    {
        _cacheUnderTest = new LeastRecentlyUsedCache<int, string>(5);

        var listOfKvps = new List<(int Key, string Val)>
            { (1, "One"), (2, "Two"), (3, "Three"), (4, "Four"), (5, "Five"),
              (6, "Six"), (7, "Seven"), (8, "Eight"), (9, "Nine"), (10, "Ten")};

        var listOfKeys = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Task.Run(() => Parallel.ForEach(listOfKvps, item => _cacheUnderTest.Put(item.Key, item.Val)));
        Task.Run(() => Parallel.ForEach(listOfKeys, item => _cacheUnderTest.TryGet(item, out _)));
        Task.Run(() => Parallel.ForEach(listOfKvps, item => _cacheUnderTest.Put(item.Key, item.Val)));
        Task.Run(() => Parallel.ForEach(listOfKeys, item => _cacheUnderTest.TryGet(item, out _)));
    }

}