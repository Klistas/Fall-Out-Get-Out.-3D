using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public void UseGasMask()
    {
            PlayerStatus.GasMaskDuration += 150f;
             Debug.Log("가스마스크 온");
            gameObject.SetActive(false);

    }

    public void UsePainKiller()
    {
        GameManager.Instance.RecoverPlayer();
        Debug.Log("페인킬러 온");
        gameObject.SetActive(false);
    }

   public void GetKey()
    {
        Debug.Log("키 온");
        gameObject.SetActive(false);
    }

   
}
