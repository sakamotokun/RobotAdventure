using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingObject : MonoBehaviour 
{
	public Image myTrans;
	public float Interval;
	private float ypos;
	private float xpos;
	private bool isActive;
	public bool isLength;

	// Use this for initialization
	void Start () 
	{
		Active();
		ypos=myTrans.transform.position.y;
		xpos=myTrans.transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isActive)
		{
			float l_pos= Mathf.Sin (Time.frameCount / Interval);;
			Vector3 vector;
			if(isLength)
			{
				ypos+=l_pos;
				vector = new Vector3(myTrans.transform.position.x,ypos,myTrans.transform.position.z);
			}
			else
			{
				xpos+=l_pos;
				vector = new Vector3(xpos,myTrans.transform.position.y,myTrans.transform.position.z);
			}
			myTrans.transform.position = vector;
		}
	}

	public void Active(){isActive=true;}
	public void UnActive(){isActive=false;}

}
