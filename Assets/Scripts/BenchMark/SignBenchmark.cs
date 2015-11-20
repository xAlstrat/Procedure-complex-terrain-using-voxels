using UnityEngine;
using System.Collections;

public class SignBenchmark : Benchmark
{
	public SignBenchmark(string name):base(name){}

	protected override void initBenchmark ()
	{
		addAnalysis ("BitSign", new BitZeroComparer ());
		addAnalysis("NormalSign", new NormalZeroComparer());
	}

	protected override void endBenchmark ()	{}
}

