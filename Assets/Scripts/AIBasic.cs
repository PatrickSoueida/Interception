using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson{

	public class AIBasic : MonoBehaviour {

        public Transform enemy; 
		public NavMeshAgent agent;
		public ThirdPersonCharacter character;

		public enum State{
			PATROL,
			CHASE,
			RETURN
		}

		public State state;
		private bool alive;

        public bool sawPlayer;

		//Variables for Wandering
		public GameObject[] waypoints;
		public Transform returnPoint;
		private int waypointIndex;
		public float patrolSpeed = 0.5f;

		//Variables for Chasing
		public float chaseSpeed = 1f;
		public GameObject target;

		void Start () {

			agent = GetComponent<NavMeshAgent> ();
			character = GetComponent<ThirdPersonCharacter> ();

			agent.updatePosition = true;
			agent.updateRotation = false;

			if(gameObject.tag == "AI"){
				waypoints = GameObject.FindGameObjectsWithTag ("Waypoint Set 1");
				waypointIndex = Random.Range (0, waypoints.Length);
			}

			state = AIBasic.State.PATROL;
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
				}

				yield return null;
			}
		
		}

		void Patrol()
        {
			agent.speed = patrolSpeed;
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
            Vector3 direction = (enemy.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(enemy.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
            float realSpeed = 20f;
            transform.Translate(direction * realSpeed * Time.deltaTime, Space.World);
			
		}

		void Return(){
			
		}
			
		void Update ()
        {
            Vector3 targetDir = enemy.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            if (angle < 15.0f)
            {
                print("close");
                sawPlayer = true;
            }

            if (sawPlayer == true)
            {
                Chase();
            }
            // add running animation
		
		}
	}
}

