using UnityEngine;
using System.Collections;

public class LandMove : MoveMethod
{

	public Vector2 moveAround;

	public LandMove (Vector2 position) : base()
	{
		this.moveAround = position;
	}

	#region implemented abstract members of MoveMethod

	public override void Move (float x, float y, float horizontalSpeed, float verticalSpeed, Transform transform, float deltaTime)
	{
		transform.RotateAround (moveAround, transform.forward, -x * deltaTime * horizontalSpeed);
	}

	#endregion

}
