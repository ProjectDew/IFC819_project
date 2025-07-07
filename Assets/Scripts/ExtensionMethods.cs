using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static IList<T> Shuffle<T> (this IList<T> list)
	{
		Random random = new ();

		int count = list.Count;

		while (count > 1)
		{
			count--;

			int i =	random.Next (count + 1);

			T value = list[i];

			list[i] = list[count];
			list[count] = value;
		}

		return list;
	}
}
