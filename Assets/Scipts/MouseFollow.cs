using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float xDisplacement;
    [SerializeField] private float yDisplacement;

    private Vector3 mousePosition;

    // Update is called once per frame
    void Update()
    {
	    
            mousePosition = Input.mousePosition;
	        mousePosition.z = 10;
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePosition.x / xDisplacement, mousePosition.y / yDisplacement, mousePosition.z),moveSpeed * Time.deltaTime);
            Debug.Log("Mouse: " + mousePosition);
            Debug.Log("Effect: " + transform.position);
    }
}
