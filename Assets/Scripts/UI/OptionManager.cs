using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionManager : MonoBehaviour
{
    public Slider VolumeSlider;
    public Slider MouseSensitiveSlider;
    public Slider CharacterSensitiveSlider;
    public AudioMixer _audioMixer;
    
    private float Volume;
    
    // Start is called before the first frame update
    void Awake()
    {
        VolumeSlider.minValue = -40f;
        VolumeSlider.maxValue = 20f;
        VolumeSlider.value = 0f;
        MouseSensitiveSlider.minValue = 0.5f;
        MouseSensitiveSlider.maxValue = 3f;
        MouseSensitiveSlider.value = 1.5f;
        CharacterSensitiveSlider.minValue = 15f;
        CharacterSensitiveSlider.maxValue = 100f;
        CharacterSensitiveSlider.value = 35f;
        
    }

    // Update is called once per frame
    void Update()
    {
        VolumeOption();
        MouseSensitiveOption();
        CharacterSensitiveOption();
    }
    void VolumeOption()
    {
        Volume = VolumeSlider.value;
        if(Volume <= -40f)
        {
            Volume = -80f;
        }
        _audioMixer.SetFloat("Master", Volume);

    }
    void MouseSensitiveOption()
    {
        RotateMouse.rotCamYAxisSpeed = MouseSensitiveSlider.value;
    }
    void CharacterSensitiveOption()
    {
        PlayerMovement.MoveSpeed = CharacterSensitiveSlider.value;
    }
}
