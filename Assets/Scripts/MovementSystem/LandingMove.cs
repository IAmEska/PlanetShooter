using UnityEngine;
using System.Collections;

public class LandingMove : MoveMethod
{
	public delegate void Land (Vector2 planetPosition);
	public event Land Landed;

	protected Vector2 position, planetPosition;
	public LandingMove (Vector2 position, Vector2 planetPosition) : base()
	{
		this.position = position;
		this.planetPosition = planetPosition;
	}


	#region implemented abstract members of MoveMethod

	public override void Move (float x, float y, float horizontalSpeed, float verticalSpeed, Transform transform, float deltaTime)
	{
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);

		if (pos != position) {

			var dir = (pos - position).normalized;
			float rotZ = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);

			pos = Vector2.Lerp (transform.position, position, deltaTime * verticalSpeed);
			transform.position = pos;
			if (Mathf.Abs ((pos - position).magnitude) <= 0.015f)
				transform.position = position;

		} else {
			if (Landed != null)
				Landed (planetPosition);
		}
	}

	#endregion

}
