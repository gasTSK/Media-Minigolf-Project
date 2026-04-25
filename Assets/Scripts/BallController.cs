using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    private void Awake()
    {
        instance = this;
    }

    public Rigidbody theRB;

    public float hitPower;

    public float stopCutoff;
    public float stopSpeed = .95f;

    private CameraController theCam;

    private float noMoveCounter;
    public float waitToEndShot = .5f;

    private bool isOutOfBounds;

    private Vector3 lastVelocity;
    public float bounceSoundThreshold = .5f;

    // Start is called before the first frame update
    void Start()
    {
        theCam = FindFirstObjectByType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.F))
        {
            //theRB.velocity = Vector3.forward * hitPower;

            theRB.velocity = theCam.transform.forward * hitPower;

            theCam.HideIndicator();
        } */

        float speed = theRB.velocity.magnitude;

        //Debug.Log(theRB.velocity.magnitude);

        if (theRB.velocity.y > -.01f)
        {

            if (speed < stopCutoff)
            {
                theRB.velocity = theRB.velocity * stopSpeed;

                if (speed < .01f)
                {
                    theRB.velocity = Vector3.zero;
                    theRB.angularVelocity = Vector3.zero;

                    //theCam.ShowIndicator();

                    //ShotController.instance.AllowShot();
                }
            }
        }

        if(speed > .01f)
        {
            noMoveCounter = waitToEndShot;
        } else
        {
            if(noMoveCounter > 0f)
            {
                noMoveCounter -= Time.deltaTime;
                if(noMoveCounter <= 0f)
                {
                    //theCam.ShowIndicator();

                    if (isOutOfBounds == false)
                    {

                        ShotController.instance.AllowShot();
                    }
                }
            }
        }

        if(Vector3.Magnitude(theRB.velocity - lastVelocity) > bounceSoundThreshold && lastVelocity != Vector3.zero)
        {
            AudioManager.instance.PlaySFX(0);
        }

        lastVelocity = theRB.velocity;
    }

    public void ShootBall(float shotForce)
    {
        theRB.velocity = theCam.transform.forward * shotForce;

        //theCam.HideIndicator();

        noMoveCounter = waitToEndShot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Out Of Bounds") && isOutOfBounds == false)
        {
            isOutOfBounds = true;

            HoleController.instance.OutOfBounds();
        }
    }

    public void InBounds()
    {
        isOutOfBounds = false;
    }
}
