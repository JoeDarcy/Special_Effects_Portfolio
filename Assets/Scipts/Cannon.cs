using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	[SerializeField] private GameObject projectile;
	[SerializeField] private float shootForce;

	private GameObject projectileInstance;


	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
		}

		if (projectileInstance)
		{
			projectileInstance.GetComponent<Rigidbody>().AddForce(projectileInstance.transform.forward * shootForce);
		}
	}
}
