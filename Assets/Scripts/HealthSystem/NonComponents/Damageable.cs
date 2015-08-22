using UnityEngine;
using System.Collections;

public abstract class Damageable
{
	public abstract float GetTotalCurrentHealth ();
	public abstract void TakeDamage (float damage);
}
