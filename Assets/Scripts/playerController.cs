using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour 
{
    public GameObject pauseScreen;

    public GameObject energyBar;
    public RectTransform energyGauge;

    Rigidbody myRigidbody;

    public AudioSource shootSound;
    AudioSource myShootSound;

    public AudioSource emptySound;
    AudioSource myEmptySound;

    public AudioSource camoSound;
    AudioSource myCamoSound;

    public AudioSource camoOffSound;
    AudioSource myCamoOffSound;

    public AudioSource rechargeSound;
    AudioSource myRechargeSound;

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

    public GameObject bulletRef;
    public GameObject gunRef;

    bool alreadyFired;
    bool fireRecovery;
    float currentTime;

    float rechargeDelayTime;

    float energy;

    bool camoEnabled;

    float camoDrainTime;
    float rechargeRateTime;

    float rechargePerSecond;
    float drainPerSecond;
    float rechargeDelay;

    bool startedRecharge;

	void Start () 
    {
        startedRecharge = false;

        rechargeDelay = 5f;
        rechargePerSecond = 20f;
        drainPerSecond = 20f;

        rechargeRateTime = 0f;
        camoDrainTime = 0f;
        camoEnabled = false;

        myRechargeSound = rechargeSound.GetComponent<AudioSource>();
        myCamoOffSound = camoOffSound.GetComponent<AudioSource>();
        myCamoSound = camoSound.GetComponent<AudioSource>();

        rechargeDelayTime = 0f;

        UpdateEnergy(100);

        myEmptySound = emptySound.GetComponent<AudioSource>();
        myShootSound = shootSound.GetComponent<AudioSource>();

        currentTime = 0f;
        alreadyFired = false;
        fireRecovery = false;

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
        sprintSpeed = 2250f;
        movementSpeed = 1500f;
        crouchSpeed = 750f;
        myAnimator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        if(isShooting == true)
        {
            isShooting = false;
        }

        if(camoEnabled == true)
        {
            if(Time.time > camoDrainTime)
            {
                if(energy >= 20)
                {
                    startedRecharge = false;
                    UpdateEnergy(energy - drainPerSecond);
                    rechargeDelayTime = Time.time + rechargeDelay;
                    camoDrainTime = Time.time + 1f;
                }
                else
                {
                    camoEnabled = false;
                    Instantiate(myCamoOffSound);
                    GetComponentInChildren<Renderer>().material = black;
                    currentColor = "BLACK";
                }
            } 
        }

        if(Time.time > rechargeDelayTime && energy != 100)
        {
            if(Time.time > rechargeRateTime)
            {
                if(startedRecharge == false)
                {
                    Instantiate(myRechargeSound);
                    startedRecharge = true;
                }
                UpdateEnergy(energy + rechargePerSecond);
                rechargeRateTime = Time.time + 1f;
            }
        }

        if(Time.time > currentTime && alreadyFired == true && fireRecovery == false)
        {
            GameObject shot = Instantiate(bulletRef, gunRef.transform.position, gunRef.transform.rotation);
            GunBolt bolt = shot.GetComponent<GunBolt>();
            bolt.setDir(transform.forward);
            currentTime = Time.time + 1f;
            fireRecovery = true;
        }   

        if(fireRecovery == true && Time.time > currentTime)
        {
            fireRecovery = false;
            alreadyFired = false;
        }

        if (Input.GetMouseButtonDown(0) && pauseScreen.activeSelf == false)
        {
            if(energy == 100)
            {
                if(alreadyFired == false)
                {
                    startedRecharge = false;
                    UpdateEnergy(0);
                    rechargeDelayTime = Time.time + rechargeDelay;

                    Instantiate(myShootSound);
                    isShooting = true;
                    alreadyFired = true;
                    currentTime = Time.time + 1f;
                }
            }
            else
            {
                Instantiate(myEmptySound);
            }
        }

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

        if (!mouseYEnabled)
        {
            verticalLookRotation = initCamAngle;
            mouseYEnabled = true;
        }
        transform.Rotate(Vector3.up * Input.GetAxis ("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -20, 5);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;


        //UP
        if(Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddForce(transform.forward * speed);
            isWalking = true;
            isForward = true;
        }
        //DOWN
        if(Input.GetKey(KeyCode.S))
        {
            myRigidbody.AddForce(transform.forward * -speed);
            isWalking = true;
            isBackward = true;
        }
        //LEFT
        if(Input.GetKey(KeyCode.A))
        {
            myRigidbody.AddForce(transform.right * -speed);
            isWalking = true;
            isLeft = true;
        }
        //RIGHT
        if(Input.GetKey(KeyCode.D))
        {
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
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(isCrouching == true)
            {
                isCrouching = false;
            }
            else if(isCrouching == false)
            {
                isCrouching = true;
            }
        }
        /*if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }*/

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
            
        //JUMP
        if(isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.AddForce(0,2750,0);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeSelf == false)
            {
                pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //Instantiate(myOpenPause);
            }
            else if(pauseScreen.activeSelf == true)
            {
                pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                //Instantiate(myClosePause);
            }
        }

        //RED
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!currentColor.Equals("RED"))
            {
                if(energy >= 20)
                {
                    camoEnabled = true;
                    startedRecharge = false;
                    UpdateEnergy(energy - drainPerSecond);
                    rechargeDelayTime = Time.time + rechargeDelay;
                    camoDrainTime = Time.time + 1f;

                    Instantiate(myCamoSound);
                    GetComponentInChildren<Renderer>().material = red;
    			    currentColor = "RED";
                }
                else
                {
                    Instantiate(myEmptySound);
                }
            }
        }

        //GREEN
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(!currentColor.Equals("GREEN"))
            {
                if(energy >= 20)
                {
                    camoEnabled = true;
                    startedRecharge = false;
                    UpdateEnergy(energy - drainPerSecond);
                    rechargeDelayTime = Time.time + rechargeDelay;
                    camoDrainTime = Time.time + 1f;

                    Instantiate(myCamoSound);
                    GetComponentInChildren<Renderer>().material = green;
    			    currentColor = "GREEN";
                }
                else
                {
                    Instantiate(myEmptySound);
                }
            }
        }

        //BLUE
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(!currentColor.Equals("BLUE"))
            {
                if(energy >= 20)
                {
                    camoEnabled = true;
                    startedRecharge = false;
                    UpdateEnergy(energy - drainPerSecond);
                    rechargeDelayTime = Time.time + rechargeDelay;
                    camoDrainTime = Time.time + 1f;

                    Instantiate(myCamoSound);
                    GetComponentInChildren<Renderer>().material = blue;
    			    currentColor = "BLUE";
                }
                else
                {
                    Instantiate(myEmptySound);
                }
            }
        }

        //RESET
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!currentColor.Equals("BLACK"))
            {
                camoEnabled = false;
                Instantiate(myCamoOffSound);
                GetComponentInChildren<Renderer>().material = black;
			    currentColor = "BLACK";
            }
        }

	}

    void CheckGrounded()
    {
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

    public void UpdateEnergy(float newEnergy)
    {
        energy = newEnergy;
        energyBar.GetComponent<Text>().text = "Energy: "  + energy.ToString();
        energyGauge.sizeDelta = new Vector2(energy * 2, energyGauge.sizeDelta.y);
    }

	public bool GetGrounded(){
		return isGrounded;
	}
}