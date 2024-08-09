using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float timerStart;
    [SerializeField] private float pauseTime;
    [SerializeField] private float restartTime;


	private float timer;


	private void Start()
	{
		timer = timerStart;
	}

	// Update is called once per frame
	void Update()
    {
        
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            explosion.Play();
            timer = timerStart;
	    }
    

        timer -= Time.deltaTime;

        if (timer <= timerStart * pauseTime)
		{
			explosion.Pause();
		}
    }
}
