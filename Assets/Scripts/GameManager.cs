using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent OnGameClear = new UnityEvent();

    public UnityEvent FilmMachineOnEvent = new UnityEvent();
    public UnityEvent FilmMachineOffEvent = new UnityEvent();
    public Image KeyA;
    public Image KeyB;
    public Image KeyC;
    public GameObject Monsters;
    public GameObject HintPanel;
    public GameObject MapPanel;
    public AudioClip Paper;
    public Camera LockpickCamera;
    public Camera MainCamera;
    public bool _isHintOn;
    public bool _isFilmOn;
    public bool _isEnd;
    public bool _isPause;
    public bool _isOption;
    public bool _isDamaged;
    public bool _isRecovered;
    public bool[] _gotEscapeKey = new bool[] { false, false, false };
    public bool _canClearGame;
    public bool _isGameStart;
    public bool _isMonsterActive;
    public bool _isLockPick;
    public bool _haveMap;
    public bool _onMap;
    public float _playerHealth;
    private AudioSource _audioSource;

    private void Start()
    {
        LockpickCamera.enabled = false;

        _audioSource = GetComponent<AudioSource>();
        
    }


    private void Update()
    {

        if(_haveMap && Input.GetKeyDown(KeyCode.Tab))
        {
            MapPanelOn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isLockPick)
            {
                LockPanelOff();
            }
            else
            {
                if (_onMap)
                {
                    MapPanelOff();
                }
                else
                {
                    if (_isHintOn)
                    {
                        HintOff();
                    }
                }
            }
           
        }

       
        if(_gotEscapeKey[0])
        {
            KeyA.color = new Color(255, 255, 255, 255);
        }
        if (_gotEscapeKey[1])
        {
            KeyB.color = new Color(255, 255, 255, 255);
        }
        if (_gotEscapeKey[2])
        {
            KeyC.color = new Color(255, 255, 255, 255);
        }

        if(_gotEscapeKey[0] && _gotEscapeKey[1] && _gotEscapeKey[2])
        {
            _canClearGame = true;
        }
        if (_isGameStart && _isMonsterActive == false)
        {
            Monsters.SetActive(true);
        }
    }

    public void OnDamagedPlayer()
    {
        _isDamaged = true;
    }
    public void RecoverPlayer()
    {
        _isRecovered = true;
    }

    public void GameClear()
    {
        _isFilmOn = false;
        _isEnd = false;
        _isPause = false;
        _isOption = false;
        _canClearGame = false;
        _isMonsterActive = false;
        _onMap = false;
        _haveMap = false;
        for (int i = 0; i < 3; i++)
        {
            _gotEscapeKey[i] = false;
        }

        SceneManager.LoadScene(3);

    }


    public void LockPanelOn()
    {   
        _isLockPick = true;
        _isPause = true;
        LockpickCamera.enabled = true;
        MainCamera.enabled = false;
    }

    public void LockPanelOff()
    {
        Time.timeScale = 1;
        _isLockPick = false;
        _isPause = false;
        LockpickCamera.enabled = false;
        MainCamera.enabled = true;
    }

    public void HintOn()
    {
        Time.timeScale = 0;
        _audioSource.PlayOneShot(Paper);
        HintPanel.SetActive(true);

        _isPause = true;
        _isHintOn = true;
    }

    void HintOff()
    {
        Time.timeScale = 1;
        _audioSource.PlayOneShot(Paper);
        HintPanel.SetActive(false);

        _isPause = false;
        _isHintOn = false;
    }

    void MapPanelOn()
    {
        Debug.Log("¸Ê¿Â");
        MapPanel.SetActive(true);
        _isPause = true;
        _onMap = true;
        Time.timeScale = 0;
        _audioSource.PlayOneShot(Paper);
    }

    void MapPanelOff()
    {
        Debug.Log("¸Ê¿ÀÇÁ");
        MapPanel.SetActive(false);
        _isPause = false;
        _onMap = false;
        Time.timeScale = 1;
        _audioSource.PlayOneShot(Paper);
    }

    public void FilmMachineOn()
    {

        FilmMachineOnEvent.Invoke();
       
    }
    public void FilmMachineOff()
    {
        FilmMachineOffEvent.Invoke();

    }

    public void End()
    {
        _isEnd = true;

        Time.timeScale = 0;
        OnGameEnd.Invoke();
    }



}