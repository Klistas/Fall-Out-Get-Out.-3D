using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interact : MonoBehaviour
{
    public Image InteractiveImage;
    public GameObject Interactive;
    public GameObject InteractiveImageActive;
    public GameObject Map;
    public TextMeshProUGUI InteractText;
    public LayerMask InterativeLayer;
    public static bool _clearTextOn;

    private float _interactDiastance = 5f;
    void Update()
    {
        
        if(GameManager.Instance._isLockPick == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, _interactDiastance, InterativeLayer))
            {
                if(_clearTextOn == false)
                {

                   if(GameManager.Instance._isPause == false && GameManager.Instance._isEnd == false && GameManager.Instance._isGameStart)
                   {
                    
                      if(!hit.collider.CompareTag("Untagged"))
                      {
                          InteractText.text = hit.collider.tag;
                          Interactive.SetActive(true);
                      }
                      else
                      {
                         Interactive.SetActive(false);
                      }


                    if (hit.collider.CompareTag("Injection") || hit.collider.CompareTag("Knife"))
                    {
                        InteractiveImage.color = new Color(0, 0, 0, 0);
                    }
                    else
                    {
                        InteractiveImage.color = new Color(1, 1, 1, 1);
                    }
         
                      if (Input.GetKeyDown(KeyCode.E)) // �����丵
                      {
                          Interactive.SetActive(false);
                          InteractiveImageActive.SetActive(true);

                          if (hit.collider.CompareTag("Door")) // ��
                          {
                              hit.collider.GetComponent<Door>().ChangeDoorState();
                          }

                          if (hit.collider.CompareTag("Cabinet")) // ĳ���
                          {
                              hit.collider.GetComponent<Cabinet>().ChangeCabinetState();
                          }

                          if (hit.collider.CompareTag("Button")) // ��ư
                          {
                              hit.collider.GetComponent<Buttons>().ClickButton();
                          }

                          if (hit.collider.CompareTag("GasMaskItem")) // ��ȭ��
                          {
                              hit.collider.GetComponent<Item>().UseGasMask();
                          }

                          if (hit.collider.CompareTag("PainKillerItem") &&GameManager.Instance._playerHealth != 3f) // ������
                          {
                              hit.collider.GetComponent<Item>().UsePainKiller();
                          }

                          if (hit.collider.CompareTag("EscapeKeyA")) // Ű 1
                          {
                              GameManager.Instance._gotEscapeKey[0] = true;
                              hit.collider.GetComponent<Item>().GetKey();
                          }

                          if (hit.collider.CompareTag("EscapeKeyB")) // Ű 2
                          {
                              GameManager.Instance._gotEscapeKey[1] = true;
                              hit.collider.GetComponent<Item>().GetKey();
                          }

                          if(hit.collider.CompareTag("EscapeKeyC")) // Ű 3
                          {
                              GameManager.Instance._gotEscapeKey[2] = true;
                              hit.collider.GetComponent<Item>().GetKey();
                          }

                          if (hit.collider.CompareTag("ConfidentialFile")) // ����
                          {
                              GameManager.Instance.HintOn();
                          }

                          if(hit.collider.CompareTag("FilmMachine")) // �����
                          {
                              if(GameManager.Instance._isFilmOn)
                              {
                                  GameManager.Instance.FilmMachineOff();
                              }
                              else
                              {
                                  GameManager.Instance.FilmMachineOn();
                              }
                          }

                          if(hit.collider.CompareTag("Lock")) // �ڹ���
                          {
                              GameManager.Instance.LockPanelOn();
                          }

                          if (hit.collider.CompareTag("EscapeDoor") && GameManager.Instance._canClearGame) // Ż�ⱸ
                          {
                            GameManager.Instance.OnGameClear.Invoke();
                            

                          }

                        if (hit.collider.CompareTag("Map")) // ���ϸ�
                          {
                              GameManager.Instance._haveMap = true;
                              Map.SetActive(false);
                          }
                      }
           
                   
                   }
                    InteractiveImageActive.SetActive(false);  
                }
                else
                {
                    InteractiveImage.color = new Color(0, 0, 0, 0);
                    InteractText.text = "YOU ESCAPED!!!";
                }
          
            
            }
            else
            {
                    Interactive.SetActive(false);
            }


        }






    }

}
