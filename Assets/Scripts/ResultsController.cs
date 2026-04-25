using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour
{
    public ScorecardController scorecard;

    public TMP_Text overallResultText;

    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        scorecard.UpdateScore();

        int finalScore = CourseController.instance.overallScore;

        switch(finalScore)
        {
            case > 0:

                overallResultText.text = "+" + finalScore;

                break;

            case 0:

                overallResultText.text = "Par";

                break;

            case < 0:

                overallResultText.text = finalScore.ToString();

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void RestartCourse()
    {
        CourseController.instance.ResetCourse();

        SceneManager.LoadScene(CourseController.instance.firstScene);
    }
}
