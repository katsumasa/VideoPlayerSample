using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;


// 動画ファイルをApplication.persistentDataPathへ保存してから再生するサンプル
public class Sample2 : MonoBehaviour {

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string url;
    string assetFilePath;


    // Use this for initialization
    void Start() {
        assetFilePath = Application.persistentDataPath + "/movie.mp4";
        if (System.IO.File.Exists(assetFilePath) == true) {
            videoPlayer.url = assetFilePath;
            videoPlayer.Play();
        } else {
            StartCoroutine(GetMP4());
        }
	}
	

	// Update is called once per frame
	void Update () {		
	}


    IEnumerator GetMP4()
    {        
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                
                File.WriteAllBytes(assetFilePath, www.downloadHandler.data);                
                videoPlayer.url = assetFilePath;                
                videoPlayer.Play();
            }
        }
    }
}
