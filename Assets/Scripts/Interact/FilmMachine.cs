using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PhotoHint;
    public bool _isFilmOn;
    public AudioClip FilmMachineOnSound;
    public AudioClip FilmMachineRunningSound;
    public AudioClip FilmMachineOffSound;

    private  AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        GameManager.Instance.FilmMachineOnEvent.RemoveListener(UseFilmMachineOn);
        GameManager.Instance.FilmMachineOffEvent.RemoveListener(UseFilmMachineOff);
        GameManager.Instance.FilmMachineOnEvent.AddListener(UseFilmMachineOn);
        GameManager.Instance.FilmMachineOffEvent.AddListener(UseFilmMachineOff);
    }
    public void UseFilmMachineOn()
    {
            PhotoHint.SetActive(true);
            _audioSource.PlayOneShot(FilmMachineOnSound);
            _audioSource.clip = FilmMachineRunningSound;
            _audioSource.loop = true;
            _audioSource.PlayDelayed(1f);
            _isFilmOn = true;
            GameManager.Instance._isFilmOn = _isFilmOn;
            Debug.Log("¿Â");

    }
    public void UseFilmMachineOff()
    {
        
            PhotoHint.SetActive(false);
            _audioSource.Stop();
            _audioSource.PlayOneShot(FilmMachineOffSound);
            _isFilmOn = false;
            GameManager.Instance._isFilmOn = _isFilmOn;
            Debug.Log("¿ÀÇÁ");

       
    }

    private void OnDisable()
    {
        GameManager.Instance.FilmMachineOnEvent.RemoveListener(UseFilmMachineOn);
        GameManager.Instance.FilmMachineOffEvent.RemoveListener(UseFilmMachineOff);
    }

}
