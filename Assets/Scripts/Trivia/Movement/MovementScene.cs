using UnityEngine;

public class MovementScene : ManagedBehaviour
{
	[SerializeField]
	private float moveDistance;

	[SerializeField]
	private float speedMultiplier;

	private Transform tr;
	
	private float minPosition;

	public override void ManagedStart ()
	{
		tr = transform;
		minPosition = tr.position.x - moveDistance;
	}

	public override void ManagedUpdate ()
	{
		float movement = DataManager.Instance.MoveSpeed * speedMultiplier * Time.deltaTime;

		tr.position += movement * Vector3.left;

		if (tr.position.x < minPosition)
			tr.position = new Vector3 (tr.position.x + moveDistance, tr.position.y, tr.position.z);
	}
}
