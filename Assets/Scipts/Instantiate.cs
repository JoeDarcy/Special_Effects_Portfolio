using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
	[SerializeField] private GameObject prefab;
	[SerializeField] private float spawnTime;

	private GameObject prefabInstance;

	private float timer;

	// Update is called once per frame
	void Update()
    {
	    
    }
}
