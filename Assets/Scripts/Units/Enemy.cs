using UnityEngine;
using System.Collections;

public class Enemy : Ship
{
	protected ShipBehaviour behaviour;
	public Player player;
	public GameObject bounds;
	public float chaseDistance = 10f;
	public float onChaseDistance = 5f;
	public Vector3 spawnPosition;

	#region implemented abstract members of Ship

	protected override void AliveBehaviour ()
	{
		CheckBehaviour ();
		if (behaviour != null)
			behaviour.Behave ();
	}

	#endregion

	protected void CheckBehaviour ()
	{
		if (Mathf.Abs ((transform.position - player.transform.position).magnitude) <= chaseDistance) {
			if (!(behaviour is ChaseBehaviour)) {
				behaviour = new ChaseBehaviour (this, player);
			}
		} else if (!(behaviour is PatrolBehaviour)) {
			behaviour = new PatrolBehaviour (this, bounds);
		}
	}

	protected override void RespawnBehaviour ()
	{
		behaviour = null;
		transform.position = spawnPosition;
		base.RespawnBehaviour ();
	}

	public WeaponManager GetWeaponManager ()
	{
		return this.weaponManager;
	}

	public MovementManager GetMovementManager ()
	{
		return this.movementManager;
	}

}
