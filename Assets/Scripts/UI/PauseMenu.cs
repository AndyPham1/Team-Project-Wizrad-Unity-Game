using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Public variables
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundFxVolumeSlider;
    public UnityEngine.Audio.AudioMixer audioMixer;
    public GameObject pauseMenuObject;
    public GameObject optionObject;
    public GameObject optionSavedObject;
    public string masterVolume = "MasterVolume";
    public string soundEffectsVolume = "SoundEffectsVolume";
    public string musicVolume = "MusicVolume";
    public AudioSource soundEffectsSampler;

    // Private variables
    private float tempVolumeMaster;
    private float tempVolumeMusic;
    private float tempVolumeSoundFx;

    private void Start()
    {
        tempVolumeMaster = masterVolumeSlider.value;
        tempVolumeMusic = musicVolumeSlider.value;
        tempVolumeSoundFx = soundFxVolumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenuObject.activeSelf)
        {
            PauseGame();
        }
        if (!pauseMenuObject.activeSelf && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            PauseGame();
            pauseMenuObject.SetActive(true);
        }
        else if (pauseMenuObject.activeSelf && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            ResumeGame();
        }
    }

    IEnumerator WaitToSaveOption()
    {
        float startCountDown = Time.realtimeSinceStartup + 2f;
        while (Time.realtimeSinceStartup < startCountDown)
        {
            yield return 0;
        }

        optionSavedObject.SetActive(false);
        optionObject.SetActive(false);
        pauseMenuObject.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        audioMixer.GetFloat(masterVolume, out tempVolumeMaster);
        audioMixer.GetFloat(musicVolume, out tempVolumeMusic);
        audioMixer.GetFloat(soundEffectsVolume, out tempVolumeSoundFx);
        masterVolumeSlider.value = tempVolumeMaster;
        musicVolumeSlider.value = tempVolumeMusic;
        soundFxVolumeSlider.value = tempVolumeSoundFx;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (pauseMenuObject.activeSelf)
        {
            pauseMenuObject.SetActive(false);
        }
    }
    public void CancelOption()
    {
        SetMasterVolume(tempVolumeMaster);
        SetMusicVolume(tempVolumeMusic);
        SetSoundEffectsVolume(tempVolumeSoundFx);
        masterVolumeSlider.value = tempVolumeMaster;
        musicVolumeSlider.value = tempVolumeMusic;
        soundFxVolumeSlider.value = tempVolumeSoundFx;
    }

    public void SaveOption()
    {
        tempVolumeMaster = masterVolumeSlider.value;
        tempVolumeMusic = musicVolumeSlider.value;
        tempVolumeSoundFx = soundFxVolumeSlider.value;
        optionSavedObject.SetActive(true);
        StartCoroutine(WaitToSaveOption());
    }

    public void BackToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(masterVolume, value);
    }

    public void SetSoundEffectsVolume(float value)
    {
        audioMixer.SetFloat(soundEffectsVolume, value);
        soundEffectsSampler.Play();
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(musicVolume, value);
    }
}
