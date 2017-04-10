using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson{

	public class AISight : MonoBehaviour {

		public NavMeshAgent agent;
		public ThirdPersonCharacter character;
		public playerController Player;

		public enum State{
			PATROL,
			CHASE,
			INVESTIGATE,
			RETURN
		}

		public State state;
		private bool alive;
		private int totalNumOfAI = 3;

		//public bool sawPlayer;

		//Variables for Wandering
		public GameObject[] waypoints;
		public Transform returnPoint;
		private int waypointIndex;
		public float patrolSpeed = 0.5f;

		//Variables for Investigating
		private Vector3 investigateSpot;
		private float timer = 0;
		public float investigateWait = 2;

		//Variables for Sight
		public float heightMultiplier;
		public float sightDist = 30.0f;

		//Variables for Chasing
		public float chaseSpeed = 1f;
		public GameObject target;

		void Start () {

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

			/*if(gameObject.tag == "AI"){
				waypoints = GameObject.FindGameObjectsWithTag ("Waypoint Set 1");
				waypointIndex = Random.Range (0, waypoints.Length);
			}

			if(gameObject.tag == "AI 2"){
				waypoints = GameObject.FindGameObjectsWithTag ("Waypoint Set 2");
				waypointIndex = Random.Range (0, waypoints.Length);
			}*/

			state = AISight.State.PATROL;
			alive = true;

			heightMultiplier = 9f;

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
				case State.INVESTIGATE:
					Investigate ();
					break;
				case State.RETURN:
					Return ();
					break;
				}

				yield return null;
			}

		}

		void Patrol()
		{
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

			//Display Exclamation Mark Above Head?

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

		void Investigate(){
			
			//Display Question Mark Above Head?

			timer += Time.deltaTime;
			agent.SetDestination (this.transform.position);
			character.Move (Vector3.zero, false, false);
			transform.LookAt (investigateSpot);
			if (timer >= investigateWait) {
				state = AISight.State.PATROL;
				timer = 0;
			}
		}

		void Return(){

			agent.speed = patrolSpeed;
			agent.SetDestination (returnPoint.position);
			character.Move (agent.desiredVelocity, false, false);
			SetState ("PATROL");

		}

		void Update ()
		{

			if (Player.GetCamo () && GetState () == 1) {
				SetState ("PATROL");
			}

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

		}

		void OnTriggerEnter(Collider coll){
			if (coll.tag == "Player") {
				state = AISight.State.INVESTIGATE;
				investigateSpot = coll.gameObject.transform.position;
			}
		}

		void FixedUpdate(){
			//Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.yellow);
			//Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.yellow);
			//Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.yellow);
			RaycastHit hit;
			if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					state = AISight.State.CHASE;
					target = hit.collider.gameObject;
				}
			}
			if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					state = AISight.State.CHASE;
					target = hit.collider.gameObject;
				}
			}
			if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist)) {
				if (hit.collider.gameObject.tag == "Player") {
					state = AISight.State.CHASE;
					target = hit.collider.gameObject;
				}
			}
			 
		}

		public void SetState(string newState){
			if (newState == "CHASE") {
				state = AISight.State.CHASE;
			}
			if (newState == "PATROL") {
				state = AISight.State.PATROL;
			}
			if (newState == "INVESTIGATE") {
				state = AISight.State.INVESTIGATE;
			}
			if (newState == "RETURN") {
				state = AISight.State.RETURN;
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


