using UnityEngine;
using System.Collections;

public class Pair<T1, T2>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }

	internal Pair(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}
}

