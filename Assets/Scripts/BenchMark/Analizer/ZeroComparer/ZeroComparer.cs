using UnityEngine;
using System.Collections;

public abstract class ZeroComparer : Analyzer
{
	Random random = new Random();
	float nextNumber;
	int count = -1;
	float[] sign = new float[]{-1, 1};

	protected override void initTest ()
	{
		Random.seed = 1321;
		prepateNextOperation ();
	}

	protected override void prepateNextOperation ()
	{
		count++;
		nextNumber = Random.value;
		while(nextNumber == 0)
			nextNumber = Random.value;
		nextNumber = sign[Random.Range(0, 2)] *(Random.value / nextNumber);
	}
	protected override void runOperation ()
	{
		compare (nextNumber);
	}

	protected override bool finishTestCondition ()
	{
		return count > 500;
	}

	protected abstract int compare (float n);

}

