using UnityEngine;
using System.Collections;

public class DeBruijnMSBAnalyzer : MSBAnalyzer
{	
	public DeBruijnMSBAnalyzer(int max)
	:base(max){
	}

	private static readonly int[] MultiplyDeBruijnBitPosition2 = new int[32] 
	{
		0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 
		31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9
	};


	
	public override int MSB(int bits){
		return MultiplyDeBruijnBitPosition2[(uint)(bits * 0x077CB531U) >> 27];
	}
}

