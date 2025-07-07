using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using TouchPhase = UnityEngine.InputSystem.TouchPhase;

[RequireComponent (typeof (Animator))]
public class Controller : ManagedBehaviour
{
	[SerializeField]
	private float strafePosition;

	[SerializeField]
	private float strafeSpeed;

	private Transform tr;
	private Animator animator;

	private Vector3 goalPosition;

	public override void ManagedAwake ()
	{
		animator = GetComponent<Animator> ();

		tr = transform;

		goalPosition = tr.position;

		if (Touchscreen.current != null)
			InputSystem.EnableDevice (Touchscreen.current);
		else
			TouchSimulation.Enable ();
	}

	public override void ManagedUpdate ()
	{
		animator.SetFloat ("X", DataManager.Instance.MoveSpeed);

		Strafe ();

		if (tr.position != goalPosition)
			tr.position = Vector3.Lerp (tr.position, goalPosition, strafeSpeed * Time.deltaTime);
	}

	private void Strafe ()
	{
		if (Touchscreen.current == null)
			return;

		if (Touchscreen.current.touches.Count == 0)
			return;

		TouchPhase touchPhase = Touchscreen.current.primaryTouch.phase.ReadValue ();

		if (touchPhase == TouchPhase.Canceled || touchPhase == TouchPhase.Ended)
		{
			goalPosition = new (tr.position.x, tr.position.y, 0);
			return;
		}

		Vector2 startPosition = Touchscreen.current.primaryTouch.startPosition.ReadValue ();
		Vector2 position = Touchscreen.current.primaryTouch.position.ReadValue ();

		float offsetPositionY = position.y - startPosition.y;

		float positionZ = 0;

		if (offsetPositionY > DataManager.Instance.GameData.MinTouchDistance)
			positionZ = strafePosition;
		else if (offsetPositionY < -DataManager.Instance.GameData.MinTouchDistance)
			positionZ = -strafePosition;

		goalPosition = new (tr.position.x, tr.position.y, positionZ);
	}
}
