using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HomePlanet : MonoBehaviour
{

	public string playerTag = "Player";
	public float landingTime = 3f;
	public float landingDistance = 0.5f;

	protected Transform player;
	protected float realLandingDistance;

	IEnumerator LandingCountDown ()
	{
		yield return new WaitForSeconds (landingTime);
		var manager = player.gameObject.GetComponent<MovementManager> ();

		Debug.Log ("Landing dir: " + (player.position - transform.position).normalized);
		var dir = (player.position - transform.position);
		var distance = transform.position + dir.normalized * realLandingDistance;
		manager.PrepareForLanding (distance, transform.position);
	}

	void Start ()
	{
		var renderer = transform.GetComponent<SpriteRenderer> ();
		realLandingDistance = renderer.bounds.size.x / 2 + landingDistance;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag.Equals (playerTag)) {
			player = other.transform;
			StartCoroutine (LandingCountDown ());
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag.Equals (playerTag)) {
			player = null;
			StopAllCoroutines ();
		}
	}
}
