using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowTwoD : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private Camera mainCamera;
    
    private Vector3 mousePosition;


    // Update is called once per frame
    void Update()
    {
	    mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
	    transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);

	    Debug.Log("Mouse: " + mousePosition);
	    //Debug.Log("Effect: " + transform.position);
    }
}
