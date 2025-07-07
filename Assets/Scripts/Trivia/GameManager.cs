using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private ManagedBehaviour[] managedBehaviours;

	[SerializeField]
	private UIIngame ingameMenu;

	[SerializeField]
	private UITrivia triviaMenu;

	[SerializeField]
	private UIEnd endMenu;

	[SerializeField]
	private MovementTrivia movementTrivia;

	[SerializeField]
	private MovementBonus movementBonus;

	[SerializeField]
	private int rightAnswerScore;

	[SerializeField]
	private float waitingTime;

	[SerializeField]
	private float timeToReadRightAnswer;

	[SerializeField]
	private float timeToReadWrongAnswer;

	private enum State
	{
		Initializing,
		QuizMode,
		InBetween,
		RunnerMode,
		Finishing
	}

	private State currentState;
	
	private float timeToReadAnswer;

	private float waitingCounter;
	private float answerCounter;
	private float readAnswerCounter;

	private void Awake ()
	{
		for (int i = 0; i < managedBehaviours.Length; i++)
			managedBehaviours[i].ManagedAwake ();

		currentState = State.Initializing;
	}

	private void Start ()
	{
		for (int i = 0; i < managedBehaviours.Length; i++)
			managedBehaviours[i].ManagedStart ();
		
		DataManager.Instance.ResetGameData ();

		triviaMenu.OnQuestionAnswered += SetInBetweenState;
	}

	private void Update ()
	{
		if (DataManager.Instance.TriviaState.ID != "Ingame")
			return;

		UpdateBehaviours ();

		if (currentState == State.Initializing)
		{
			Initialize ();
			return;
		}
		
		if (currentState == State.InBetween)
			WaitForPlayerToReadAnswer ();

		CalculateTime ();
	}

	private void UpdateBehaviours ()
	{
		for (int i = 0; i < managedBehaviours.Length; i++)
			managedBehaviours[i].ManagedUpdate ();
	}

	private void Initialize ()
	{
		waitingCounter += Time.deltaTime;

		if (waitingCounter < waitingTime)
			return;

		movementTrivia.MoveIntoScreen ();

		waitingCounter = 0;

		currentState = State.QuizMode;
	}

	private void CalculateTime ()
	{
		answerCounter += Time.deltaTime;
		
		ingameMenu.UpdateTimer (answerCounter);

		if (answerCounter < DataManager.Instance.GameData.TimeToAnswer)
			return;
		
		movementBonus.StopLoop ();

		if (!triviaMenu.TryAssignNextQuestion ())
		{
			endMenu.Show ();
			
			DataManager.Instance.CheckTutorialCompleted ();
			DataManager.Instance.SaveRecords ();

			currentState = State.Finishing;

			return;
		}

		movementTrivia.MoveIntoScreen ();

		answerCounter = 0;

		currentState = State.QuizMode;
	}

	private void WaitForPlayerToReadAnswer ()
	{
		readAnswerCounter += Time.deltaTime;

		if (readAnswerCounter < timeToReadAnswer)
			return;

		movementTrivia.MoveOutOfScreen ();
		movementBonus.StartLoop ();

		currentState = State.RunnerMode;
	}

	private void SetInBetweenState (bool answerIsCorrect)
	{
		if (answerIsCorrect)
		{
			DataManager.Instance.Score += rightAnswerScore * (int)DataManager.Instance.MoveSpeed;
			DataManager.Instance.RightAnswers++;
			
			timeToReadAnswer = timeToReadRightAnswer;

			DataManager.Instance.MoveSpeed++;
		}
		else
		{
			timeToReadAnswer = timeToReadWrongAnswer;
			DataManager.Instance.MoveSpeed--;
		}

		currentState = State.InBetween;
	}
}
