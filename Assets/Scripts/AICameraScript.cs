using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson{

	public class AICameraScript : MonoBehaviour {

        public GameObject billboard;

        float billboardTime;

        public AudioSource alertSound;
        AudioSource myAlertSound;

        public AudioSource searchSound;
        AudioSource mySearchSound;

        public AudioSource stunSound;
        AudioSource myStunSound;

        public NavMeshAgent agent;
		public ThirdPersonCharacter character;
		public playerController Player;

        bool alert;
        bool searching;
        bool stunned;

        Animator myAnimator;

		public enum State{
			PATROL,
			CHASE,
			RETURN,
			STUNNED
		}

		public State state;
		private bool alive;
		private int totalNumOfAI = 41;
		private int lastState;

		//public bool sawPlayer;

		//Variables for Wandering
		public GameObject[] waypoints;
		public Transform returnPoint;
		private int waypointIndex;
		public float patrolSpeed = 0.5f;

		//Variables for Sight
		public float sightDist = 50.0f;

		//Variables for Camera Sight
		public Collider playerColl;
		public Camera AICam;
		private Plane[] planes;

		//Variables for Chasing
		public float chaseSpeed = 1f;
		public GameObject target;

		void Start () 
        {
            myAnimator = GetComponent<Animator>();

            alert = false;
            searching = true;
            stunned = false;

            mySearchSound = searchSound.GetComponent<AudioSource>();
            myStunSound = stunSound.GetComponent<AudioSource>();
            myAlertSound = alertSound.GetComponent<AudioSource>();
            billboardTime = 0f;

            agent = GetComponent<NavMeshAgent> ();
			character = GetComponent<ThirdPersonCharacter> ();
			GetComponent<Rigidbody> ().freezeRotation = true;

			agent.updatePosition = true;
			agent.updateRotation = false;

			//Each AI will have a different tag with their own patrol points
			for(int i = 1; i <= totalNumOfAI; i++){
				if (gameObject.tag == "AI") {
					waypoints = GameObject.FindGameObjectsWithTag ("Waypoint Set 1");
					waypointIndex = Random.Range (0, waypoints.Length);
				} 
				else {
					if (gameObject.tag == "AI " + i) {
						waypoints = GameObject.FindGameObjectsWithTag ("Waypoint Set " + i);
						waypointIndex = Random.Range (0, waypoints.Length);
					}
				}
			}

			playerColl = Player.GetComponent<CapsuleCollider> ();

			state = AICameraScript.State.PATROL;
			alive = true;

			StartCoroutine ("FSM");

		}

		IEnumerator FSM(){

			while (alive) {
				switch (state) {
				case State.PATROL:
					Patrol ();
					break;
				case State.CHASE:
					Chase ();
					break;
				case State.RETURN:
					Return ();
					break;
				case State.STUNNED:
					Stunned ();
					break;
				}

				yield return null;
			}

		}

		void Patrol()
		{
            alert = false;
            stunned = false;
            //question mark

            if(billboard.transform.GetChild(1).gameObject.activeSelf == false && searching == false)
            {
                searching = true;

                Instantiate(mySearchSound);

                billboard.transform.GetChild(0).gameObject.SetActive(false);
                billboard.transform.GetChild(2).gameObject.SetActive(false);

                billboard.transform.GetChild(1).gameObject.SetActive(true);

                billboardTime = Time.time + 2f;
            }	

            agent.speed = patrolSpeed;
				float tempY = transform.rotation.y;
				transform.Rotate (0, tempY, 0);
				if (Vector3.Distance (this.transform.position, waypoints [waypointIndex].transform.position) >= 2) {
					agent.SetDestination (waypoints [waypointIndex].transform.position);
					character.Move (agent.desiredVelocity, false, false);
				} else if (Vector3.Distance (this.transform.position, waypoints [waypointIndex].transform.position) <= 2) {
					waypointIndex = Random.Range (0, waypoints.Length);
				} else {
					character.Move (Vector3.zero, false, false);
				}
		}

		void Chase()
		{
            searching = false;
            stunned = false;

			//Display Exclamation Mark Above Head?
            if(billboard.transform.GetChild(0).gameObject.activeSelf == false && alert == false)
            {
                alert = true;
                Instantiate(myAlertSound);

                billboard.transform.GetChild(1).gameObject.SetActive(false);
                billboard.transform.GetChild(2).gameObject.SetActive(false);

                billboard.transform.GetChild(0).gameObject.SetActive(true);

                billboardTime = Time.time + 2f;
            }

			//Matt's Code
			float tempY = transform.rotation.y;
			transform.Rotate (0, tempY, 0);
			agent.speed = chaseSpeed;
			agent.SetDestination (target.transform.position);
			character.Move (agent.desiredVelocity, false, false);

			//Memo's Code
			/*Vector3 direction = (target.transform.position - transform.position).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
            float realSpeed = 20f;
            transform.Translate(direction * realSpeed * Time.deltaTime, Space.World);*/

		}
			
		void Return(){

            agent.speed = patrolSpeed;
			agent.SetDestination (returnPoint.position);
			character.Move (agent.desiredVelocity, false, false);
			SetState ("PATROL");

		}

		//void Stunned(){
		//	StartCoroutine(Stun ());
		//}

		//IEnumerator Stun(){
        void Stunned(){
            alert = false;
            searching = false;
            //stunned 
            if(billboard.transform.GetChild(2).gameObject.activeSelf == false && stunned == false)
            {
                stunned = true;

                Instantiate(myStunSound);
                billboard.transform.GetChild(0).gameObject.SetActive(false);
                billboard.transform.GetChild(1).gameObject.SetActive(false);

                billboard.transform.GetChild(2).gameObject.SetActive(true);

                billboardTime = Time.time + 3.5f;
            }

            agent.speed = 0f;
			character.Move (Vector3.zero, false, false);
			//gameObject.GetComponentInChildren<Camera> ().enabled = false;
			//Debug.Log ("AI is stunned!");
			//yield return new WaitForSeconds(3.5f);
			//SetState ("PATROL");
		}

		void Update ()
		{
            myAnimator.SetBool("isStunned", stunned);

            //Memo's Code
			/*Vector3 targetDir = target.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            if (angle < 5.0f)
            {
                print("close");
                sawPlayer = true;
            }

            if (sawPlayer == true)
                Chase();*/

            if(Time.time > billboardTime)
            {
                billboard.transform.GetChild(0).gameObject.SetActive(false);
                billboard.transform.GetChild(1).gameObject.SetActive(false);

                if(billboard.transform.GetChild(2).gameObject.activeSelf == true)
                {
                    SetState ("PATROL");
                    billboard.transform.GetChild(2).gameObject.SetActive(false);
                }
            }

			if (Player.GetCrouched ()) {
				//playerColl = Player.GetComponent<BoxCollider> ();
				playerColl = Player.GetCollider (2);
			} else {
				//playerColl = Player.GetComponent<CapsuleCollider> ();
				playerColl = Player.GetCollider (1);
			}

			if (!Player.GetCamo ()) {
				planes = GeometryUtility.CalculateFrustumPlanes (AICam);
				if (GeometryUtility.TestPlanesAABB (planes, playerColl.bounds)) {
					//Debug.Log ("Player Sighted");
					CheckForPlayer ();
				} 
				else {
				}

			} else {
				SetState ("PATROL");
			}

		}

		void OnTriggerEnter(Collider coll){
			if (coll.gameObject.tag == "Player") {
				SetState ("PATROL");
			} else {
				if (coll.tag == "Bolt") {
					//Debug.Log ("You shot the AI!");
					lastState = GetState ();
					Destroy (coll.gameObject);
					SetState ("STUNNED");
				}
			}
		}

		void CheckForPlayer(){
            RaycastHit hit;
			Debug.DrawRay (AICam.transform.position, transform.forward * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, transform.right * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, -transform.right * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + transform.right).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward - transform.right).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.forward + transform.right)).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.forward - transform.right)).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward - transform.right))).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward + transform.right))).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward + (transform.forward - transform.right)))).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward + (transform.forward + transform.right)))).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + (transform.right/2)).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward - (transform.right/2)).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, (transform.forward + transform.up).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, ((transform.forward + transform.up) + transform.right).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, ((transform.forward + transform.up) - transform.right).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, ((transform.forward + transform.up) + transform.forward + transform.right).normalized * sightDist, Color.yellow);
			Debug.DrawRay (AICam.transform.position, ((transform.forward + transform.up) + transform.forward - transform.right).normalized * sightDist, Color.yellow);
			if (Physics.Raycast (AICam.transform.position, transform.forward, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, transform.right, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, -transform.right, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward - transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.forward + transform.right)).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.forward - transform.right)).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward - transform.right))).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward + transform.right))).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward + (transform.forward - transform.right)))).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.forward + (transform.forward + (transform.forward + transform.right)))).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + (transform.right/2)).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward - (transform.right/2)).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, (transform.forward + transform.up).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, ((transform.forward + transform.up) + transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, ((transform.forward + transform.up) - transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, ((transform.forward + transform.up) + transform.forward + transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
			if (Physics.Raycast (AICam.transform.position, ((transform.forward + transform.up) + transform.forward - transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (state != AICameraScript.State.STUNNED) {
						state = AICameraScript.State.CHASE;
						target = hit.collider.gameObject;
					}
				}
			}
		}

		public void SetState(string newState){
			if (newState == "CHASE") {
				state = AICameraScript.State.CHASE;
			}
			if (newState == "PATROL") {
				state = AICameraScript.State.PATROL;
			}
			if (newState == "RETURN") {
				state = AICameraScript.State.RETURN;
			}
			if (newState == "STUNNED") {
				state = AICameraScript.State.STUNNED;
			}
		}

		public void SetTarget(GameObject newTarget){
			target = newTarget;
		}

		public int GetState(){
			int temp = (int)state;
			return temp;
		}
	}
}


