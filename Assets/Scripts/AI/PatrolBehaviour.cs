using UnityEngine;
using System.Collections;

public class PatrolBehaviour : ShipBehaviour
{
	protected GameObject bounds;
	protected float boundSize;

	protected Enemy enemy;

	public PatrolBehaviour (Enemy enemy, GameObject bounds) : base()
	{
		this.enemy = enemy;
		this.bounds = bounds;
		boundSize = bounds.GetComponent<SpriteRenderer> ().bounds.size.y / 2;
	}

	#region implemented abstract members of ShipBehaviour

	public override void Behave ()
	{
		float distance = Mathf.Abs ((bounds.transform.position - (enemy.transform.position + enemy.transform.up * 2)).magnitude);
		Debug.Log ("distance" + distance);
		Debug.Log ("radius" + boundSize);
		if (boundSize - distance > 1) {
			enemy.GetMovementManager ().Move (0, 1);
		} else {
			enemy.transform.Rotate (enemy.transform.forward, Random.Range (-75, 75));
		}
	}

	#endregion

}
