using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCodes")]
    public KeyCode KeyCodeESC = KeyCode.Escape;



    private RotateMouse _rotateMouse;
    private PlayerMovement _movement;
    private AudioSource _audioSource;
    private Animator _animator;
    private bool _isWalk;
    private bool isOnSettingUI;

    private static class AnimID
    {
        public static readonly int IS_WALK = Animator.StringToHash("IsWalk");
        public static readonly int DIE = Animator.StringToHash("Die");
    }

    void Awake()
    {
        isOnSettingUI = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
        _rotateMouse = GetComponent<RotateMouse>();
        _movement = GetComponent<PlayerMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOnSettingUI == false && GameManager.Instance._isEnd == false)
        {
            UpdateRotate();
            UpdateMove();

        }

        UpdateScreen();
    }

    void UpdateScreen()
    {

        isOnSettingUI = GameManager.Instance._isPause;

        if (isOnSettingUI)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _rotateMouse.UpdateRotate(mouseY, mouseX);
    }

    void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        if (x != 0 || z != 0)
        {
            _isWalk = true;
            if (_audioSource.isPlaying == false)
            {
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }

        else
        {
            _isWalk = false;
            if (_audioSource.isPlaying == true)
            {
                _audioSource.Stop();
            }
        }
        _animator.SetBool(AnimID.IS_WALK, _isWalk);

        _movement.MoveTo(new Vector3(x, 0, z));
    }


}
