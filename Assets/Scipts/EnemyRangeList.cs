using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EnemyRangeList : MonoBehaviour
{
    // List of enemies in range of attack
    public static List<GameObject> enemiesInRange = new List<GameObject>();
	private GameObject enemy = null;

	// Set player attack range between range (0-70%)
	[Tooltip("Set the attack range of the player (0.2 - 3.0)")]
	[Range(0.2f, 3.0f)]
	[SerializeField] private float playerAttackRange = 1.3f;
	private SphereCollider attackRangeSphere = null;


    private void Start()
    {
		attackRangeSphere = GetComponent<SphereCollider>();
		attackRangeSphere.radius = playerAttackRange;
    }

    private void Update()
    {
        if (playerAttackRange != attackRangeSphere.radius)
			attackRangeSphere.radius = playerAttackRange;
	}

    // Add enemies in range of atttacks to list
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			enemy = other.transform.parent.gameObject;
			enemiesInRange.Add(enemy);
		}
	}

	// Remove enemies that leave range of atttacks from list
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			enemy = other.transform.parent.gameObject;
			enemiesInRange.Remove(enemy);
		}
	}
}
