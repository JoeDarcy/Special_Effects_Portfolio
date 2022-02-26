using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 10.0f;
	private Rigidbody playerRigidbody;

	private void Start()
	{
		playerRigidbody = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
	    if (Input.GetKey(KeyCode.W))
	    {
		    playerRigidbody.MovePosition(transform.position + Vector3.forward * moveSpeed * Time.deltaTime);
		}

	    if (Input.GetKey(KeyCode.S))
	    {
		    playerRigidbody.MovePosition(transform.position - Vector3.forward * moveSpeed * Time.deltaTime);
	    }

	    if (Input.GetKey(KeyCode.D))
	    {
		    playerRigidbody.MovePosition(transform.position + Vector3.right * moveSpeed * Time.deltaTime);
	    }

	    if (Input.GetKey(KeyCode.A))
	    {
		    playerRigidbody.MovePosition(transform.position - Vector3.right * moveSpeed * Time.deltaTime);
	    }
	}
}
