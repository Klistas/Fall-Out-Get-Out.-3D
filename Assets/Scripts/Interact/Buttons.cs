using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public AudioClip ClickSound;
    public bool _buttonClick;

    private Vector3 _buttonOut = new Vector3(0f, 0.4f, 0f);
    private Vector3 _buttonIn = new Vector3(0f, 0f, 0f);
    private float _openSpeed = 10f;
    private AudioSource _audioSource;
    private bool _buttonOn;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

        if (_buttonOn)
        {
            _audioSource.PlayOneShot(ClickSound);
            transform.localPosition = Vector3.Slerp(transform.localPosition, _buttonIn, Time.deltaTime * _openSpeed);
            _buttonOn = false;
        }

        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, _buttonOut, Time.deltaTime * _openSpeed);
        }

    }

    public void ClickButton()
    {
        _buttonOn = true;
        _buttonClick = true;

    }


}