using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : SingletonMonoBehaviour<GameSystem>  
{

	private bool isFirstSetting=false; 
	private bool isSwitchScene=false;

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
		CameraFade.StartAlphaFade(Color.black,true,1,0,()=>{isFirstSetting=true;});
	}

	// Update is called once per frame
	void Update () 
	{

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
