using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    // Public variables
    public GameObject mainMenuObject;
    public GameObject optionObject;
    public GameObject savedOptionObject;
    public Slider sliderMasterVolume;
    public Slider sliderMusicVolume;
    public Slider sliderSFXVolume;
    public AudioMixer audioMixer;
    public AudioSource sfxSampleSound;
    public string masterVolume = "MasterVolume";
    public string soundEffectsVolume = "SoundEffectsVolume";
    public string musicVolume = "MusicVolume";

    // Private variables
    private float tempVolumeMaster;
    private float tempVolumeMusic;
    private float tempVolumeSFX;

    // Corroutine to saved optino countdown
    IEnumerator WaitToSaveOption()
    {
        // Countdown = 2 seconds
        float startCountDown = Time.realtimeSinceStartup + 2f;
        while (Time.realtimeSinceStartup < startCountDown)
        {
            yield return 0;
        }

        savedOptionObject.SetActive(false);
        optionObject.SetActive(false);
        mainMenuObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        tempVolumeMaster = sliderMasterVolume.value;
        tempVolumeMusic = sliderMusicVolume.value;
        tempVolumeSFX = sliderSFXVolume.value;
    }

    // Exit the application
    public void QuitGame()
    {
        Application.Quit();
    }

    // Cancel saving option and return the original set up
    public void CancelOption()
    {
        SetMasterVolume(tempVolumeMaster);
        SetMusicVolume(tempVolumeMusic);
        SetSoundEffectsVolume(tempVolumeSFX);
        sliderMasterVolume.value = tempVolumeMaster;
        sliderMusicVolume.value = tempVolumeMusic;
        sliderSFXVolume.value = tempVolumeSFX;
    }

    // Save the current option values
    public void SaveOption()
    {
        tempVolumeMaster = sliderMasterVolume.value;
        tempVolumeMusic = sliderMusicVolume.value;
        tempVolumeSFX = sliderSFXVolume.value;
        savedOptionObject.SetActive(true);
        StartCoroutine(WaitToSaveOption());
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(masterVolume, value);
    }

    public void SetSoundEffectsVolume(float value)
    {
        audioMixer.SetFloat(soundEffectsVolume, value);
        sfxSampleSound.Play();
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(musicVolume, value);
    }
}
