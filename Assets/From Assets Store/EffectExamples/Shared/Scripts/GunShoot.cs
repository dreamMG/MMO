using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GunShoot : MonoBehaviour {


	void Start()
	{
	}
	private void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag("Enemy"))
		{
		}
	}

	
}