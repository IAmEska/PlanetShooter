using UnityEngine;
using System.Collections;

public class ChaseBehaviour : ShipBehaviour
{


	protected Enemy enemy;
	protected Player player;

	public ChaseBehaviour (Enemy enemy, Player player)
	{
		this.enemy = enemy;
		this.player = player;
	}

	#region implemented abstract members of ShipBehaviour

	public override void Behave ()
	{
		var dir = (enemy.transform.position - player.transform.position).normalized;
		float rotZ = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion newQuat = Quaternion.Euler (0, 0, rotZ + 90);
		enemy.transform.rotation = Quaternion.Lerp (enemy.transform.rotation, newQuat, Time.deltaTime * enemy.GetMovementManager ().horizontalSpeed);

		if (Mathf.Abs (newQuat.z - enemy.transform.rotation.z) < 5)
			enemy.GetWeaponManager ().Fire ();

		if (Mathf.Abs ((enemy.transform.position - player.transform.position).magnitude) > enemy.onChaseDistance) {
			enemy.GetMovementManager ().Move (0, 1);
		}
	}

	#endregion

}
