using UnityEngine;
using System.Collections;

public class VerticalFlatNoiseFilter : NoiseFilter
{
	public float minHeight = 0;
	public float maxHeight = 0;

	public override bool filterAt (float x, float y, float z, float h)
	{
		y -= h;
		return (y < minHeight) || (y > maxHeight);
	}

	public override bool uniqueNoiseRequired ()
	{
		return true;
	}

	public void setHeightRange(float min_height, float max_height){
		setMinHeight (min_height);
		setMaxHeight (max_height);
	}

	public void setMaxHeight(float height){
		maxHeight = height;
	}

	public void setMinHeight(float height){
		minHeight = height;
	}

}

