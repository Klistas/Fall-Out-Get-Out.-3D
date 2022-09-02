using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeigerCounter : MonoBehaviour
{
    public AudioClip LightGeiger;
    public AudioClip HeavyGeiger;
    public AudioClip GeigerStart;
    public Image GeigerCounterPointer;
    private float _geigerCounterPointerSec = 0.00001f;
    private AudioSource _audioSource;
    private float _lightGeigerMin = 130f;
    private float _lightGeigerMax = 115f;
    private float _heavyGeigerMin = 65f;
    private float _heavyGeigerMax = 50f;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = LightGeiger;
        _audioSource.loop = true;
        _audioSource.Play();
        StartCoroutine(LightGeigerPointer());
    }
    private void Update()
    {
        
        if (_audioSource.isPlaying == false && _audioSource.clip == GeigerStart)
        {
           

        }

        if(GameManager.Instance._isEnd)
        {
            StopAllCoroutines();
            _audioSource.Stop();
        }
        
    }

    IEnumerator LightGeigerPointer()
    {
       
        while (true)
        {
            for (float i = _lightGeigerMin; i >= _lightGeigerMax; i -= 2f)
            {
                GeigerCounterPointer.transform.rotation = Quaternion.Euler(0f, 0f, i);
                if (i == Random.Range(_lightGeigerMax, _lightGeigerMin))
                    break;
                yield return new WaitForSeconds(_geigerCounterPointerSec);
            }

            for (float i = _lightGeigerMax; i <= _lightGeigerMin; i += 2f)
            {
                GeigerCounterPointer.transform.rotation = Quaternion.Euler(0f, 0f, i);
                if (i == Random.Range(_lightGeigerMax, _lightGeigerMin))
                    break;
                yield return new WaitForSeconds(_geigerCounterPointerSec);
            }

            yield return new WaitForSeconds(_geigerCounterPointerSec);
            yield return null;
        }

    }

    IEnumerator HeavyGeigerPointer()
    {
        
           
        
        while (true)
        {
            for (float i = _heavyGeigerMax; i <= _heavyGeigerMin; i += 2f)
            {
                GeigerCounterPointer.transform.rotation = Quaternion.Euler(0f, 0f, i);
                if (i == Random.Range(_heavyGeigerMax, _heavyGeigerMin))
                    break;
                yield return new WaitForSeconds(_geigerCounterPointerSec);
            }
            for (float i = _heavyGeigerMin; i >= _heavyGeigerMax; i -= 2f)
            {
                GeigerCounterPointer.transform.rotation = Quaternion.Euler(0f, 0f, i);
                if (i == Random.Range(_heavyGeigerMax, _heavyGeigerMin))
                    break;
                yield return new WaitForSeconds(_geigerCounterPointerSec);
            }


            yield return new WaitForSeconds(_geigerCounterPointerSec);
            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster") || other.CompareTag("Radiation"))
        {
            StopAllCoroutines();
            StartCoroutine(HeavyGeigerPointer());
            _audioSource.Stop();
            _audioSource.PlayOneShot(GeigerStart);
            _audioSource.clip = HeavyGeiger;
            _audioSource.loop = true;
            _audioSource.Play();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Monster") || other.CompareTag("Radiation"))
        {
            StopAllCoroutines();
            StartCoroutine(LightGeigerPointer());
            _audioSource.Stop();
            _audioSource.clip = LightGeiger;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }


}
