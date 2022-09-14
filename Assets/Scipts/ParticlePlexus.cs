using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePlexus : MonoBehaviour
{
	[SerializeField] private float maxDistance;
	[SerializeField] private LineRenderer lineRendererTemplate;

	private List<LineRenderer> lineRenderers= new List<LineRenderer>();
	private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.MainModule particleSystemMainModule;
    private int maxParticles;
    private int particleCount;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
	    maxParticles = particleSystemMainModule.maxParticles;

	    if (particles == null || particles.Length < maxParticles)
	    {
		    particles = new ParticleSystem.Particle[maxParticles];
	    }

	    particleSystem.GetParticles(particles);
	    particleCount = particleSystem.particleCount;

	    float maxDistanceSqr = maxDistance * maxDistance;

	    int lrIndex = 0;
	    int lineRendererCount = lineRenderers.Count;

	    for (int i = 0; i < particleCount; i++)
	    {
		    Vector3 p1Position = particles[i].position;

		    for (int j = i + 1; j < particleCount; j++)
		    {
			    Vector3 p2Position = particles[j].position;
			    float distanceSqr = Vector3.SqrMagnitude(p1Position - p2Position);

			    if (distanceSqr <= maxDistanceSqr)
			    {
				    LineRenderer lr;

					if (lrIndex == lineRendererCount)
				    {
					    lr = Instantiate(lineRendererTemplate, _transform, false);
						lineRenderers.Add(lr);

						lineRendererCount++;
				    }

					lr = lineRenderers[lrIndex];

					lr.enabled = true;

					lr.SetPosition(0, p1Position);
					lr.SetPosition(1, p2Position);

				    lrIndex++;
			    }
		    }
        }

	    for (int i = lrIndex; i < lineRendererCount; i++)
	    {
			lineRenderers[i].enabled = false;
	    }
    }
}
