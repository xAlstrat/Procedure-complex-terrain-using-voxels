using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MSBTester : MonoBehaviour
{
	private readonly Dictionary<int, int> res = new Dictionary<int, int>();
	// Use this for initialization
	void Start ()
	{
		for (int i=0; i<31; i++) {
			res.Add(1<<i,i);
		}

		Debug.Log("Linear MSB Test");
		test (new LinearMSBAnalizer(32));
		Debug.Log("Binary MSB Test");
		test (new BinaryMSBAnalizer(32));
		Debug.Log("Binary 2 MSB Test");
		test (new BinaryMSB2Analizer(32));
		Debug.Log("DeBruijn MSB Test");
		test (new DeBruijnMSBAnalyzer(32));
	}

	private void test(MSBAnalyzer analyzer){
		for (int i=0; i<31; i++) {
			int n = 1<<i;
			int index = analyzer.MSB(n);
			if(index != res[n]){
				Debug.Log("Test Failed. Expected: "+res[n]+", Given: "+index);
				return;
			}
		}
	}
}

