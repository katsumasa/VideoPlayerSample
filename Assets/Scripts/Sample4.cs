using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;


// シークバーの実装例
public class Sample4 : MonoBehaviour {

    delegate void Task();

    
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] string url;
    string assetFilePath;
    Task m_task;
    bool isChanged;


    // Use this for initialization
    void Start () {
        isChanged = false;
        assetFilePath = Application.persistentDataPath + "/movie.mp4";
        if (System.IO.File.Exists(assetFilePath) == true)
        {
            InitVideoPlayer();
        }
        else
        {
            StartCoroutine(DownLoadMP4());
        }
	}
	

	// Update is called once per frame
	void Update () {
        if(m_task != null)
        {
            m_task();
        }
	}


    void PlayTask()
    {
        if ((videoPlayer.isPlaying)||(videoPlayer.isPrepared))
        {
#if UNITY_EDITOR
            if(Input.GetMouseButton(0))
#else
            if (Input.touchCount > 0)
#endif
            {
                videoPlayer.Pause();
                isChanged = false;
                m_task = PauseTask;
            }
            else
            {
                var value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
                slider.value = value;
            }
        }
    }


    void PauseTask()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton(0) == false)
#else
        if (Input.touchCount == 0)
#endif
        {
            if (isChanged)
            {
                var value = slider.value * videoPlayer.frameCount;
                videoPlayer.seekCompleted += SeekCompleted;
                videoPlayer.frame = (long)value;
                m_task = null;
            }
            else
            {
                videoPlayer.Play();
                m_task = PlayTask;
            }
        }
    }


    IEnumerator DownLoadMP4()
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
                InitVideoPlayer();
            }   
        }
    }


    private void InitVideoPlayer()
    {
        if (videoPlayer != null)
        {
            videoPlayer.url = assetFilePath;
            videoPlayer.prepareCompleted += PrepareCompleted;
            videoPlayer.Prepare();
        }
    }


    public void ChangeValueCB(float value)
    {
        isChanged = true;
    }


    private void SeekCompleted(VideoPlayer vp)
    {
        vp.seekCompleted -= SeekCompleted;
        vp.Play();
        m_task = PlayTask;
    }


    void PrepareCompleted(VideoPlayer vp)
    {
        videoPlayer.prepareCompleted -= PrepareCompleted;
        vp.Play();
        m_task = PlayTask;
    }
}
