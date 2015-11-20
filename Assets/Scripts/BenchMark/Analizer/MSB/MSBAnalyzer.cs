using UnityEngine;
using System.Collections;

public abstract class MSBAnalyzer : Analyzer
{
	private int maxBit;
	private int currentBit;

	public MSBAnalyzer(int max){
		maxBit = 1<<(max-1);
		currentBit = 1;
	}
	
	protected override bool finishTestCondition ()
	{
		return (maxBit & currentBit) > 0;
	}
	
	protected override void runOperation ()
	{
		MSB (currentBit);
	}
	
	protected override void prepateNextOperation ()
	{
		currentBit <<= 1;
	}
	
	public abstract int MSB (int bits);
}

