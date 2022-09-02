using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioClip OneDoorOpen;
    public AudioClip OneDoorClose;
    public AudioClip TwoDoorOpen;
    public AudioClip TwoDoorClose;


    private Quaternion _closeAngle = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _openAngle = Quaternion.Euler(0f, 90f, 0f);
    private Quaternion _lCloseAngle = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion _lOpenAngle = Quaternion.Euler(0f, 95f, 0f);

    private float _openSpeed = 10f;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

       
    }

    public void ChangeDoorState()
    {

        if (transform.localRotation == _closeAngle || transform.localRotation == _lCloseAngle)
        {
            StopAllCoroutines();
            StartCoroutine(Open());

        }

        else if (transform.localRotation == _openAngle || transform.localRotation == _lOpenAngle)
        {
            StopAllCoroutines();
            StartCoroutine(Close());

        }
    }
    
    IEnumerator Open()
    {
        if (transform.localRotation == _closeAngle)
        {
                _audioSource.PlayOneShot(OneDoorOpen);
            while (true)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _openAngle, Time.deltaTime * _openSpeed);
                yield return new WaitForSeconds(0.005f);
                if (transform.localRotation == _openAngle)
                {
                    yield return null;
                }
            }
        }
       else if (transform.localRotation == _lCloseAngle)
        {
                _audioSource.PlayOneShot(TwoDoorOpen);
            while(true)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _lOpenAngle, Time.deltaTime * _openSpeed);
                yield return new WaitForSeconds(0.005f);
                if (transform.localRotation == _lOpenAngle)
                {
                    yield return null;
                }
            }
            
        }
    
        

    }
    IEnumerator Close()
    {
        if (transform.localRotation == _openAngle)
        {
                _audioSource.PlayOneShot(OneDoorClose);
            while (true)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _closeAngle, Time.deltaTime * _openSpeed);

                yield return new WaitForSeconds(0.005f);
                if (transform.localRotation == _closeAngle)
                {
                    yield return null;
                }
            }
        }
       else if (transform.localRotation == _lOpenAngle)
        {
                _audioSource.PlayOneShot(TwoDoorClose);
            while (true)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _lCloseAngle, Time.deltaTime * _openSpeed);
                yield return new WaitForSeconds(0.005f);
                if (transform.localRotation == _lCloseAngle)
                {
                    yield return null;
                }
            }

        }
       
    }

  
}
