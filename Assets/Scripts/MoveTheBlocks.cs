using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheBlocks : MonoBehaviour {

	[SerializeField]
	Mesh m;

	[SerializeField]
	Transform block;

	[SerializeField]
	Transform startTransform;

	[SerializeField]
	Transform endTransform;

	[SerializeField]
	float movementSpeed;

	private Rigidbody rbBlock;
	private Vector3 direction;
	private Transform destination;

	void Start()
	{
		rbBlock = block.GetComponent<Rigidbody> ();
		setDestination (startTransform);
	}

	void FixedUpdate() 
	{
		rbBlock.MovePosition (block.position + direction * movementSpeed * Time.fixedDeltaTime);

		if (Vector3.Distance (block.position, destination.position) < movementSpeed * Time.fixedDeltaTime) 
		{
			setDestination (destination == startTransform ? endTransform : startTransform);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawMesh(m, startTransform.position, block.rotation, block.localScale);

		Gizmos.color = Color.red;
		Gizmos.DrawMesh(m, endTransform.position, block.rotation, block.localScale);

	}

	void setDestination(Transform d)
	{
		destination = d;
		direction = (destination.position - block.position).normalized;
	}
}