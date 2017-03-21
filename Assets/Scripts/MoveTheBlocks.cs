using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheBlocks : MonoBehaviour {

	float maxHeight, minHeight;

	void Start()
	{
		maxHeight = 0.35f;
		minHeight = 0.2f;
	}

	void Update ()
	{
		float hoverHeight = (maxHeight + minHeight) / 2.0f;
		float hoverRange = maxHeight - minHeight;
		float hoverSpeed = 2.0f;

		this.transform.position = new Vector3 (this.transform.position.x, hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange, this.transform.position.z);
	}
}