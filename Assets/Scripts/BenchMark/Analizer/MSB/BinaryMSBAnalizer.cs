using UnityEngine;
using System.Collections;

public class BinaryMSBAnalizer : MSBAnalyzer
{	
	public BinaryMSBAnalizer(int max)
	:base(max){
	}

	private static readonly int[] MSBTable = new int[]{0,1,2,2,3,3,3,3,4,4,4,4,4,4,4,4};
	
	public override int MSB(int bits){
		int index = -1;
		if ((bits & 0xFFFF0000) > 0) {
			bits >>= 16;
			index += 16;
		}
		if ((bits & 0xFF00) > 0) {
			bits >>= 8;
			index += 8;
		}
		if ((bits & 0xF0) > 0) {
			bits >>= 4;
			index += 4;
		}
		return index + MSBTable [bits];
	}
}

