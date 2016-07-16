using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title_Manager : MonoBehaviour
{
	private bool isFirstSetting=false; 
	private bool isSwitchScene=false;

	// Use this for initialization
	void Start ()
	{
		CameraFade.StartAlphaFade(Color.black,true,1,0,()=>{isFirstSetting=true;});
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isFirstSetting||isSwitchScene){return;}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			isSwitchScene=true;
			CameraFade.StartAlphaFade(Color.black,false,1,0,()=>{SceneManager.LoadScene("Game");});
		}
	}
}
