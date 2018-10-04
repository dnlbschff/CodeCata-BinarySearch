public class BinarySearchRecursive : ISearchProvider
{
    public int FindKey(int searchKey, int[] array)
    {
        return FindRecursive(searchKey, array, 0);
    }

    private int FindRecursive(int searchKey, int[] array, int resultOffset)
    {
        if (array.Length < 1)
        {
            return -1;
        }

        var lookupIndex = array.Length - 1 >> 1;
        var item = array[lookupIndex];

        if (item == searchKey)
        {
            return resultOffset + lookupIndex;
        }

        if (searchKey < item)
        {
            array = array.GetRange(0, lookupIndex);
        }
        else
        {
            array = array.GetRange(lookupIndex + 1, array.Length - (lookupIndex + 1));
            resultOffset += lookupIndex + 1;
        }

        return FindRecursive(searchKey, array, resultOffset);
    }
}