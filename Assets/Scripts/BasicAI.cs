using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson{

	public class BasicAI : MonoBehaviour {

		public NavMeshAgent agent;
		public ThirdPersonCharacter character;

		public enum State{
			PATROL,
			CHASE,
			RETURN
		}

		public State state;
		private bool alive;

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

			state = BasicAI.State.PATROL;
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

		void Patrol(){
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

		void Chase(){
			
		}

		void Return(){
			
		}
			
		void Update () {
		
		}
	}
}

