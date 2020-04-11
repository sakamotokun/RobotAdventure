using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingObject : MonoBehaviour
{
	public float Interval;
	public Image myTrans;

	private float InvisibleMax=1;
	private float InvisibleMin=0;
	private float InvisibleNow;
	private float TimeCnt;
	private bool isTrue;
	private bool isActive;

	// Use this for initialization
	void Start ()
	{
		isActive=true;
		isTrue=true;
		InvisibleNow=myTrans.color.a;
		TimeCnt=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isActive)
		{
			float baseNum=1;
			if(!isTrue){baseNum*=-1;}

			TimeCnt+=baseNum;

			if(TimeCnt>Interval||TimeCnt<0)
			{
				TimeCnt-=baseNum;
				isTrue=!isTrue;
			}

			InvisibleNow=MyMath.MapValues(TimeCnt,0,Interval,InvisibleMin,InvisibleMax);
			myTrans.color=new Color(myTrans.color.r,myTrans.color.g,myTrans.color.b,InvisibleNow);
		}
	}

	public void Active()
	{
		isActive=true;
		TimeCnt=0;
		
		InvisibleNow=MyMath.MapValues(TimeCnt,0,Interval,InvisibleMin,InvisibleMax);
		myTrans.color=new Color(myTrans.color.r,myTrans.color.g,myTrans.color.b,InvisibleNow);
	}

	public void UnActive()
	{
		isActive=false;
		TimeCnt=0;
		
		InvisibleNow=MyMath.MapValues(Interval,0,Interval,InvisibleMin,InvisibleMax);
		myTrans.color=new Color(myTrans.color.r,myTrans.color.g,myTrans.color.b,InvisibleNow);
	}
}
