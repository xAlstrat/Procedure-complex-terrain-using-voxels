using UnityEngine;
using System.Collections;

public class MSBBenchmark : Benchmark
{
	public MSBBenchmark(string dirName)
	:base(dirName){
	}

	protected override void initBenchmark ()
	{
		addAnalysis ("32BIT_Linear", new LinearMSBAnalizer(31));
		addAnalysis ("32BIT_Binary", new BinaryMSBAnalizer(31));
		addAnalysis ("32BIT_Binary2", new BinaryMSB2Analizer(31));
		addAnalysis ("32BIT_DeBruijn", new DeBruijnMSBAnalyzer(31));
		addAnalysis ("12BIT_Linear", new LinearMSBAnalizer(12));
		addAnalysis ("12BIT_Binary", new BinaryMSBAnalizer(12));
		addAnalysis ("12BIT_Binary2", new BinaryMSB2Analizer(12));
		addAnalysis ("12BIT_DeBruijn", new DeBruijnMSBAnalyzer(12));
	}

	protected override void endBenchmark ()
	{

	}
}

