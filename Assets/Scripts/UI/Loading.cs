using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadingSceneManager.LoadScene("GameScene");
    }

}
