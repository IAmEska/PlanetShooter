using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Bounds : MonoBehaviour
{

	void OnTriggerExit2D (Collider2D other)
	{
		Debug.Log ("Exit");
		var manager = other.GetComponent<HealthManager> ();
		if (manager != null)
			manager.TakeDamage (1000);
	}

}
