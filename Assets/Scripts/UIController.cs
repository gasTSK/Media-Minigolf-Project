using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider powerBar;

    public GameObject inHoleText;

    public TMP_Text shotsText;

    public TMP_Text parText;

    public GameObject endScreen;
    public TMP_Text endScreenScoreText;

    public GameObject outOfBoundsMessage;

    public string mainMenu;

    public ScorecardController scoreCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowHideScorecard();
        }
    }

    public void ShowPowerBar()
    {
        powerBar.gameObject.SetActive(true);
    }

    public void SetPowerBar(float power, float maxPower)
    {
        powerBar.maxValue = maxPower;
        powerBar.value = power;
    }

    public void HidePowerBar()
    {
        powerBar.gameObject.SetActive(false);
    }

    public void DisplayInHole()
    {
        inHoleText.SetActive(true);
    }

    public void UpdateShotsText(int currentShots)
    {
        shotsText.text = "Shots: " + currentShots;
    }

    public void SetParText(int currentPar)
    {
        parText.text = "Par: " + currentPar;
    }

    public void ShowEndScreen(string scoreResult)
    {
        endScreenScoreText.text = scoreResult;

        endScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void NextHole()
    {
        HoleController.instance.GoToNextHole();
    }

    public void ShowOoB()
    {
        outOfBoundsMessage.SetActive(true);
    }

    public void HideOoB()
    {
        outOfBoundsMessage.SetActive(false);
    }

    public void ShowHideScorecard()
    {
        scoreCard.gameObject.SetActive(!scoreCard.gameObject.activeSelf);

        if(scoreCard.gameObject.activeSelf == true)
        {
            scoreCard.UpdateScore();
        }
    }
}
