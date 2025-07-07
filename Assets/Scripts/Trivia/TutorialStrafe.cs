using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class TutorialStrafe : MonoBehaviour
{
	[SerializeField]
	private UITrivia triviaMenu;

	[SerializeField]
	private float waitingTime;

	[SerializeField]
	private float maxPosition;

	[SerializeField]
	private float minPosition;

	[SerializeField]
	private float moveSpeed;

	private State currentState;

	private UIDocument tutorialStrafeMenu;

	private ITransform tr;

	private float waitingCounter;

	private enum State
	{
		Idle,
		Waiting,
		MovingUp,
		MovingDown
	}

	private void Awake ()
	{
		tutorialStrafeMenu = GetComponent<UIDocument> ();

		tr = tutorialStrafeMenu.rootVisualElement.transform;

		tutorialStrafeMenu.rootVisualElement.style.display = DisplayStyle.None;

		currentState = State.Idle;
	}

	private void Start ()
	{
		if (Touchscreen.current != null && !Touchscreen.current.enabled)
			InputSystem.EnableDevice (Touchscreen.current);
	}

	private void OnEnable ()
	{
		triviaMenu.OnQuestionAnswered += InitializeTutorial;
	}

	private void OnDisable ()
	{
		triviaMenu.OnQuestionAnswered -= InitializeTutorial;
	}

	private void Update ()
	{
		CheckTouchscreen ();

		if (currentState == State.Waiting)
			Wait ();
		else if (currentState == State.MovingUp)
			Move (Vector2.up);
		else if (currentState == State.MovingDown)
			Move (Vector2.down);
	}

	private void CheckTouchscreen ()
	{
		if (Touchscreen.current == null || Touchscreen.current.touches.Count == 0)
			return;

		if (currentState == State.Idle)
			return;

		Vector2 startPosition = Touchscreen.current.primaryTouch.startPosition.ReadValue ();
		Vector2 position = Touchscreen.current.primaryTouch.position.ReadValue ();

		if (Mathf.Abs (position.y - startPosition.y) > DataManager.Instance.GameData.MinTouchDistance)
		{
			Destroy (gameObject);
			return;
		}
	}

	private void Wait ()
	{
		waitingCounter += Time.deltaTime;

		if (waitingCounter < waitingTime)
			return;

		currentState = State.MovingUp;
	}

	private void Move (Vector3 direction)
	{
		tr.position += moveSpeed * Time.deltaTime * direction;

		if (tr.position.y > maxPosition)
			currentState = State.MovingDown;
		else if (tr.position.y < minPosition)
			currentState = State.MovingUp;
	}

	private void InitializeTutorial (bool isCorrect)
	{
		if (DataManager.Instance.PersistentData.TutorialCompleted)
		{
			Destroy (gameObject);
			return;
		}

		tutorialStrafeMenu.rootVisualElement.style.display = DisplayStyle.Flex;

		currentState = State.Waiting;
	}
}
