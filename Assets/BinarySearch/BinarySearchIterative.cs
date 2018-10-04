public class BinarySearchIterative : ISearchProvider
{
    public int FindKey(int searchKey, int[] array)
    {
        var resultIndex = 0;
        while (array.Length > 0)
        {
            var lookupIndex = array.Length - 1 >> 1;
            var item = array[lookupIndex];

            if (item == searchKey)
            {
                return resultIndex + lookupIndex;
            }

            if (searchKey < item)
            {
                array = array.GetRange(0, lookupIndex);
            }
            else
            {
                array = array.GetRange(lookupIndex + 1, array.Length - (lookupIndex + 1));
                resultIndex += lookupIndex + 1;
            }
        }

        return -1;
    }
}