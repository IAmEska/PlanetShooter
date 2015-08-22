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

	public float preCalculateCollisionDistance = 1f;
	public float projectileWidthCollision = 0.1f;
	public float speed = 15f;
	public float damage = 20f;
	public float lifetime = 2f;

	protected ProjectileState state, prevState;
	
	protected string originTag;

	void FixedUpdate ()
	{
		if (state == ProjectileState.Fly) {
			var hits = Physics2D.CircleCastAll (transform.position, projectileWidthCollision, transform.forward, preCalculateCollisionDistance, LayerMask.NameToLayer ("Destroyable"));
			if (hits.Length > 0) {
				foreach (RaycastHit2D hit in hits) {
					if (!hit.transform.gameObject.tag.Equals (originTag)) {
						HealthManager hm = hit.transform.GetComponent<HealthManager> ();
						if (hm != null)
							hm.TakeDamage (damage);
						else
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

	public void Launch (Transform origin)
	{
		originTag = origin.gameObject.tag;
		gameObject.SetActive (true);
		transform.rotation = origin.rotation;
		transform.position = origin.position;
		state = ProjectileState.Fly;
		StartCoroutine (AutoDestroy ());
	}

	public virtual void Destroy ()
	{
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
