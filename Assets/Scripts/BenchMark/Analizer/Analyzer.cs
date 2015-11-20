using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Analyzer
{
	private int testedOperations = 0;
	private List<float> times = new List<float> ();

	private float mean;
	private float stDev;
	private float minTime;
	private float maxTime;
	private int minTimeIndex = -1;
	private int maxTimeIndex = -1;

	protected virtual void initTest (){}
	protected virtual void endTest (){}
	protected virtual void prepateNextOperation(){}
	protected abstract void runOperation();
	protected abstract bool finishTestCondition ();

	public void run ( int n){
		float t1, t2, c, rep;
		initTest ();
		while (true) {
			rep = n;
			while(rep-- > 0){
				t1 = Time.realtimeSinceStartup;
				runOperation ();
				t2 = Time.realtimeSinceStartup;
				addTime(t2-t1);
			}
			prepateNextOperation();
			if(finishTestCondition())
				break;

		}
		endTest ();
		calculateResults ();
	}

	private void addTime(float dt){
		testedOperations++;
		times.Add (dt);
	}

	private void calculateResults(){
		calculateMean ();
		calculateStDev ();
		calculateMinAndMax ();
	}

	private void calculateMean(){
		float total = 0;
		foreach (float dt in times)
			total += dt;
		mean = total / testedOperations;
	}

	private void calculateStDev(){
		float total = 0;
		float val;
		foreach (float dt in times) {
			val = dt - mean;
			total += (val * val);
		}
		stDev = Mathf.Sqrt(total / testedOperations);
	}

	private void calculateMinAndMax(){
		int index = 0;
		minTime = times [index];
		maxTime = times [index];
		minTimeIndex = index;
		maxTimeIndex = index;
		foreach (float dt in times) {
			if(maxTime < dt){
				maxTime = dt;
				maxTimeIndex = index;
			}
			else{
				minTime = minTime>dt?dt:minTime;
				minTimeIndex = index;
			}
			index++;
		}
	}

	public float getMean(){ return mean;}
	public float getStDev(){ return stDev;}
	public float getMaxTime(){ return maxTime;}
	public float getMinTime(){ return minTime;}
	public int getMaxTimeIndex(){ return maxTimeIndex;}
	public int getMinTimeIndex(){ return minTimeIndex;}
	public int getAnalizedOperations(){ return testedOperations;}
}

