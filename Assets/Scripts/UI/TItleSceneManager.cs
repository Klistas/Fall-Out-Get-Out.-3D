using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TItleSceneManager : MonoBehaviour
{

    public Button GameStartButton;
    public Button HowToPlayButton;
    public Image HowToPlayButtonImage;
    public Image HowToPlayExitButtonImage;
    public Button SettingsButton;
    public Image SettingsButtonImage;
    public Image SettingsExitButtonImage;
    public Button HowToPlayExitButton;
    public Button SettingsExitButton;
    public Button GameExitButton;
    public GameObject TitleObject;
    public GameObject HowToPlayPanel;
    public GameObject SettingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        GameStartButton.onClick.RemoveListener(GameStart);
        GameStartButton.onClick.AddListener(GameStart);
        HowToPlayButton.onClick.RemoveListener(HowToPlayOn);
        HowToPlayButton.onClick.AddListener(HowToPlayOn);
        HowToPlayExitButton.onClick.RemoveListener(HowToPlayOff);
        HowToPlayExitButton.onClick.AddListener(HowToPlayOff);
        SettingsButton.onClick.RemoveListener(SettingsOn);
        SettingsButton.onClick.AddListener(SettingsOn);
        SettingsExitButton.onClick.RemoveListener(SettingsOff);
        SettingsExitButton.onClick.AddListener(SettingsOff);
        GameExitButton.onClick.RemoveListener(GameExit);
        GameExitButton.onClick.AddListener(GameExit);
        Time.timeScale = 1;
        GameManager.Instance._isPause = false;
        GameManager.Instance._isEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerMovement.MoveSpeed);
    }

    void GameStart()
    {
        LoadingSceneManager.LoadScene("GameScene");
    }

    void HowToPlayOn()
    {
        HowToPlayPanel.SetActive(true);
        HowToPlayButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        TitleObject.SetActive(false);
    }

    void HowToPlayOff()
    {
        HowToPlayPanel.SetActive(false);
        HowToPlayExitButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        TitleObject.SetActive(true);
    }
    void SettingsOn()
    {
        SettingsPanel.SetActive(true);
        SettingsButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        TitleObject.SetActive(false);
    }

    void SettingsOff()
    {
        SettingsPanel.SetActive(false);
        SettingsExitButtonImage.color = new Color(0f, 1f, 0.3254902f, 1f);
        TitleObject.SetActive(true);
    }

    void GameExit()
    {
        Application.Quit();
    }
    private void OnDisable()
    {
        
        SettingsButton.onClick.RemoveListener(SettingsOn);
        SettingsExitButton.onClick.RemoveListener(SettingsOff);
        HowToPlayButton.onClick.RemoveListener(HowToPlayOn);
        HowToPlayExitButton.onClick.RemoveListener(HowToPlayOff);
        GameStartButton.onClick.RemoveListener(GameStart);
        GameExitButton.onClick.RemoveListener(GameExit);
    }
}
