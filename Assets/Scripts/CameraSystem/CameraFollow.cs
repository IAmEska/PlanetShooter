using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

	public GameObject objectToFollow;
	public float followSpeed = 5f;
	protected float defaultDistance;

	void Start ()
	{
		defaultDistance = transform.position.z;
	}

	void LateUpdate ()
	{
		Vector3 newPos = Vector3.Lerp (transform.position, objectToFollow.transform.position, Time.deltaTime * followSpeed);
		newPos.z = defaultDistance;
		transform.position = newPos;
	}
}
