using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private GameObject explosion;
	private GameObject explosionInstance;
	private ParticleSystem effectParticleSystem;
	
	// OnTriggerEnter is called when the Collider other enters the trigger.
	protected void OnTriggerEnter(Collider other)
	{
		explosionInstance = Instantiate(explosion, transform.position, transform.rotation);
		effectParticleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
		effectParticleSystem.Stop();
		Destroy(gameObject, 2.0f);
	}
}
