using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AudioManager;

public class PauseMenu : MonoBehaviour
{

    private PlayerActionControls playerActionControls;

    public static bool IsGamePaused = false;
    public static bool IsGameWin = false;

    //public GameObject winMenuUI;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;

    public Slider volumeSlider;
    public Slider volumeMusicSlider;
    public Slider volumeSoundSlider;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(volumeType.master); });
        volumeMusicSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(volumeType.music); });
        volumeSoundSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(volumeType.sound); });

        volumeSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        volumeMusicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        volumeSoundSlider.value = PlayerPrefs.GetFloat("SoundVol", 0.75f);
    }

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerActionControls.Player.Start.triggered)
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {

        Time.timeScale = 1f;
        IsGamePaused = false;
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);

    }

    void Pause()
    {

        Time.timeScale = 0f;
        IsGamePaused = true;
        pauseMenuUI.SetActive(true);
        optionMenuUI.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        SceneLoader.Instance.LoadMainMenu();
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadMainMenu();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        Application.Quit();
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck(volumeType type)
    {
        switch (type)
        {
            case volumeType.master:
                AudioManager.instance.mainMixer.audioMixer.SetFloat("MasterVol", Mathf.Log10(volumeSlider.value) * 20);
                PlayerPrefs.SetFloat("MasterVol", volumeSlider.value);
                break;
            case volumeType.music:
                AudioManager.instance.mainMixer.audioMixer.SetFloat("MusicVol", Mathf.Log10(volumeMusicSlider.value) * 20);
                PlayerPrefs.SetFloat("MusicVol", volumeMusicSlider.value);
                break;
            case volumeType.sound:
                AudioManager.instance.mainMixer.audioMixer.SetFloat("SoundVol", Mathf.Log10(volumeSoundSlider.value) * 20);
                PlayerPrefs.SetFloat("SoundVol", volumeSoundSlider.value);
                break;
        }
    }

    public void LoadNextLevel()
    {
        
    }
}
