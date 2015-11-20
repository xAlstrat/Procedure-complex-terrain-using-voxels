using UnityEngine;
using System.Collections;

public class BinaryMSB2Analizer : MSBAnalyzer
{	
	public BinaryMSB2Analizer(int max)
	:base(max){
	}

	private static readonly int[] MSBTable = new int[]{0,1,2,2,3,3,3,3,4,4,4,4,4,4,4,4};
	
	public override int MSB(int bits){
		int index = -1;
		int r;
		if ((r = bits >> 16) > 0) {
			bits = r;
			index += 16;
		}
		if ((r = bits >> 8) > 0) {
			bits = r;
			index += 8;
		}
		if ((r = bits >> 4) > 0) {
			bits = r;
			index += 4;
		}
		return index + MSBTable [bits];
	}
}

