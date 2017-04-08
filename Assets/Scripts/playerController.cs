using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour 
{
    Rigidbody myRigidbody;

    

    public Transform cameraTransform;

    float movementSpeed;
    float crouchSpeed;
    float sprintSpeed;
    float speed;

    public Material red;
    public Material green;
    public Material blue;

    Animator myAnimator;

    bool isWalking;
    bool isRunning;
    bool isCrouching;
    bool isShooting;
    bool isJumping;

    bool isLeft;
    bool isRight;
    bool isForward;
    bool isBackward;

    float mouseSensitivityX;
    float mouseSensitivityY;
    float verticalLookRotation;
    bool mouseYEnabled;
    float initCamAngle;

	void Start () 
    {
        speed = 0;
        isLeft = false;
        isRight = false;
        isForward = false;
        isBackward = false;

        mouseYEnabled = false;
        mouseSensitivityX = 1.25f;
        mouseSensitivityY = 1.25f;
        initCamAngle = -cameraTransform.localEulerAngles.x;

        isWalking = false;
        isRunning = false;
        isCrouching = false;
        isShooting = false;
        isJumping = false;

        myRigidbody = GetComponent<Rigidbody>();
        sprintSpeed = 60f;
        movementSpeed = 30f;
        crouchSpeed = 15f;
        myAnimator = GetComponent<Animator>();
	}
	
	void Update () 
    {
        myAnimator.SetBool("isWalking", isWalking);
        myAnimator.SetBool("isRunning", isRunning);
        myAnimator.SetBool("isCrouching", isCrouching);
        myAnimator.SetBool("isShooting", isShooting);
        myAnimator.SetBool("isJumping", isJumping);

        myAnimator.SetBool("isLeft", isLeft);
        myAnimator.SetBool("isRight", isRight);
        myAnimator.SetBool("isForward", isForward);
        myAnimator.SetBool("isBackward", isBackward);

        if(isRunning == true)
        {
            speed =  sprintSpeed;
        }
        else if(isCrouching == true)
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = movementSpeed;
        }

        if(isShooting == true)
        {
            isShooting = false;
        }

        if (!mouseYEnabled)
        {
            verticalLookRotation = initCamAngle;
            mouseYEnabled = true;
        }
        transform.Rotate(Vector3.up * Input.GetAxis ("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -40, 5);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;


        //UP
        if(Input.GetKey(KeyCode.W))
        {
            myRigidbody.transform.Translate(0,0, speed * Time.deltaTime);
            isWalking = true;
            isForward = true;
        }
        //DOWN
        if(Input.GetKey(KeyCode.S))
        {
            myRigidbody.transform.Translate(0,0, -speed * Time.deltaTime);
            isWalking = true;
            isBackward = true;
        }
        //LEFT
        if(Input.GetKey(KeyCode.A))
        {
            myRigidbody.transform.Translate(-speed * Time.deltaTime,0,0);
            isWalking = true;
            isLeft = true;
        }
        //RIGHT
        if(Input.GetKey(KeyCode.D))
        {
            myRigidbody.transform.Translate(speed * Time.deltaTime,0,0);
            isWalking = true;
            isRight = true;
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            isForward = false;
            isWalking = false;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            isBackward = false;
            isWalking = false;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            isLeft = false;
            isWalking = false;
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            isRight = false;
            isWalking = false;
        }

        //CROUCH
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        //RUN
        if(isWalking == true && isCrouching == false && isForward == true && isLeft == false && isRight == false && isBackward == false)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        //SHOOT
        if(Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }
            
        //JUMP
        if(Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(0,4000,0);
        }

        //RED
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponentInChildren<Renderer>().material = red;
        }

        //GREEN
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponentInChildren<Renderer>().material = green;
        }

        //BLUE
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponentInChildren<Renderer>().material = blue;
        }

	}
}
