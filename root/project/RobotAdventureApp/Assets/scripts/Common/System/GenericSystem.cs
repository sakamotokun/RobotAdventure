using UnityEngine;
using System.Collections;

public class GenericSystem : SingletonMonoBehaviour<GenericSystem>
{

	// Use this for initialization
	void Start () 
	{
		if(this != Instance)
		{
			Destroy(this.gameObject);
			return;
		}

		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	static public void CreateGenericSystem()
	{
		//すでに存在する場合は生成しない
		if(GenericSystem.Instance){return;}

		// プレハブを取得
		GameObject prefab = (GameObject)Resources.Load ("Prefabs/Generic/DontDestroyManager");

		if(!prefab){return;}

		// プレハブからインスタンスを生成
		Instantiate (prefab, Vector3.zero, Quaternion.identity);
	}

}
