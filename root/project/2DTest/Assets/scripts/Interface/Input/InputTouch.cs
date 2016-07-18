//=================================================================================
//
// タッチ入力管理クラス(InputTouch.cs)
// 制作者 坂本友希
//
//=================================================================================
using UnityEngine;
using System.Collections;

///==========================================================
/// <summary>
/// タッチ入力管理クラス
/// </summary>
///==========================================================
public class InputTouch : SingletonMonoBehaviour<InputTouch> 
{
	private int CurrentActiveFingerID;
	private bool CanTouch;

	public void Awake()
	{

		if(this != Instance)
		{
			Destroy(this);
			return;
		}

		CanTouch=Input.touchSupported;

	}

	void Update()
	{
		if(!CanTouch){return;}

		if(isTouch())
		{
			if(Input.touches[0].fingerId!=0)
			{
				CurrentActiveFingerID=Input.touches[0].fingerId;
			}
			
			else if(CurrentActiveFingerID!=0)
			{
				
				if(InputTouch.Instance.SerchIDwithFinger(CurrentActiveFingerID)==-1)
				{
					CurrentActiveFingerID=0;
				}
			}
		}
	}

	public bool isTouch()
	{
		if(!CanTouch){return (false);}

		return (Input.touchCount>=1);
	}

	public int TouchCnt()
	{
		if(!CanTouch){return (0);}

		return (Input.touchCount);
	}

	public TouchPhase TouchPhase_(int id)
	{
		if(!CanTouch){return (TouchPhase.Canceled);}

		if(isTouch())
		{
			if(Input.touchCount<id)
			{
				return (TouchPhase.Canceled);
			}
	
			return (Input.touches[id].phase);
		}

		return (TouchPhase.Canceled);
	}

	public Vector2 Position(int id)
	{
		if(!CanTouch){return (Vector2.zero);}

		if(isTouch())
		{
			if(Input.touchCount<id)
			{
				return (Vector2.zero);
			}
	
			return (Input.touches[id].position);
		}

		return (Vector2.zero);
	}

	public Vector2 DragMovement(int id)
	{
		if(!CanTouch){return (Vector2.zero);}

		if(isTouch())
		{
			if(Input.touchCount<id)
			{
				return (Vector2.zero);
			}

			return (Input.touches[id].deltaPosition);
		}

		return (Vector2.zero);
	}

	public int TapCnt(int id)
	{
		if(!CanTouch){return (0);}

		if(isTouch())
		{
			if(Input.touchCount<id)
			{
				return (0);
			}
			
			return (Input.touches[id].tapCount);
		}
		
		return (0);
	}

	public bool isDoubleTap(int id)
	{
		if(!CanTouch){return (false);}

		if(isTouch())
		{
			if(Input.touchCount<id)
			{
				return (false);
			}
			
			if(Input.touches[id].tapCount==2)
			{
				return (true);
			}
		}
		
		return (false);
	}

	public int SerchIDwithFinger(int id)
	{
		if(!CanTouch){return (0);}

		if(isTouch())
		{
			for(int i=0;i<Input.touchCount;i++)
			{
				if(Input.touches[i].fingerId==id)
				{
					return (i);
				}
			}

		}

		return (-1);
	}


	public int CurrentActiveFingerID_()
	{
		if(!CanTouch){return (0);}

		return (CurrentActiveFingerID);
	}

	public int CurrentActiveID_()
	{
		if(!CanTouch){return (0);}

		return (SerchIDwithFinger(CurrentActiveFingerID));
	}

}
