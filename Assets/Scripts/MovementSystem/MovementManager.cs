using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour
{
	public enum MoveType
	{
		Fly,
		Landing,
		LandMove
	}

	public MoveType type = MoveType.Fly;

	public float horizontalSpeed;
	public float verticalSpeed;

	protected MoveMethod actualMethod = new BasicMoveMethod ();

	void FixedUpdate ()
	{
		if (actualMethod is LandingMove)
			actualMethod.Move (0, 0, horizontalSpeed, verticalSpeed, transform, Time.deltaTime);
	}

	public void SetHorizontalSpeed (float speed)
	{
		this.horizontalSpeed = speed;
	}

	public void SetVerticalSpeed (float speed)
	{
		this.verticalSpeed = speed;
	}

	public void Move (float x, float y)
	{
		if (!(actualMethod is LandingMove))
			actualMethod.Move (x, y, horizontalSpeed, verticalSpeed, transform, Time.deltaTime);

		if (actualMethod is LandMove && y > 0)
			LiftOff ();

	}

	public void PrepareForLanding (Vector2 landPosition, Vector2 planetPosition)
	{
		var method = new LandingMove (landPosition, planetPosition);
		method.Landed += MoveOnPlanet;
		actualMethod = method;
		type = MoveType.Landing;
	}

	public void MoveOnPlanet (Vector2 planetPosition)
	{
		if (actualMethod is LandingMove)
			(actualMethod as LandingMove).Landed -= MoveOnPlanet;

		actualMethod = new LandMove (planetPosition);
		type = MoveType.LandMove;
	}


	public void LiftOff ()
	{
		actualMethod = new BasicMoveMethod ();
		type = MoveType.Fly;
	}
}
