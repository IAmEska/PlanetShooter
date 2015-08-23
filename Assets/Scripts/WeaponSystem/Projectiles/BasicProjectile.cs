using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TrailRenderer))]
public class BasicProjectile : Projectile
{

	protected TrailRenderer trailRenderer;

	IEnumerator EnableTrail ()
	{
		yield return new WaitForSeconds (0.1f);
		trailRenderer.enabled = true;
	}

	void Awake ()
	{
		trailRenderer = GetComponent<TrailRenderer> ();
	}

	public override void Launch (Transform origin)
	{
		base.Launch (origin);
		StartCoroutine (EnableTrail ());
	}

	public override void Destroy ()
	{
		trailRenderer.enabled = false;
		base.Destroy ();
	}

	#region implemented abstract members of Projectile

	public override void Fly ()
	{
		transform.position += transform.up * Time.deltaTime * speed;
	}

	#endregion
	
}
