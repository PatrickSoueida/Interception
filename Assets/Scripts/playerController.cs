using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour 
{
    Rigidbody myRigidbody;

    

    public Transform cameraTransform;
    public LayerMask groundedMask;

    float movementSpeed;
    float crouchSpeed;
    float sprintSpeed;
    float speed;

    public Material red;
    public Material green;
    public Material blue;
    public Material black;
	public string currentColor;

	public bool camouflaged;

    Animator myAnimator;

    bool isWalking;
    bool isRunning;
    bool isCrouching;
    bool isShooting;

    bool isGrounded;

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
        isGrounded = true;
        speed = 0;
        isLeft = false;
        isRight = false;
        isForward = false;
        isBackward = false;

		camouflaged = false;

        mouseYEnabled = false;
        mouseSensitivityX = 1.5f;
        mouseSensitivityY = 1.5f;
        initCamAngle = -cameraTransform.localEulerAngles.x;

		currentColor = "BLACK";

        isWalking = false;
        isRunning = false;
        isCrouching = false;
        isShooting = false;

        myRigidbody = GetComponent<Rigidbody>();
        //sprintSpeed = 60f;
        //movementSpeed = 30f;
        //crouchSpeed = 15f;
        sprintSpeed = 3000f;
        movementSpeed = 1500f;
        crouchSpeed = 750f;
        myAnimator = GetComponent<Animator>();
        Screen.lockCursor = true;
	}
	
	void Update () 
    {
        CheckGrounded();

        myAnimator.SetBool("isGrounded", isGrounded);

        myAnimator.SetBool("isWalking", isWalking);
        myAnimator.SetBool("isRunning", isRunning);
        myAnimator.SetBool("isCrouching", isCrouching);
        myAnimator.SetBool("isShooting", isShooting);

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
            //myRigidbody.transform.Translate(0,0, speed * Time.deltaTime);
            myRigidbody.AddForce(transform.forward * speed);
            isWalking = true;
            isForward = true;
        }
        //DOWN
        if(Input.GetKey(KeyCode.S))
        {
            //myRigidbody.transform.Translate(0,0, -speed * Time.deltaTime);
            myRigidbody.AddForce(transform.forward * -speed);
            isWalking = true;
            isBackward = true;
        }
        //LEFT
        if(Input.GetKey(KeyCode.A))
        {
            //myRigidbody.transform.Translate(-speed * Time.deltaTime,0,0);
            myRigidbody.AddForce(transform.right * -speed);
            isWalking = true;
            isLeft = true;
        }
        //RIGHT
        if(Input.GetKey(KeyCode.D))
        {
            //myRigidbody.transform.Translate(speed * Time.deltaTime,0,0);
            myRigidbody.AddForce(transform.right * speed);
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
        if(Input.GetKeyUp(KeyCode.LeftShift) || isWalking == false || isCrouching == true)
        {
            isRunning = false;
        }

        //SHOOT
        if(Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }
            
        //JUMP
        if(isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.AddForce(0,4000,0);
            }
        }

        //RED
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponentInChildren<Renderer>().material = red;
			currentColor = "RED";
        }

        //GREEN
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponentInChildren<Renderer>().material = green;
			currentColor = "GREEN";
        }

        //BLUE
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponentInChildren<Renderer>().material = blue;
			currentColor = "BLUE";
        }

        //RESET
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            GetComponentInChildren<Renderer>().material = black;
			currentColor = "BLACK";
        }

	}

    void CheckGrounded()
    {
       /*Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }*/

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, groundedMask);
        foreach(Collider col in colliders)
        {
            if(col.gameObject != gameObject)
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }

	public string GetColor(){
		return currentColor;
	}

	public bool GetCamo(){
		return camouflaged;
	}

	public void SetCamo(bool value){
		camouflaged = value;
	}
}
