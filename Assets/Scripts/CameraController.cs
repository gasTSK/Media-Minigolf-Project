using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform target;

    public float moveSpeed;

    private float rotation;

    private float vertRot;
    public Transform vertPoint;

    public GameObject directionIndicator;

    private bool canMove;

    //public bool useMouseRotation;

    // Start is called before the first frame update
    void Start()
    {
        vertRot = vertPoint.localRotation.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Confined;

        canMove = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canMove == true)
        {
            transform.position = target.position;

            /*
            //if (useMouseRotation == false)
            {
                rotation += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
                vertRot += Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            } //else
            {
                rotation += Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
                vertRot += Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
            }
            */

            rotation += Mathf.Clamp(Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X"), -1f, 1f) * moveSpeed * Time.deltaTime;
            vertRot += Mathf.Clamp(Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y"), -1f, 1f) * moveSpeed * Time.deltaTime;

            vertRot = Mathf.Clamp(vertRot, 0f, 75f);


            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            vertPoint.localRotation = Quaternion.Euler(vertRot, 0f, 0f);
        }

    }

    public void ShowIndicator()
    {
        directionIndicator.SetActive(true);
    }

    public void HideIndicator()
    {
        directionIndicator.SetActive(false);
    }

    public void PreventMovement()
    {
        canMove = false;
    }
}
