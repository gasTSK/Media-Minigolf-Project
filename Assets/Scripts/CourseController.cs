using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseController : MonoBehaviour
{
    public static CourseController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    public string courseName;

    //public string[] holes;
    public HoleInfo[] holes;

    public int currentHole;

    public int overallScore;

    public string firstScene;

    public void UpdateOverallScore()
    {
        overallScore = 0;

        for(int i = 0; i < currentHole; i++)
        {
            overallScore += holes[i].result - holes[i].par;
        }

        if (HoleController.instance != null)
        {
            if (HoleController.instance.ballInCup == true)
            {
                overallScore = 0;

                for (int i = 0; i <= currentHole; i++)
                {
                    overallScore += holes[i].result - holes[i].par;
                }
            }
        }
    }

    public void ResetCourse()
    {
        overallScore = 0;

        currentHole = 0;

        foreach(HoleInfo hole in holes)
        {
            hole.result = 0;
        }
    }
}

[System.Serializable]
public class HoleInfo
{
    public string nextSceneName;
    public int par;
    public int result;
}
