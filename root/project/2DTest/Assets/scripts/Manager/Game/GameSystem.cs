using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : SingletonMonoBehaviour<GameSystem>  
{

	private bool isFirstSetting=false; 
	private bool isSwitchScene=false;

	public Text text;
	public Timer_Base timer;

	public void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}
	}

	// Use this for initialization
	void Start ()
	{
		GenericSystem.CreateGenericSystem();
		CameraFade.StartAlphaFade(Color.black,true,1,0,()=>{isFirstSetting=true;});
	}

	// Update is called once per frame
	void Update () 
	{
		if(isFirstSetting)
		{
			text.text=timer.GetNowTime().ToString();
		}

		if(timer.GetNowTime()>=30)
		{
			EndScene();
		}
	}

	public void EndScene()
	{
		if(!isSwitchScene)
		{
			CameraFade.StartAlphaFade(Color.black,false,1,0,()=>{SceneManager.LoadScene("Title");});
			isSwitchScene=true;
		}
	}
}
