using UnityEngine;
using System.Collections;

public abstract class MoveMethod
{

	public abstract void Move (float x, float y, float horizontalSpeed, float verticalSpeed, Transform transform, float deltaTime);

}
