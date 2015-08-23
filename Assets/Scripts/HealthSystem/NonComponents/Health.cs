using UnityEngine;
using System.Collections;

public class Health : Damageable
{

	public float health;

	protected float defaultHealth;

	public Health (float health)
	{
		this.health = health;
		defaultHealth = health;
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

	public override void Reset ()
	{
		health = defaultHealth;
	}

	public override float GetBasicHealthPercentage ()
	{
		return health / defaultHealth;
	}

	#endregion

}
