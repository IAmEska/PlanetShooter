using UnityEngine;
using System.Collections;

public class Health : Damageable
{

	public float health;

	public Health (float health)
	{
		this.health = health;
	}

	#region implemented abstract members of Damageable
	
	public override float GetTotalCurrentHealth ()
	{
		return health;
	}
	
	public override void TakeDamage (float damage)
	{
		health = Mathf.Max (0, health - damage);
	}
	
	#endregion

}
