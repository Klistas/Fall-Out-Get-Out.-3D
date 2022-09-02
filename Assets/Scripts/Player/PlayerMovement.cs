using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float MoveSpeed = 50f;
    public Transform Head;
    private Vector3 moveForce;
    private float _elapsedTime;
    private bool _isTakeDamage;


    private void Awake()
    {
        _elapsedTime = 2f;
    }

    void Update()
    {
        _isTakeDamage = GameManager.Instance._isDamaged;
        _elapsedTime += Time.deltaTime;
        if(GameManager.Instance._isEnd == false && GameManager.Instance._isGameStart && GameManager.Instance._isPause == false)
        {
            transform.Translate(moveForce);
           
                if(_isTakeDamage)
                {

                    _isTakeDamage = false;
                    _elapsedTime = 0f;

                }
        }
    }

    public void MoveTo(Vector3 direction)
    {

        moveForce = direction / MoveSpeed;
    }


}
