//#define  FROM_PERSISTENT

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;




// AssetBundleからVideoClipを再生するサンプル
// 注意事項
// Unity2018.1.x以降でのみ動作します。
// Unityの仮想ファイルシステムのパスからは動画を再生出来ない為、
// Application.persistentDataPathへAssetBundleをコピーしています。
// 雑な作りな為、Android,iOSのみ対応させています。
public class Sample3 : MonoBehaviour {
    [SerializeField] VideoPlayer videoPlayer;



    // Use this for initialization
    System.Collections.IEnumerator Start () {
#if FROM_PERSISTENT
        // Application.persistentDataPath から読み込む
#if UNITY_ANDROID
        var src = Application.streamingAssetsPath + "/AssetBundle/Android/sample";
        var dst = Application.persistentDataPath + "/AssetBundle/Android/sample";
        var dir = Application.persistentDataPath + "/AssetBundle/Android";
#else
        var src = Application.streamingAssetsPath + "/AssetBundle/iOS/sample";
        var dst = Application.persistentDataPath + "/AssetBundle/iOS/sample";
        var dir = Application.persistentDataPath + "/AssetBundle/iOS";
#endif
        if (System.IO.File.Exists(dst) == false)
        {
            if (System.IO.Directory.Exists(dir) == false)
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(src))
            { 
                yield return unityWebRequest.SendWebRequest();
                System.IO.File.WriteAllBytes(dst, unityWebRequest.downloadHandler.data);
            }
        }
#else
        // Application.streamingAssetsPath から読み込む
#if UNITY_ANDROID
        var dst = Application.streamingAssetsPath + "/AssetBundle/Android/sample";
#else
        var dst = Application.streamingAssetsPath + "/AssetBundle/iOS/sample";
#endif
#endif
        var assetBundle = AssetBundle.LoadFromFile(dst);
        videoPlayer.clip = assetBundle.LoadAsset<VideoClip>("Sample.mp4");
        videoPlayer.prepareCompleted += PrepareCB;
        videoPlayer.Prepare();
        yield return null;
	}
	

	// Update is called once per frame
	void Update () {
	}

    void PrepareCB(VideoPlayer vc)
    {
        videoPlayer.prepareCompleted -= PrepareCB;
        vc.Play();
    }
}
