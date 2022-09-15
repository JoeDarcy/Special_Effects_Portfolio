using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private float speed;
	private Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
	    rb.AddForce(Vector3.forward * -speed, ForceMode.Force);
    }
}
