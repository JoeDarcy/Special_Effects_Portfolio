using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSeek : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float force = 10.0f;       // Seek
    //[SerializeField] private float fleeForce = 10.0f;
    //[SerializeField] private float alignForce = 10.0f;

    private ParticleSystem emitterParticleSystem;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.MainModule emitterParticleSystemMainModule;
    private int maxParticles;

    // Start is called before the first frame update
    void Start()
    {
	    emitterParticleSystem = GetComponent<ParticleSystem>();
	    emitterParticleSystemMainModule = emitterParticleSystem.main;
	    maxParticles = emitterParticleSystemMainModule.maxParticles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
	    if (particles == null || particles.Length < maxParticles)
	    {
		    particles = new ParticleSystem.Particle[maxParticles];
        }

	    // Populate the array with currently active particles
        emitterParticleSystem.GetParticles(particles);

        // Precalculate force * deltatime
        float forceDeltaTime = force * Time.deltaTime;

        // Save target position
        Vector3 targetTransformPosition;

        switch (emitterParticleSystemMainModule.simulationSpace)
        {
	        case ParticleSystemSimulationSpace.Local:
	        {
		        targetTransformPosition = transform.InverseTransformPoint(target.position);
		        break;
	        }
	        case ParticleSystemSimulationSpace.Custom:
	        {
		        targetTransformPosition = emitterParticleSystemMainModule.customSimulationSpace.InverseTransformPoint(target.position);
		        break;
	        }
	        case ParticleSystemSimulationSpace.World:
	        {
		        targetTransformPosition = target.position;
		        break;
	        }
			default:
			{
				targetTransformPosition = target.position;
				break;
			}
        }
        
        // Iterate over the particles
        for (int i = 0; i < particles.Length; i++)
        {
	        // Get vector to target
	        Vector3 directionToTarget = Vector3.Normalize(targetTransformPosition - particles[i].position);
            // Calculate seek force for the particle
            Vector3 seekForce = directionToTarget * forceDeltaTime;
            // Apply seek force to the particle
            particles[i].velocity += seekForce;
        }
        // Set the new velocity values for each particle back to the particle system from the modified arrayj
        emitterParticleSystem.SetParticles(particles, particles.Length);
    }
}
