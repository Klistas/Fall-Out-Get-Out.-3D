using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHighlight : MonoBehaviour
{

    public float blinkingTime = 0.5f;
    public bool _isSelect;
    private GameObject _lockPassword;


    private void Awake()
    {
    }
    void Start()
    {
        _lockPassword = gameObject;
    }


    public void BlinkingMaterial()
    {
        _lockPassword.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        
        if (_isSelect)
        {
            _lockPassword.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.clear, Color.blue, Mathf.PingPong(Time.time, blinkingTime)));
        }
        if (_isSelect == false)
        {
            _lockPassword.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
        }

    }
}
