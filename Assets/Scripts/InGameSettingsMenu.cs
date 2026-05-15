using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InGameSettingsMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject settingsMenuPanel;

    [Header("Buttons")]
    [SerializeField] private Button voiceButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown graphicsDropdown;

    [Header("Gameplay Input To Disable While Menu Is Open")]
    [SerializeField] private MonoBehaviour shootingInputScript; // assign ShotController

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    private bool menuOpen = false;
    private bool voiceOn = true;
    private TMP_Text voiceButtonText;

    // Static property for other scripts to check menu status
    public static bool IsOpen { get; private set; }

    private void Start()
    {
        settingsMenuPanel.SetActive(false);
        IsOpen = false;

        if (shootingInputScript != null)
            shootingInputScript.enabled = true;

        voiceButtonText = voiceButton.GetComponentInChildren<TMP_Text>();

        voiceButton.onClick.AddListener(ToggleVoice);
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        SetupGraphicsDropdown();
        UpdateVoiceButtonText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        menuOpen = !menuOpen;
        IsOpen = menuOpen;

        // Show or hide menu panel
        settingsMenuPanel.SetActive(menuOpen);

        // Enable/disable shooting input
        if (shootingInputScript != null)
        {
            shootingInputScript.enabled = !menuOpen;
        }

        // Show cursor when menu is open
        if (menuOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void ToggleVoice()
    {
        voiceOn = !voiceOn;
        AudioListener.volume = voiceOn ? 1f : 0f;
        UpdateVoiceButtonText();
    }

    private void UpdateVoiceButtonText()
    {
        if (voiceButtonText != null)
        {
            voiceButtonText.text = voiceOn ? "Voice: ON" : "Voice: OFF";
        }
    }

    private void SetupGraphicsDropdown()
    {
        graphicsDropdown.ClearOptions();

        List<string> qualityOptions = new List<string>();
        foreach (string qualityName in QualitySettings.names)
            qualityOptions.Add(qualityName);

        graphicsDropdown.AddOptions(qualityOptions);

        int currentQuality = QualitySettings.GetQualityLevel();
        graphicsDropdown.value = currentQuality;
        graphicsDropdown.RefreshShownValue();

        graphicsDropdown.onValueChanged.AddListener(ChangeGraphicsQuality);
    }

    private void ChangeGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void OnDestroy()
    {
        if (shootingInputScript != null)
            shootingInputScript.enabled = true;

        IsOpen = false;
    }
}