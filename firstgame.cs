using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement
    private CharacterController controller;
    public float speed = 2f;
    //------------------------------
    //Camera Controller
    private float Xrotation = 0f;
    public float mouseSensitivity = 100f;
    //------------------------------
    // jump and gravity
    private Vector3 velocity;
    private float gravity = -9.81f;
    public Transform groundChecker;
    public float groundCheckerRadius;
    public LayerMask obstacleLayer;
    private bool isground;

    public float jumpHeight = 0.1f;
    public float gravityDivision = 100f;
    public float jumpspeed = 30;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isground = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);
        //movement
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;

        controller.Move(moveVelocity);
        //----------------------------
        //Camera Controller
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);

        Xrotation -= Input.GetAxis("Mouse Y")*Time.deltaTime* mouseSensitivity;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        

        //-------------------------------
        //Jump and Gravity
        if (!isground)
        {
            velocity.y += gravity * Time.deltaTime/gravityDivision;
            speed = jumpspeed;
        }
        else
        {
            velocity.y = -0.05f;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isground)
        {
            velocity.y = Mathf.Sqrt(jumpHeight* -2f * gravity / gravityDivision);
            speed = 10;
        }


     
        controller.Move(velocity);
    }
}
