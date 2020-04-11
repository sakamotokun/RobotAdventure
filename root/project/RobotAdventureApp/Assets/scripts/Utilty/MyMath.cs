using UnityEngine;
using System.Collections;

static public class MyMath
{
	static public float MapValues(float x,float inMin,float inMax,float outMin,float outMax)
	{
		return ((x-inMin)*(outMax-outMin)/(inMax-inMin)+outMin);
	}

	static public float Fps()
	{
		return (1.0f / Time.deltaTime);
	}
}
