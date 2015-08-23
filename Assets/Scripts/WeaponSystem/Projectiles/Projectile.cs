using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Projectile : MonoBehaviour
{
	public enum ProjectileState
	{
		Idle,
		Fly,
		Destroy
	}

	public float preCalculateCollisionDistance = 2f;
	public float projectileWidthCollision = 0.1f;
	public float speed = 15f;
	public float damage = 20f;
	public float lifetime = 2f;
	public LayerMask affectLayer;

	protected ProjectileState state, prevState;

	void FixedUpdate ()
	{
		if (state == ProjectileState.Fly) {
			var hits = Physics2D.CircleCastAll (transform.position, projectileWidthCollision, transform.forward, preCalculateCollisionDistance, affectLayer);
			if (hits.Length > 0) {
				foreach (RaycastHit2D hit in hits) {
					Debug.Log ("hit");
					if (!hit.transform.gameObject.tag.Equals (tag)) {
						HealthManager hm = hit.transform.gameObject.GetComponent<HealthManager> ();
						if (hm != null) {
							hm.TakeDamage (damage);
							Debug.Log ("Taking damage: " + damage);
						} else
							Debug.LogError (hit.transform.GetInstanceID () + " was hit and has no health manager");
						
						state = ProjectileState.Destroy;

						break;
					}
				}
			}
			Fly ();

		} else if (state != prevState) {
			switch (state) {
			case ProjectileState.Destroy:
				Destroy ();

				break;
			case ProjectileState.Idle:
				break;
			}
			prevState = state;
		}
	}

	public virtual void Launch (Transform origin)
	{
		tag = origin.gameObject.tag;
		transform.rotation = origin.rotation;
		transform.position = origin.position;
		gameObject.SetActive (true);

		state = ProjectileState.Fly;
		StartCoroutine (AutoDestroy ());
	}

	public virtual void Destroy ()
	{
		state = ProjectileState.Idle;
		StopAllCoroutines ();
		if (CachePool<Projectile>.Instance.ReturnObject (this)) {
			gameObject.SetActive (false);
		} else {
			DestroyObject (gameObject);
		}
	}

	IEnumerator AutoDestroy ()
	{
		yield return new WaitForSeconds (lifetime);
		if (state != ProjectileState.Destroy)
			Destroy ();
	}

	public abstract void Fly ();
}
