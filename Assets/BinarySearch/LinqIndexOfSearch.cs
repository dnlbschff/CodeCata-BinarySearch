using System.Linq;

public class LinqIndexOfSearch : ISearchProvider
{
    public int FindKey(int searchKey, int[] array)
    {
        return array.ToList().IndexOf(searchKey);
    }
}