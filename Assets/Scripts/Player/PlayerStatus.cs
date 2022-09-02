using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public float PlayerHealth;
    public static float GasMaskDuration;
    public AudioClip PlayerSigh;
    public AudioClip FullHealthBreath;
    public AudioClip TwoHealthBreath;
    public AudioClip LastHealthBreath;
    public AudioClip HitScream;
    public AudioClip DieScream;
    public AudioClip GameOverSound;
    public Image HealthHeartImage;
    public Image HitImage;
    public Image BloodImage;
    public AnimationCurve ImageAnimationCurve;
    public Slider GasMaskFilterSlider;

    private AudioSource _audioSource;
    private float _playerHealthMax = 3f;
    private float _gasMaskDurationMax = 300f;
    private float _elapsedTime;
    private float _breathLevel;
    private float _soundChangeDelay = 1.3f;
    private float _heartBeatRate;
    private float _heartBeatRateSec;
    private bool _isGameOver;
    private bool _isTakeDamage;
    private bool _isRecovered;
    private Color _green = new Color { r = 0, g = 255, b = 0, a = 255 };
    private Color _yellow = new Color { r = 255, g = 255, b = 0, a = 255 };
    private Color _red = new Color { r = 255, g = 0, b = 0, a = 255 };
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayerHealth = _playerHealthMax;
        GasMaskDuration = _gasMaskDurationMax;
        GasMaskFilterSlider.maxValue = GasMaskDuration;
        GasMaskFilterSlider.value = 0;
        _heartBeatRate = 0.7f;
        _heartBeatRateSec = 0.001f;
        _breathLevel = 2f;
        _audioSource.clip = FullHealthBreath;
        _audioSource.Play();
        StartCoroutine(HeartBeat());
    }

    
    private void Update()
    {

        _isTakeDamage = GameManager.Instance._isDamaged;
        _isRecovered = GameManager.Instance._isRecovered;
        GameManager.Instance._playerHealth = PlayerHealth;
        _elapsedTime += Time.deltaTime;
       
        if (_elapsedTime >= _breathLevel)
        {
            UseGasMask();
        }
        if (GasMaskDuration > 300f)
        {
            GasMaskDuration = 300f;
        }
        if (_isGameOver != true && _isTakeDamage)
        {

            _isTakeDamage = false;
            TakeDamage();
        }
        if (_isRecovered == true && PlayerHealth != 3f)
        {
            GameManager.Instance._isRecovered = false;
            Recovery();
        }
        if (PlayerHealth == 0 || GasMaskDuration == 0)
        {
            GameOver();
        }
    }
   
    void TakeDamage()
    {
        _audioSource.Stop();
        PlayerHealth--;
        _breathLevel -= 0.7f;
        _heartBeatRate /= 10f;
        _heartBeatRateSec /= 10f;
        if (PlayerHealth != 0f)
            StartCoroutine(OnBloodScreen());

        if (PlayerHealth == 2f)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(HitScream);
            _audioSource.clip = TwoHealthBreath;
            _audioSource.PlayDelayed(_soundChangeDelay);
            _audioSource.loop = true;
            HealthHeartImage.color = new Color32 { r = 255, g = 143, b = 143, a = 255 };
        }
        if (PlayerHealth == 1f)
        {

            _audioSource.Stop();
            _audioSource.PlayOneShot(HitScream);
            _audioSource.clip = LastHealthBreath;
            _audioSource.PlayDelayed(_soundChangeDelay);
            _audioSource.loop = true;
            HealthHeartImage.color = new Color { r = 255, g = 0, b = 0, a = 255 };
        }
        HitImage.color = new Color(255, 0, 0, 0);
        GameManager.Instance._isDamaged = false;


    }
    IEnumerator OnBloodScreen()
    {
        float percent = 0;

        while (percent < 3)
        {
            percent += Time.deltaTime;

            Color color = HitImage.color;
            color.a = Mathf.Lerp(1, 0, ImageAnimationCurve.Evaluate(percent));
            HitImage.color = color;
            BloodImage.color = color;
            yield return null;
        }

    }

    void Recovery()
    {
        _audioSource.Stop();
        PlayerHealth++;
        _breathLevel += 0.7f;
        _heartBeatRate *= 10f;
        _heartBeatRateSec *= 10f;
        if (PlayerHealth == 3f)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(PlayerSigh);
            _audioSource.clip = FullHealthBreath;
            _audioSource.PlayDelayed(_soundChangeDelay);
            _audioSource.loop = true;
            HealthHeartImage.color = new Color32 { r = 255, g = 255, b = 255, a = 255 };
        }
        if (PlayerHealth == 2f)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(PlayerSigh);
            _audioSource.clip = TwoHealthBreath;
            _audioSource.PlayDelayed(_soundChangeDelay);
            _audioSource.loop = true;
            HealthHeartImage.color = new Color32 { r = 255, g = 143, b = 143, a = 255 };
        }


    }
    void UseGasMask()
    {
        _elapsedTime = 0;
        GasMaskDuration--;
        UseGasMaskFilterSlider();

    }
    IEnumerator HeartBeat()
    {
        while (true)
        {
            for (float i = 2; i <= 2.3; i += 0.1f)
            {
                HealthHeartImage.transform.localScale = new Vector3(i, i, 1f);
                yield return new WaitForSeconds(_heartBeatRateSec);
            }

            for (float i = 2.3f; i >= 2.0; i -= 0.01f)
            {
                HealthHeartImage.transform.localScale = new Vector3(i, i, 1f);
                yield return new WaitForSeconds(_heartBeatRateSec);
            }

            yield return new WaitForSeconds(_heartBeatRate);
            yield return null;
        }

    }
    void UseGasMaskFilterSlider()
    {
        GasMaskFilterSlider.value = _gasMaskDurationMax - GasMaskDuration;

        if (GasMaskFilterSlider.value < 100f)
        {
            ColorBlock FullHealthGasMaskColor = GasMaskFilterSlider.colors;
            FullHealthGasMaskColor.normalColor = _green;
            GasMaskFilterSlider.colors = FullHealthGasMaskColor;
        }

        if (GasMaskFilterSlider.value >= 100f && GasMaskFilterSlider.value < 200f)
        {
            ColorBlock TwoHealthGasMaskColor = GasMaskFilterSlider.colors;
            TwoHealthGasMaskColor.normalColor = _yellow;
            GasMaskFilterSlider.colors = TwoHealthGasMaskColor;
        }

        if (GasMaskFilterSlider.value >= 200f)
        {
            ColorBlock LastHealthGasMaskColor = GasMaskFilterSlider.colors;
            LastHealthGasMaskColor.normalColor = _red;
            GasMaskFilterSlider.colors = LastHealthGasMaskColor;
        }
    }





    void GameOver()
    {
        if (_isGameOver == false)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(DieScream);
            _audioSource.clip = GameOverSound;
            _audioSource.loop = false;
            _audioSource.PlayDelayed(1f);

            _isGameOver = true;
            GameManager.Instance.End();
            Debug.Log("게임오버");


        }

    }



}
