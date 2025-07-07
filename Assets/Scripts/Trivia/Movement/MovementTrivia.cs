using UnityEngine;
using UnityEngine.UIElements;

public class MovementTrivia : ManagedBehaviour
{
	[SerializeField]
	private UIDocument triviaMenu;
	
	[SerializeField]
	private Vector3 offsetPosition;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float distanceToChangeState;

	private enum State
	{
		Waiting,
		MovingIntoScreen,
		MovingToDefault,
		MovingOutOfScreen,
		MovingToHidden,
	}

	private State currentState;

	private ITransform triviaTransform;
	
	private Vector3 hiddenPosition;

	public void MoveIntoScreen ()
	{
		currentState = State.MovingIntoScreen;
	}

	public void MoveOutOfScreen ()
	{
		currentState = State.MovingOutOfScreen;
	}

	public override void ManagedAwake ()
	{
		triviaTransform = triviaMenu.rootVisualElement.transform;

		hiddenPosition = new (0f, -Screen.height, 0f);
		triviaTransform.position = hiddenPosition;

		currentState = State.Waiting;
	}

	public override void ManagedUpdate ()
	{
		switch (currentState)
		{
			case State.MovingIntoScreen:
				Interpolate (offsetPosition, State.MovingToDefault);
				break;

			case State.MovingToDefault:
				Interpolate (Vector3.zero, State.Waiting);
				break;
				
			case State.MovingOutOfScreen:
				Interpolate (offsetPosition, State.MovingToHidden);
				break;

			case State.MovingToHidden:
				Interpolate (hiddenPosition, State.Waiting);
				break;

			default:
				break;
		}
	}

	private void Interpolate (Vector3 goalPosition, State nextState)
	{
		triviaTransform.position = Vector3.Lerp (triviaTransform.position, goalPosition, moveSpeed * Time.deltaTime);

		if (Vector3.Distance (triviaTransform.position, goalPosition) > distanceToChangeState)
			return;

		triviaTransform.position = goalPosition;

		currentState = nextState;
	}
}
