using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject OptionPanel;
    public Button OptionButton;
    public Button OptionOffButton;
    public Button ResumeButton;
    public Button TitleMenuButton;
    public Image OptionButtonImage;
    public Image OptionOffButtonImage;
    public Image ResumeButtonImage;
    public Image GameStartScreen;
    public Image GameOverScreen;
    public Image RadiationScreen;
    public AudioClip GameStartSound;
    public TextMeshProUGUI GameOverText;

    private AudioSource _audioSource;
    private bool _isPause;
    private bool _isClear;
    private bool _isOption;
    
    // Update is called once per frame


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        OptionButton.onClick.RemoveListener(OptionPanelOn);
        OptionButton.onClick.AddListener(OptionPanelOn);
        OptionOffButton.onClick.RemoveListener(OptionPanelOff);
        OptionOffButton.onClick.AddListener(OptionPanelOff);
        TitleMenuButton.onClick.RemoveListener(ToTitleMenu);
        TitleMenuButton.onClick.AddListener(ToTitleMenu);
        ResumeButton.onClick.RemoveListener(GameResume);
        ResumeButton.onClick.AddListener(GameResume);
        GameManager.Instance.OnGameEnd.RemoveListener(GameOver);
        GameManager.Instance.OnGameEnd.AddListener(GameOver);
        GameManager.Instance.OnGameClear.RemoveListener(CallGameClear);
        GameManager.Instance.OnGameClear.AddListener(CallGameClear);
        StartCoroutine(GameStart());
        _audioSource.PlayOneShot(GameStartSound);
    }
    void Update()
    {
        if(GameManager.Instance._isLockPick == false)
        {
            GameManager.Instance._isPause = _isPause;
        }

        if (GameManager.Instance._isHintOn == false && GameManager.Instance._onMap == false && GameManager.Instance._isLockPick == false)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(_isPause)
                {
                    if(_isOption)
                    {
                        OptionPanelOff();
                    }
                    else
                    {
                        GameResume();
                    }

                }
                else
                {
                    GamePause();
                }
            }

        }
        if(_isClear == true)
        {

            StartCoroutine(GameClear());
            Interact._clearTextOn = true;
            _isClear = false;
        }
       
    }
    IEnumerator GameStart()
    {
        float Percent = 1f;

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(GameStartImage(Percent));
            yield return new WaitForSeconds(2.2f);
        }


        GameManager.Instance._isGameStart = true;
        _audioSource.Play();
        _audioSource.loop = true;


        yield return null;

    }

    IEnumerator GameStartImage(float Percent)
    {

        while (true)
        {
            Percent -= 0.0005f;

            GameStartScreen.color = new Color(0, 0, 0, Percent);


            yield return null;
        }
    }
    void CallGameClear()
    {
        StopAllCoroutines();
        GameStartScreen.color = new Color(0, 0, 0, 0);

        _isClear = true;
    }
    IEnumerator GameClear()
    {
        GameManager.Instance._isPause = true;
        for (float Percent = 0f; Percent <= 1; Percent += 0.0005f)
        {

            Percent += 0.0005f;

            GameStartScreen.color = new Color(0, 0, 0, Percent);
            yield return null;
        }
            GameManager.Instance.GameClear();
    }

    void ToTitleMenu()
    {
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        AudioListener.pause = true;

        _isPause = true;
    }
    void GameResume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        AudioListener.pause = false;
        ResumeButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        _isPause = false;
    }
    void OptionPanelOn()
    {
        PausePanel.SetActive(false);
        _isOption = true;
        OptionButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        OptionPanel.SetActive(true);
    }
    void OptionPanelOff()
    {
        PausePanel.SetActive(true);
        _isOption = false;
        OptionOffButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        OptionPanel.SetActive(false);
    }
    void GameOver()
    {
        StartCoroutine(GameOverUI());
    }
    IEnumerator GameOverUI()
    {

        float Percent = 0f;

        while (true)
        {
            Percent += 0.005f;


            GameOverScreen.color = new Color(0, 0, 0, Percent);
            RadiationScreen.color = new Color(255, 255, 255, Percent);
            GameOverText.color = new Color(0, 255, 0, Percent);
            if (Percent == 1f)
                break;
            yield return null;
        }
    }

    private void OnDisable()
    {
        OptionButton.onClick.RemoveListener(OptionPanelOn);
        OptionOffButton.onClick.RemoveListener(OptionPanelOff);
        TitleMenuButton.onClick.RemoveListener(ToTitleMenu);
        ResumeButton.onClick.RemoveListener(GameResume);
        GameManager.Instance.OnGameEnd.RemoveListener(GameOver);
        GameManager.Instance.OnGameClear.RemoveListener(CallGameClear);


    }

}
