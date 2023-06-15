using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnding : MonoBehaviour
{
    VideoPlayer _video;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
       _video = gameObject.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 5)
        {
            timer += Time.deltaTime;
        }
        
        if (!_video.isPlaying && timer > 5)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
