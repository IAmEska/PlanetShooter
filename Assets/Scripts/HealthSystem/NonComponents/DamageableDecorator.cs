using UnityEngine;
using System.Collections;

public abstract class DamageableDecorator : Damageable
{
	protected Damageable damageable;

	public DamageableDecorator (Damageable damageable)
	{
		this.damageable = damageable;
	}

	#region implemented abstract members of Damageable

	public override float GetTotalCurrentHealth ()
	{
		return damageable.GetTotalCurrentHealth ();
	}

	public override void TakeDamage (float damage)
	{
		damageable.TakeDamage (damage);
	}

	#endregion
}
