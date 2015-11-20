using UnityEngine;
using System.Collections;

public class BitZeroComparer : ZeroComparer
{
 	protected override int compare (float n)
	{
		return (int)n >> 31;
	}
}

