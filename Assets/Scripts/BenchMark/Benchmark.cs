using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public abstract class Benchmark
{
	private string benchDirName;

	public Benchmark(string dirName){
		benchDirName = dirName;
		createDirectory ();
	}

	private Dictionary<string, Analyzer> analizers = new Dictionary<string, Analyzer>();

	protected void addAnalysis(string filePrefix, Analyzer analizer){ analizers.Add (filePrefix, analizer);}
	protected abstract void initBenchmark();
	protected abstract void endBenchmark();

	public void runBenchmark(int n){
		Debug.Log("Benchmark '"+benchDirName+"' started to run.");
		initBenchmark ();
		foreach (string key in analizers.Keys) {
			Debug.Log("Analysis '"+key+"' have started.");
			analizers[key].run (n);
			Debug.Log("Analysis have finished.");
			writeResults(key);
			Debug.Log("Results written successfully.");
		}
		endBenchmark ();
		Debug.Log("Benchmark '"+benchDirName+"' analysis have finised.");
	}

	private void writeResults(string filePrefix){
		Analyzer analyzer = analizers [filePrefix];
		StreamWriter sw = File.CreateText ("Assets/Analyzer/"+benchDirName+"/"+filePrefix+".txt");
		sw.WriteLine ("Mean, StDev, Max, Min, N°Operations");
		sw.Write (analyzer.getMean() + ", ");
		sw.Write (analyzer.getStDev() + ", ");
		sw.Write (analyzer.getMaxTime() + ", ");
		sw.Write (analyzer.getMinTime() + ", ");
		sw.WriteLine (analyzer.getAnalizedOperations());
		sw.Close ();

	}

	private void createDirectory(){
		Directory.CreateDirectory ("Assets/Analyzer/"+benchDirName);
	}

}

