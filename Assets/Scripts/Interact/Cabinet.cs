using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{

    public AudioClip CabinetDoorOpen;
    public AudioClip CabinetDoorClose;


    private Quaternion _closeAngle = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _openAngle = Quaternion.Euler(0f, -90f, 0f);
    private float _openSpeed = 10f;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {


    }

    public void ChangeCabinetState()
    {

        if (transform.localRotation == _closeAngle)
        {
            StopAllCoroutines();
            StartCoroutine(CabinetOpen());

        }

        else if (transform.localRotation == _openAngle)
        {
            StopAllCoroutines();
            StartCoroutine(CabinetClose());

        }
    }

    IEnumerator CabinetOpen()
    {
        if (transform.localRotation == _closeAngle)
        {
            _audioSource.PlayOneShot(CabinetDoorOpen);
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




    }
    IEnumerator CabinetClose()
    {
        if (transform.localRotation == _openAngle)
        {
            _audioSource.PlayOneShot(CabinetDoorClose);
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


    }


}
