using UnityEngine;
using System.Collections;

public class BasicMoveMethod : MoveMethod
{
	#region implemented abstract members of MoveMethod
	public override void Move (float x, float y, float horizontalSpeed, float verticalSpeed, Transform transform, float deltaTime)
	{
		transform.position += y * transform.up * deltaTime * verticalSpeed;
		transform.Rotate (-x * transform.forward * deltaTime * horizontalSpeed);
	}
	#endregion
}
