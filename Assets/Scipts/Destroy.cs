using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Tooltip("Time before game object is destroyed in seconds")]
    [SerializeField] private float timeUntilDeath = 0.0f;

    // Update is called once per frame
    void Update()
    {
	    Destroy(gameObject, timeUntilDeath);
    }
}
