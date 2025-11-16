using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject menuFirst;

    [Header("Setting Panel Section")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingFirst;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;
    public AudioMixer audioMixer;

    float sfxValue;
    float bgmValue;

    [Header("Credits Panel Section")]
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject CreditsFirst;

    public void Start()
    {
        audioMixer.GetFloat("SFXvolume", out sfxValue);
        sfxSlider.value = sfxValue;
        audioMixer.GetFloat("BGMvolume", out bgmValue);
        bgmSlider.value = bgmValue;
        MusicManager.Play("MenuMusic");
    }

    public void SfxVolume(float volume)
    {
        if (volume == -25)
        {
            audioMixer.SetFloat("SFXvolume", -80);
        }
        else
        {
            audioMixer.SetFloat("SFXvolume", volume);
        }
    }

    public void BgmVolume(float volume)
    {
        if (volume == -35)
        {
            audioMixer.SetFloat("BGMvolume", -80);
        }
        else
        {
            audioMixer.SetFloat("BGMvolume", volume);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
        MusicManager.Play("GameplayMusic");
    }

    public void Credits()
    {
        menuButtons.SetActive(false);
        creditsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(CreditsFirst);
    }

    public void CreditsBack()
    {
        creditsPanel.SetActive(false);
        menuButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuFirst);
    }

    public void Setting()
    {
        menuButtons.SetActive(false);
        settingPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingFirst);
    }

    public void SettingBack()
    {
        settingPanel.SetActive(false);
        menuButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuFirst);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
