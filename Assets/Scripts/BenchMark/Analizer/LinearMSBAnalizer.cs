using UnityEngine;
using System.Collections;

public class LinearMSBAnalizer : MSBAnalyzer
{

	public LinearMSBAnalizer(int max)
	:base(max){
	}

	public override int MSB(int bits){
		int index = -1;
		while (bits>0) {
			bits >>= 1;
			index++;
		}
		return index;
	}
}

