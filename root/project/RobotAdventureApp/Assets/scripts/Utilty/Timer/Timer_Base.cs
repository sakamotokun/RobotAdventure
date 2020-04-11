using UnityEngine;
using System.Collections;

public class Timer_Base : MonoBehaviour 
{
	private float NowTimeFloat =0;
	private int NowTime=0;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		NowTimeFloat+=Time.deltaTime;

		if(NowTimeFloat>=1.0f)
		{
			NowTime++;

			NowTimeFloat-=1.0f;
		}
	}

	public int GetNowTime()
	{
		return (NowTime);
	}

	public static Timer_Base Create(GameObject obj)
	{
		Timer_Base Out=obj.AddComponent<Timer_Base>();

		return Out;
	}
}
