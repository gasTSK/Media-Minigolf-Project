using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorecardController : MonoBehaviour
{
    public TMP_Text overallScore;

    public TMP_Text[] parTexts;
    public TMP_Text[] scoreTexts;

    public void UpdateScore()
    {
        CourseController.instance.UpdateOverallScore();

        overallScore.text = "Overall Score: " + CourseController.instance.overallScore;

        for(int i = 0; i < parTexts.Length; i++)
        {
            if (i < CourseController.instance.holes.Length)
            {
                parTexts[i].text = CourseController.instance.holes[i].par.ToString();

                if (CourseController.instance.currentHole > i || (CourseController.instance.currentHole == i && HoleController.instance.ballInCup == true))
                {
                    scoreTexts[i].text = CourseController.instance.holes[i].result.ToString();
                } else
                {
                    scoreTexts[i].text = "-";
                }
            } else
            {
                parTexts[i].text = "";

                scoreTexts[i].text = string.Empty;
            }
        }
    }
}
