using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Pause Panel Section")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButtons;
    public bool isPaused = false;
    [SerializeField] private GameObject pauseFirst;

    [Header("Settings Panel Section")]
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;
    public AudioMixer audioMixer;
    [SerializeField] private GameObject settingFirst;

    [Header("GameOver Panel Section")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameOverFirst;


    float sfxValue;
    float bgmValue;

    private void Start()
    {
        audioMixer.GetFloat("SFXvolume", out sfxValue);
        audioMixer.GetFloat("BGMvolume", out bgmValue);
        sfxSlider.value = sfxValue;
        bgmSlider.value = bgmValue;
        pauseMenu.SetActive(false);
        MusicManager.Play("GameplayMusic");
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

    void Update()
    {
        if (InputManager.MenuOpenInput)
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }
        else if (InputManager.MenuCloseInput)
        {
            if (isPaused)
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseButtons.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(pauseFirst);

        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        EventSystem.current.SetSelectedGameObject(null);

        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }
    public void backToMenu()
    {
        HealthUI ui = FindObjectOfType<HealthUI>();
        if (ui != null)
        {
            ui.savedHealth = 0;
        }
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void Setting()
    {
        pauseButtons.SetActive(false);
        settingMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingFirst);
    }

    public void SettingBack()
    {
        settingMenu.SetActive(false);
        pauseButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseFirst);
    }

    public void GameOver()
    {
        Debug.Log("HELP ME");
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(gameOverFirst);

        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
