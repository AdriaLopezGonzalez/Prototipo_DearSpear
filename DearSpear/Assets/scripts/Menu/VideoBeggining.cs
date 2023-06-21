using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoBeggining : MonoBehaviour
{
    VideoPlayer _video;
    float timer;

    void Start()
    {
        _video = gameObject.GetComponent<VideoPlayer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if ((timer > _video.length) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainLevel");
        }
    }
}
