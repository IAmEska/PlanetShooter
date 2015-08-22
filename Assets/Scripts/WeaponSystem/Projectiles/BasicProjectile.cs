using UnityEngine;
using System.Collections;

public class BasicProjectile : Projectile
{

	#region implemented abstract members of Projectile

	public override void Fly ()
	{
		transform.position += transform.up * Time.deltaTime * speed;
	}

	#endregion
	
}
