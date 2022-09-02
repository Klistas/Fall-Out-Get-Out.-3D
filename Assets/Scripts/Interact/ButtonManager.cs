using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject SecretFloor;
    public Transform SecretCloset;
    public Transform SecretCloset2;
    public Buttons[] Buttons;
    public bool[] ButtonClicks;

    private Vector3 SecretClosetMovedPosition;
    private Vector3 SecretCloset2MovedPosition;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {

            ButtonClicks[i] = false;
        }
        SecretClosetMovedPosition = new Vector3(SecretCloset.localPosition.x, SecretCloset.localPosition.y, SecretCloset.localPosition.z + 0.5f);
        SecretCloset2MovedPosition = new Vector3(SecretCloset2.localPosition.x, SecretCloset2.localPosition.y, SecretCloset2.localPosition.z - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonClicks[0])
        {
            SecretFloor.SetActive(false);
        }
        if (ButtonClicks[6])
        {
            SecretClosetMove();
        }
        CheckButtonClick();
    }

    void SecretClosetMove()
    {
        SecretCloset.localPosition = SecretClosetMovedPosition;
        SecretCloset2.localPosition = SecretCloset2MovedPosition;
    }
    void CheckButtonClick()
    {

        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i]._buttonClick)
            {
                ButtonClicks[i] = true;

            }

        }


    }
}
