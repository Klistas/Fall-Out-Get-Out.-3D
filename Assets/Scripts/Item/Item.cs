using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public void UseGasMask()
    {
            PlayerStatus.GasMaskDuration += 150f;
             Debug.Log("��������ũ ��");
            gameObject.SetActive(false);

    }

    public void UsePainKiller()
    {
        GameManager.Instance.RecoverPlayer();
        Debug.Log("����ų�� ��");
        gameObject.SetActive(false);
    }

   public void GetKey()
    {
        Debug.Log("Ű ��");
        gameObject.SetActive(false);
    }

   
}
