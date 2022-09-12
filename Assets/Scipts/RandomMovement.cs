using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
	[SerializeField] private float changeSpeed;
	[SerializeField] private float lerpDuration;
	[SerializeField] private float maxRange;
	[SerializeField] private float minRange;
	[SerializeField] private float verticalOffset;
	private Vector3 target;
    private float timer;
    private float elapsedTime;
    private float percentageComplete;

    // Start is called before the first frame update
    void Start()
    {
	    timer = changeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
	    timer -= Time.deltaTime;
	    elapsedTime += Time.deltaTime;
	    percentageComplete = elapsedTime / lerpDuration;

		if (timer <= 0)
	    {
		    elapsedTime = 0;
			target = new Vector3(Random.Range(minRange, maxRange), verticalOffset, Random.Range(minRange, maxRange));
		    timer = changeSpeed;
		}

	    transform.position = Vector3.Lerp(transform.position, target, percentageComplete);

    }
}
