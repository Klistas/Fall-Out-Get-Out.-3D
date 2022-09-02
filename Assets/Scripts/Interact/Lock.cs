using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lock : MonoBehaviour
{
    
    public List <GameObject> LockPassword = new List<GameObject>();
    public GameObject FirstPassword;
    public GameObject SecondPassword;
    public GameObject ThirdPassword;
    public GameObject ForthPassword;
    public AudioClip LockClick;
    public AudioClip LockOpen;

    private AudioSource _audioSource;
    private int[] _numberArray = {0, 0, 0, 0};
    private int[] _numberPassword = { 6, 5, 2, 7 };
    private LockHighlight _lockColor;
    private int _scrollNumber = 0;
    private int _changePassword = 0;
    private int _passwordNumber = 0;
    private bool _isSelectHighlight = false;
    



    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _lockColor = GetComponent<LockHighlight>();
        LockPassword.Add(FirstPassword);
        LockPassword.Add(SecondPassword);
        LockPassword.Add(ThirdPassword);
        LockPassword.Add(ForthPassword);
        foreach (GameObject r in LockPassword)
        {
            r.transform.Rotate(-144, 0, 0, Space.Self);
        }
    }
    void Update()
    {
        if(GameManager.Instance._isLockPick)
        {
            MoveLock();
            RotateLocks();
            Password();
            Time.timeScale = 0;

        }
    }

    void MoveLock()
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            _isSelectHighlight = true;
            _changePassword++;
            _passwordNumber += 1;

            if (_passwordNumber > 3)
            {
                _passwordNumber = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            _isSelectHighlight = true;
            _changePassword--;
            _passwordNumber -= 1;

            if (_passwordNumber < 0)
            {
                _passwordNumber = 3;
            }
        }
        _changePassword = (_changePassword + LockPassword.Count) % LockPassword.Count;


        for (int i = 0; i < LockPassword.Count; i++)
        {
            if (_isSelectHighlight)
            {
                if (_changePassword == i)
                {

                    LockPassword[i].GetComponent<LockHighlight>()._isSelect = true;
                    LockPassword[i].GetComponent<LockHighlight>().BlinkingMaterial();
                }
                else
                {
                    LockPassword[i].GetComponent<LockHighlight>()._isSelect = false;
                    LockPassword[i].GetComponent<LockHighlight>().BlinkingMaterial();
                }
            }
        }

    }

    void RotateLocks()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _audioSource.PlayOneShot(LockClick);
            _isSelectHighlight = true;
            _scrollNumber = 36;
            LockPassword[_changePassword].transform.Rotate(-_scrollNumber, 0, 0, Space.Self);

            _numberArray[_changePassword] += 1;

            if (_numberArray[_changePassword] > 9)
            {
                _numberArray[_changePassword] = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _audioSource.PlayOneShot(LockClick);
            _isSelectHighlight = true;
            _scrollNumber = 36;
            LockPassword[_changePassword].transform.Rotate(_scrollNumber, 0, 0, Space.Self);

            _numberArray[_changePassword] -= 1;

            if (_numberArray[_changePassword] < 0)
            {
                _numberArray[_changePassword] = 9;
            }
        }
        
    }

    public void Password()
    {
        if (_numberArray.SequenceEqual(_numberPassword))
        {
            Debug.Log("Password correct");

            for (int i = 0; i < LockPassword.Count; i++)
            {
                LockPassword[i].GetComponent<LockHighlight>()._isSelect = false;
                LockPassword[i].GetComponent<LockHighlight>().BlinkingMaterial();
            }
            GameManager.Instance._isLockPick = false;
            GameManager.Instance.LockPanelOff();
            _audioSource.PlayOneShot(LockOpen);
            MapLocker.LockOpen = true;
        }
    }
}
