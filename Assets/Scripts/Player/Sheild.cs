using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    public GameObject Shield;
    private float _elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_elapsedTime);
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > 5.0f && _elapsedTime < 10f)
        {
            Shield.SetActive(true);
        }
        else if(_elapsedTime >= 10f)
        {
            Shield.SetActive(false);
            _elapsedTime = 0f;
        }
    }
}
