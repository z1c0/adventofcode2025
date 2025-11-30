namespace aoc;

public static class Extensions
{
	public static T RemoveFirst<T>(this HashSet<T> hashSet)
	{
		var t = hashSet.First();
		hashSet.Remove(t);
		return t;
	}

	public static T RemoveFirst<T>(this IList<T> list)
	{
		var t = list.First();
		list.Remove(t);
		return t;
	}
	
	public static void InsertBefore<T>(this IList<T> list, T existingItem, T newItem)
	{
		var index = GetIndex(list, existingItem);
		list.Insert(index, newItem);
	}

	public static void InsertAfter<T>(this IList<T> list, T existingItem, T newItem)
	{
		var index = GetIndex(list, existingItem);
		list.Insert(index + 1, newItem);
	}

	private static int GetIndex<T>(IList<T> list, T item)
	{
		var index = list.IndexOf(item);
		if (index == -1)
		{
			throw new ArgumentException("The item was not found in the list.");
		}
		return index;
	}
	
	public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
	{
		foreach (var t in items)
		{
			hashSet.Add(t);
		}
	}
	
	public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> valueFactory)
	{
		if (!dict.TryGetValue(key, out var value))
		{
			value = valueFactory();
			dict[key] = value;
		}
		return value;
	}

}