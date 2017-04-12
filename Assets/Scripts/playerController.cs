﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public GameObject billboard;
    float billboardTime;
    public AudioSource alertSound;
    AudioSource myAlertSound;
    public AudioSource searchSound;
    AudioSource mySearchSound;
    public AudioSource stunSound;
    AudioSource myStunSound;

    public GameObject outroText;

    public GameObject switchController;
    switchScript mySwitchController;

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

    public AudioSource crouchSound;
    AudioSource myCrouchSound;

    public AudioSource deathSound;
    AudioSource myDeathSound;

    public AudioSource jumpSound;
    AudioSource myJumpSound;

    public AudioSource openCloseMenuSound;
    AudioSource myOpenCloseMenuSound;

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

    bool isPunching;

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

    //Alec's smooth drain variables: will clear other stuff once done
    enum state {full, draining, delay, regen };
    state curState;
    float curEnergy;
    const float maxEnergy = 100f;
    float drainRate = 20f;
    float rechargeRate = 15f;
    float regenDelay = 10f;
    float delayTime;
    //end energy stuff

    bool camoEnabled;

    float camoDrainTime;
    float rechargeRateTime;

    float rechargePerSecond;
    float drainPerSecond;
    float rechargeDelay;
    float jumpDelay;

    bool startedRecharge;

    //camera stuff
    
    CameraBehaviour camScript;

	void Start () 
    {
        //Debug.Log(transform.position);
        //Debug.Log(transform.rotation);
        camScript = cameraTransform.GetComponent<CameraBehaviour>();
        curState = state.full;
        curEnergy = maxEnergy;
        delayTime = 0;

        billboardTime = 0f;
        mySearchSound = searchSound.GetComponent<AudioSource>();
        myStunSound = stunSound.GetComponent<AudioSource>();
        myAlertSound = alertSound.GetComponent<AudioSource>();

        isPunching = false;
        myOpenCloseMenuSound = openCloseMenuSound.GetComponent<AudioSource>();
        jumpDelay = 0f;
        myJumpSound = jumpSound.GetComponent<AudioSource>();
        myDeathSound = deathSound.GetComponent<AudioSource>();
        myCrouchSound = crouchSound.GetComponent<AudioSource>();
        mySwitchController = switchController.GetComponent<switchScript>();

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
        //Debug.DrawLine(transform.position + Vector3.up * 8, camScript.targetPosition.localPosition, Color.red);

        /*if(curState == state.full)
        {
            

            if (camoEnabled)
            {
                curState = state.draining;
            }
            
        }

        else if(curState == state.delay)
        {
            if (delayTime >= regenDelay)
            {
                delayTime = 0;
                Instantiate(myRechargeSound);
                curState = state.regen;
            }
            else delayTime += Time.deltaTime;

            if (camoEnabled) //condition only met when energy is non-empty
            {
                curState = state.draining;
            }
        }

        else if(curState == state.draining)
        {
            if (curEnergy <= 0)
            {
                curEnergy = 0;
                camoEnabled = false;
                Instantiate(myCamoOffSound);
                GetComponentInChildren<Renderer>().material = black;
                currentColor = "BLACK";
                curState = state.delay;
            }
            else curEnergy -= Time.deltaTime * drainRate;
        }
        else //regen
        {
            if (curEnergy >= maxEnergy)
            {
                curEnergy = maxEnergy;
                curState = state.full;
            }
            else curEnergy += Time.deltaTime * rechargeRate;
        }

        //END OF ENERGY STATE BEHAVIOR~~~~~~~~~~~~~~~~~~

        if (Input.GetMouseButtonDown(0) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if (curState == state.full)
            {
                if (alreadyFired == false)
                {
                    curEnergy = 0;
                    curState = state.delay;

                    Instantiate(myShootSound);
                    isShooting = true;
                    alreadyFired = true;
                    //currentTime = Time.time + 1f;
                }
            }
            else
            {
                Instantiate(myEmptySound);
            }
        }

        //RED
        if (Input.GetKeyDown(KeyCode.Alpha1) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if (!currentColor.Equals("RED"))
            {
                if (curEnergy > 0)
                {
                    camoEnabled = true;
                    //startedRecharge = false;
                    //UpdateEnergy(energy - drainPerSecond);
                    //rechargeDelayTime = Time.time + rechargeDelay;
                    //camoDrainTime = Time.time + 1f;

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
        if (Input.GetKeyDown(KeyCode.Alpha2) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if (!currentColor.Equals("GREEN"))
            {
                if (curEnergy > 0)
                {
                    camoEnabled = true;
                    //startedRecharge = false;
                    //UpdateEnergy(energy - drainPerSecond);
                    //rechargeDelayTime = Time.time + rechargeDelay;
                    //camoDrainTime = Time.time + 1f;

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
        if (Input.GetKeyDown(KeyCode.Alpha3) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if (!currentColor.Equals("BLUE"))
            {
                if (curEnergy > 0)
                {
                    camoEnabled = true;
                    //startedRecharge = false;
                    //UpdateEnergy(energy - drainPerSecond);
                    //rechargeDelayTime = Time.time + rechargeDelay;
                    //camoDrainTime = Time.time + 1f;

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
        if (Input.GetKeyDown(KeyCode.Tab) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if (!currentColor.Equals("BLACK"))
            {
                curState = state.delay;
                camoEnabled = false;
                Instantiate(myCamoOffSound);
                GetComponentInChildren<Renderer>().material = black;
                currentColor = "BLACK";
            }
        }*/

        //end of Alec's stuff~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

        myAnimator.SetBool("isPunching", isPunching);

        if(isShooting == true)
        {
            isShooting = false;
        }

        if(isPunching == true)
        {
            isPunching = false;
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
           
            if(cameraTransform.gameObject.GetComponent<CameraBehaviour>().GetButtonPressed() == true)
            {
                bolt.setDir(cameraTransform.forward);
            }
            else
            {
                bolt.setDir(transform.forward);
            }
            currentTime = Time.time + 1f;
            fireRecovery = true;
        }   

        if(fireRecovery == true && Time.time > currentTime)
        {
            fireRecovery = false;
            alreadyFired = false;
        }

        if (Input.GetMouseButtonDown(0) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
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

        if(pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            transform.Rotate(Vector3.up * Input.GetAxis ("Mouse X") * mouseSensitivityX);
            verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -20, 6);
            cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
        }

        //STUNNED BILLBOARD
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(billboard.transform.GetChild(2).gameObject.activeSelf == false)
            {
                Instantiate(myStunSound);
                billboard.transform.GetChild(0).gameObject.SetActive(false);
                billboard.transform.GetChild(1).gameObject.SetActive(false);

                billboard.transform.GetChild(2).gameObject.SetActive(true);

                billboardTime = Time.time + 2f;
            }
        }
            
        //SEARCH BILLBOARD
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(billboard.transform.GetChild(1).gameObject.activeSelf == false)
            {
                Instantiate(mySearchSound);

                billboard.transform.GetChild(0).gameObject.SetActive(false);
                billboard.transform.GetChild(2).gameObject.SetActive(false);

                billboard.transform.GetChild(1).gameObject.SetActive(true);

                billboardTime = Time.time + 2f;
            }
        }

        //ALERT BILLBOARD
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(billboard.transform.GetChild(0).gameObject.activeSelf == false)
            {
                Instantiate(myAlertSound);

                billboard.transform.GetChild(1).gameObject.SetActive(false);
                billboard.transform.GetChild(2).gameObject.SetActive(false);

                billboard.transform.GetChild(0).gameObject.SetActive(true);

                billboardTime = Time.time + 2f;
            }
        }

        if(Time.time > billboardTime)
        {
            billboard.transform.GetChild(0).gameObject.SetActive(false);
            billboard.transform.GetChild(1).gameObject.SetActive(false);
            billboard.transform.GetChild(2).gameObject.SetActive(false);
        }

        //ALTERNATE 3RD PERSON CAMERA
        /*Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero) {
            float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0.2f);
        }*/


        //UP
        if(Input.GetKey(KeyCode.W) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            myRigidbody.AddForce(transform.forward * speed);
            isWalking = true;
            isForward = true;
        }
        //DOWN
        if(Input.GetKey(KeyCode.S) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            myRigidbody.AddForce(transform.forward * -speed);
            isWalking = true;
            isBackward = true;
        }
        //LEFT
        if(Input.GetKey(KeyCode.A) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            myRigidbody.AddForce(transform.right * -speed);
            isWalking = true;
            isLeft = true;
        }
        //RIGHT
        if(Input.GetKey(KeyCode.D) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
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
        if(Input.GetKeyDown(KeyCode.LeftControl) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if(isCrouching == true)
            {
                if(isRunning == false && isGrounded == true)
                {
                    Instantiate(myCrouchSound);
                }
                isCrouching = false;
				gameObject.GetComponent<CapsuleCollider> ().enabled = true;
				gameObject.GetComponent<BoxCollider> ().enabled = false;
            }
            else if(isCrouching == false && isGrounded == true)
            {
                if(isRunning == false)
                {
                    Instantiate(myCrouchSound);
                }
                isCrouching = true;
				gameObject.GetComponent<CapsuleCollider> ().enabled = false;
				gameObject.GetComponent<BoxCollider> ().enabled = true;
            }
        }
        /*if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }*/

        //RUN
        if(isWalking == true && isForward == true && isLeft == false && isRight == false && isBackward == false)
        {
            if(Input.GetKey(KeyCode.LeftShift) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
            {
                isCrouching = false;
                isRunning = true;
				gameObject.GetComponent<CapsuleCollider> ().enabled = true;
				gameObject.GetComponent<BoxCollider> ().enabled = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || isWalking == false || isCrouching == true)
        {
            isRunning = false;
        }
            
        //JUMP
        if(isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space) && pauseScreen.activeSelf == false && outroText.activeSelf == false && Time.time > jumpDelay)
            {
                isCrouching = false;
                myRigidbody.AddForce(0,2750,0);
                Instantiate(myJumpSound);
                jumpDelay = Time.time + 1f;
				gameObject.GetComponent<CapsuleCollider> ().enabled = true;
				gameObject.GetComponent<BoxCollider> ().enabled = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && outroText.activeSelf == false)
        {
            if(pauseScreen.activeSelf == false)
            {
                Instantiate(myOpenCloseMenuSound);
                pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                //Instantiate(myOpenPause);
            }
            else if(pauseScreen.activeSelf == true)
            {
                Instantiate(myOpenCloseMenuSound);
                pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                //Instantiate(myClosePause);
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            isPunching = true;
        }

        
        //RED
        if(Input.GetKeyDown(KeyCode.Alpha1) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
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
        if(Input.GetKeyDown(KeyCode.Alpha2) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
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
        if(Input.GetKeyDown(KeyCode.Alpha3) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
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
        if(Input.GetKeyDown(KeyCode.Tab) && pauseScreen.activeSelf == false && outroText.activeSelf == false)
        {
            if(!currentColor.Equals("BLACK"))
            {
                camoEnabled = false;
                Instantiate(myCamoOffSound);
                GetComponentInChildren<Renderer>().material = black;
			    currentColor = "BLACK";
            }
        }
        adjustCamera();
	}

    void adjustCamera()
    {
        float dist = 10f;
        Ray ray = new Ray(transform.position + Vector3.up * 8, camScript.initialPosition.position - (transform.position + Vector3.up * 8));
        RaycastHit info;
        //Debug.DrawRay(transform.position + Vector3.up * 8, camScript.initialPosition.position - (transform.position + Vector3.up * 8), Color.red);

        if (Physics.Raycast(ray, out info, dist))
        {
            //print("obstructed by "+info.collider.name);
            //cameraTransform.position = Vector3.Lerp(cameraTransform.position, camScript.targetPosition.position)
            camScript.obstructed = true;
        }
        else camScript.obstructed = false;
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

    void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;

        if(obj.tag == "Portal")
        {
            Instantiate(myOpenCloseMenuSound);
            outroText.SetActive(true);
            Time.timeScale = 0;
            //SceneManager.LoadScene("MainMenu");
            Destroy(GameObject.Find("exitOpenSound(Clone)"));
        }

        /*if(obj.tag == "Enemy")
        {
            Respawn();
        }*/
    }

    void OnCollisionStay(Collision col)
    {
        GameObject obj = col.gameObject;

        if(obj.tag == "Switch1")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isPunching = true;
                obj.SetActive(false);
                mySwitchController.ActivateSwitch1();
            }
        }

        if(obj.tag == "Switch2")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isPunching = true;
                obj.SetActive(false);
                mySwitchController.ActivateSwitch2();
            }
        }

        if(obj.tag == "Switch3")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isPunching = true;
                obj.SetActive(false);
                mySwitchController.ActivateSwitch3();
            }
        }

        if(obj.tag == "Switch4")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isPunching = true;
                obj.SetActive(false);
                mySwitchController.ActivateSwitch4();
            }
        }
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
        //curEnergy = newEnergy;
        energyBar.GetComponent<Text>().text = "Energy: "  + energy.ToString();
        energyGauge.sizeDelta = new Vector2(energy * 2, energyGauge.sizeDelta.y);
    }

	public bool GetGrounded(){
		return isGrounded;
	}

	public bool GetCrouched(){
		return isCrouching;
	}

    public void Respawn()
    {
        Instantiate(myDeathSound);

        transform.position = new Vector3(186.2f, 67.2f, -179f);
        transform.rotation = new Quaternion(0f, -0.4f, 0f, 0.9f);
    }
}