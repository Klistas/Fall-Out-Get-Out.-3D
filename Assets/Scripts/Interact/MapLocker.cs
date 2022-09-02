using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocker : MonoBehaviour
{
    public Transform LeftDoor;
    public Transform RightDoor;
    public GameObject Lock;
    public static bool LockOpen;
    private Quaternion _openAngle = Quaternion.Euler(0f, -90f, 0f);
    private Quaternion _lOpenAngle = Quaternion.Euler(0f, 90f, 0f);

    void Start()
    {
        
    }

    void Update()
    {
        if(LockOpen)
        {
            LeftDoor.localRotation = _lOpenAngle;
            RightDoor.localRotation = _openAngle;
            Lock.SetActive(false);
        }
    }
}
