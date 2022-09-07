using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
	[SerializeField] private float scalerValue;

	private Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);

    // Start is called before the first frame update
    void Start()
    {
	    transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	    if (Time.time <= 9.7f)
	    {
		    if (transform.localScale.x < maxScale.x)
			    transform.localScale += new Vector3(scalerValue * Time.deltaTime, scalerValue * Time.deltaTime, scalerValue * Time.deltaTime);
        }
	    else
	    {
		    transform.localScale -= new Vector3((scalerValue * 2) * Time.deltaTime, (scalerValue * 2) * Time.deltaTime, (scalerValue * 2) * Time.deltaTime);
		}
    }
}
