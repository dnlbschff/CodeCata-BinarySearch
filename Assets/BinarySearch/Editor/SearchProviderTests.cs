using NUnit.Framework;

[TestFixture]
public abstract class SearchProviderTests
{
    private ISearchProvider _searchProvider;

    protected abstract ISearchProvider CreateSearchProvider();

    [SetUp]
    public void SetUp()
    {
        _searchProvider = CreateSearchProvider();
    }

    [Test]
    public void Test_FindKey_EmptyArray()
    {
        Assert.AreEqual(-1, _searchProvider.FindKey(3, new int[] { }));
    }

    [Test]
    public void Test_FindKey_SingleItem_NoMatch()
    {
        Assert.AreEqual(-1, _searchProvider.FindKey(3, new[] {1}));
    }

    [Test]
    public void Test_FindKey_SingleItem_Match()
    {
        Assert.AreEqual(0, _searchProvider.FindKey(1, new[] {1}));
    }

    [Test]
    [TestCase(1, 0)]
    [TestCase(3, 1)]
    [TestCase(5, 2)]
    public void Test_FindKey_ThreeItems_Match(int searchKey, int result)
    {
        Assert.AreEqual(result, _searchProvider.FindKey(searchKey, new[] {1, 3, 5}));
    }

    [Test]
    [TestCase(0, -1)]
    [TestCase(2, -1)]
    [TestCase(4, -1)]
    [TestCase(6, -1)]
    public void Test_FindKey_ThreeItems_NoMatch(int searchKey, int result)
    {
        Assert.AreEqual(result, _searchProvider.FindKey(searchKey, new[] {1, 3, 5}));
    }

    [Test]
    [TestCase(1, 0)]
    [TestCase(3, 1)]
    [TestCase(5, 2)]
    [TestCase(7, 3)]
    public void Test_FindKey_FourItems_Match(int searchKey, int result)
    {
        Assert.AreEqual(result, _searchProvider.FindKey(searchKey, new[] {1, 3, 5, 7}));
    }

    [Test]
    [TestCase(0, -1)]
    [TestCase(2, -1)]
    [TestCase(4, -1)]
    [TestCase(6, -1)]
    [TestCase(8, -1)]
    public void Test_FindKey_FourItems_NoMatch(int searchKey, int result)
    {
        Assert.AreEqual(result, _searchProvider.FindKey(searchKey, new[] {1, 3, 5, 7}));
    }

    [Test]
    public void Test_FindKey_Mario()
    {
        Assert.AreEqual(9,
            _searchProvider.FindKey(12, new[] {2, 4, 5, 5, 6, 8, 9, 10, 11, 12, 15, 17, 19, 22, 23, 24}));
    }
}

[TestFixture]
public class BinarySearchIterativeTests : SearchProviderTests
{
    protected override ISearchProvider CreateSearchProvider()
    {
        return new BinarySearchIterative();
    }
}

[TestFixture]
public class BinarySearchRecursiveTests : SearchProviderTests
{
    protected override ISearchProvider CreateSearchProvider()
    {
        return new BinarySearchRecursive();
    }
}

[TestFixture]
public class LinqIndexOfSearchTests : SearchProviderTests
{
    protected override ISearchProvider CreateSearchProvider()
    {
        return new LinqIndexOfSearch();
    }
}