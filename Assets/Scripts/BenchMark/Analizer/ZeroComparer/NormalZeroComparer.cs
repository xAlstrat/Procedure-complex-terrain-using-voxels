using UnityEngine;
using System.Collections;

public class NormalZeroComparer : ZeroComparer
{
	protected override int compare (float n)
	{
		return n>=0?1:0;
	}

}

