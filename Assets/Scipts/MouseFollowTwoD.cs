using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowTwoD : MonoBehaviour
{
    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    Rigidbody rb;
    private Vector2 position = new Vector2(0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
	    mousePosition = Input.mousePosition;
	    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
	    position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

	private void FixedUpdate()
	{
		rb.MovePosition(position);
	}
}
