using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleController : MonoBehaviour
{
    public static HoleController instance;

    private void Awake()
    {
        instance = this;
    }

    public int shotsTaken;

    public int par;

    public float waitAfterBallInCup;

    private Vector3 lastBallPosition;

    public float waitOutOfBounds = 2f;

    public string nextHoleScene;

    public bool ballInCup;

    // Start is called before the first frame update
    void Start()
    {
        par = CourseController.instance.holes[CourseController.instance.currentHole].par;

        UIController.instance.SetParText(par);

        SetBallPosition();

        nextHoleScene = CourseController.instance.holes[CourseController.instance.currentHole].nextSceneName;

        AudioManager.instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddShot()
    {
        shotsTaken++;

        UIController.instance.UpdateShotsText(shotsTaken);
    }

    public void BallInCup()
    {
        ballInCup = true;

        UIController.instance.DisplayInHole();

        int finalScore = shotsTaken - par;

        string finalResult = "";

        switch(finalScore)
        {
            case -4:

                finalResult = "Condor (-4)";

                break;

            case -3:

                finalResult = "Albatross (-3)";

                break;

            case -2:

                finalResult = "Eagle (-2)";

                break;

            case -1:

                finalResult = "Birdie (-1)";

                break;

            case 0:

                finalResult = "Par";

                break;

            case 1:

                finalResult = "Bogey (+1)";

                break;

            case 2:

                finalResult = "Double Bogey (+2)";

                break;

            case 3:

                finalResult = "Triple Bogey (+3)";

                break;

            default:

                if(finalScore > 0)
                {
                    finalResult = "+" + finalScore;
                } else
                {
                    finalResult = finalScore.ToString();
                }

                break;
        }

        if(shotsTaken == 1)
        {
            finalResult = "Hole-In-One!!!";
        }

        //UIController.instance.ShowEndScreen(finalScore.ToString());

        //StartCoroutine(EndHoleCo(finalScore.ToString()));

        StartCoroutine(EndHoleCo(finalResult));

        CourseController.instance.holes[CourseController.instance.currentHole].result = shotsTaken;
    }

    private IEnumerator EndHoleCo(string scoreResult)
    {
        yield return new WaitForSeconds(waitAfterBallInCup);

        UIController.instance.ShowEndScreen(scoreResult);

        CameraController.instance.PreventMovement();

        
    }

    public void SetBallPosition()
    {
        lastBallPosition = BallController.instance.transform.position;
    }

    public void OutOfBounds()
    {
        StartCoroutine(OutOfBoundsCo());
    }

    IEnumerator OutOfBoundsCo()
    {
        AddShot();

        UIController.instance.ShowOoB();

        yield return new WaitForSeconds(waitOutOfBounds);

        //BallController.instance.transform.position = lastBallPosition;

        BallController.instance.theRB.Move(lastBallPosition, BallController.instance.transform.rotation);

        BallController.instance.theRB.velocity = Vector3.zero;
        BallController.instance.theRB.angularVelocity = Vector3.zero;

        ShotController.instance.AllowShot();

        BallController.instance.InBounds();

        UIController.instance.HideOoB();
    }

    public void GoToNextHole()
    {
        SceneManager.LoadScene(nextHoleScene);

        CourseController.instance.currentHole++;
    }
}
