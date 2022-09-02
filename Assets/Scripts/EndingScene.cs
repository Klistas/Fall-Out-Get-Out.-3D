using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingScene : MonoBehaviour
{

    public VideoPlayer _videoPlayer;
    public VideoClip NextVideoClip;
    public VideoClip CurrentVideoClip;
    public Image LastImage;
    public GameObject LastText;
    public AudioClip LastBGM;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(VideoRoutine());
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);

        }
    }



    IEnumerator VideoRoutine()
    {
        _videoPlayer.clip = CurrentVideoClip;
        _videoPlayer.Play();
        yield return new WaitForSeconds(20f);

        for (float Percent = 0f; Percent <= 1; Percent += 0.0005f)
        {

            Percent += 0.0005f;

            LastImage.color = new Color(0, 0, 0, Percent);
            yield return null;
        }


        yield return new WaitForSeconds(2f);

        _videoPlayer.clip = NextVideoClip;
        _videoPlayer.Play();
        LastImage.color = new Color(0, 0, 0, 0);
        _audioSource.clip = LastBGM;
        _audioSource.Play();
        
        yield return new WaitForSeconds(25f);

        StartCoroutine(CreditSceneImage());
        
    }

    
   

 

    IEnumerator CreditSceneImage()
    {
        for (float Percent = 0f; Percent <= 1; Percent += 0.0005f)
        {

            Percent += 0.0005f;

            LastImage.color = new Color(0, 0, 0, Percent);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        LastText.SetActive(true);

        if(_audioSource.isPlaying == false)
        {
            SceneManager.LoadScene(0);
        }
    }
}
