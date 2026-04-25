using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public static ShotController instance;

    private void Awake()
    {
        instance = this;
    }

    public float maxShotPower;

    private bool canShoot;

    private BallController theBall;

    private float activeShotPower;
    public float powerChangeSpeed;
    private bool powerGrowing;

    private bool inCup;

    // Start is called before the first frame update
    void Start()
    {
        //canShoot = true;

        theBall = FindFirstObjectByType<BallController>();

        AllowShot();
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot == true)
        {
            if(activeShotPower == maxShotPower)
            {
                powerGrowing = false;
            } else if(activeShotPower == 0f)
            {
                powerGrowing = true;
            }



            if(powerGrowing == true)
            {
                activeShotPower = Mathf.MoveTowards(activeShotPower, maxShotPower, powerChangeSpeed * Time.deltaTime);
            } else
            {
                activeShotPower = Mathf.MoveTowards(activeShotPower, 0f, powerChangeSpeed * Time.deltaTime);
            }

            UIController.instance.SetPowerBar(activeShotPower, maxShotPower);


            if(Input.GetMouseButtonDown(0))
            {
                //theBall.ShootBall(maxShotPower);

                FireShot();
            }
        }
    }

    public void AllowShot()
    {
        if (inCup == false)
        {
            canShoot = true;

            UIController.instance.ShowPowerBar();

            activeShotPower = 0f;
            powerGrowing = true;

            UIController.instance.SetPowerBar(activeShotPower, maxShotPower);

            CameraController.instance.ShowIndicator();
        }
    }

    void FireShot()
    {
        HoleController.instance.SetBallPosition();

        theBall.ShootBall(activeShotPower);

        //canShoot = false;

        //UIController.instance.HidePowerBar();

        PreventShot();

        HoleController.instance.AddShot();

        AudioManager.instance.PlaySFX(1);
    }

    public void SetInCup()
    {
        inCup = true;

        PreventShot();
    }

    public void PreventShot()
    {
        canShoot = false;

        UIController.instance.HidePowerBar();

        CameraController.instance.HideIndicator();
    }
}
