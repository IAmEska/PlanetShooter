using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserWeapon : Weapon
{
	public float laserDamage = 20f;
	public float laserDistance = 10f;
	public float laserDuration = 1f;

	protected LineRenderer lineRenderer;

	void Start ()
	{
		lineRenderer = GetComponent<LineRenderer> ();
	}

	#region implemented abstract members of Weapon

	protected override void LaunchProjectile ()
	{
		DrawLaser ();
		var hits = Physics2D.RaycastAll (transform.position, transform.up, laserDistance, LayerMask.NameToLayer ("Destroyable"));
		foreach (RaycastHit2D hit in hits) {
			HealthManager hm = hit.transform.GetComponent<HealthManager> ();
			if (hm != null)
				hm.TakeDamage (laserDamage);
			else
				Debug.LogError (hit.transform.GetInstanceID () + " was hit and has no health manager");

		}
	}

	#endregion

	protected void DrawLaser ()
	{
		StopCoroutine (ClearLaser ());
		lineRenderer.enabled = true;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, transform.position + transform.up * laserDistance);
		StartCoroutine (ClearLaser ());
	}

	IEnumerator ClearLaser ()
	{
		yield return new WaitForSeconds (laserDuration);
		lineRenderer.enabled = false;
	}
}
