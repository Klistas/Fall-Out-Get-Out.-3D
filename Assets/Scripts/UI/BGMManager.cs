using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioClip TitleMusic;
    private AudioSource _audioSource;
    void Awake()
    {

        var obj = FindObjectsOfType<BGMManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
    }

    void Update()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        
        if(CurrentScene.name != "GameScene" || CurrentScene.name != "CreditScene")
        {
            MusicStart();
        }
        else
        {
            MusicStop();
        }
    }

    void MusicStart()
    {
        if(_audioSource.isPlaying == false)
        {
            _audioSource.clip = TitleMusic;
            _audioSource.Play();
        }
    }
    void MusicStop()
    {
        _audioSource.Stop();
    }
       
}
