using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UITrivia : ManagedBehaviour
{
	[SerializeField]
	private UIDocument triviaMenu;

	[SerializeField]
	private Color rightAnswerColor;

	[SerializeField]
	private Color wrongAnswerColor;

	private Color defaultButtonColor;

	private Label questionLabel;

	private Button[] answerButtons;
	
	private QuestionData[] activeQuestions;
	private QuestionData.AnswerData[] activeAnswers;

	private int currentQuestionIndex;

	private QuestionData CurrentQuestion => activeQuestions[currentQuestionIndex];

	public delegate void QuestionAnswered (bool isCorrect);
	public event QuestionAnswered OnQuestionAnswered;

	public bool TryAssignNextQuestion ()
	{
		currentQuestionIndex++;
		
		if (currentQuestionIndex < 0 || currentQuestionIndex >= activeQuestions.Length)
			return false;

		questionLabel.text = CurrentQuestion.Question;
		
		activeAnswers = CurrentQuestion.GetRandomizedAnswers ();

		for (int i = 0; i < answerButtons.Length; i++)
		{
			answerButtons[i].style.backgroundColor = defaultButtonColor;
			answerButtons[i].text = activeAnswers[i].Answer;
		}

		return true;
	}

	public override void ManagedStart ()
	{
		questionLabel = triviaMenu.rootVisualElement.Q<Label> ("Question");
		
		PrepareQuestions ();

		questionLabel.text = CurrentQuestion.Question;
	}

	private void PrepareQuestions ()
	{
		activeQuestions = DataManager.Instance.TriviaData.GetRandomizedQuestions ();

		if (activeQuestions == null || activeQuestions.Length == 0)
			throw new Exception ("There are no questions assigned yet.");

		currentQuestionIndex = 0;
		
		activeAnswers = CurrentQuestion.GetRandomizedAnswers ();

		if (activeAnswers == null || activeAnswers.Length == 0)
			throw new Exception ("The question has no answers assigned.");

		answerButtons = new Button[activeAnswers.Length];

		for (int i = 0; i < answerButtons.Length; i++)
		{
			answerButtons[i] = triviaMenu.rootVisualElement.Q<Button> ($"Answer{i}");
			
			if (answerButtons[i] == null)
				throw new Exception ($"There is no button with the name \"Answer{i}\".");

			if (i == 0)
				defaultButtonColor = answerButtons[i].style.backgroundColor.value;

			answerButtons[i].text = activeAnswers[i].Answer;
		}
		
		answerButtons[0].clicked += CheckAnswerA;
		answerButtons[1].clicked += CheckAnswerB;
		answerButtons[2].clicked += CheckAnswerC;
	}

	private void CheckAnswer (int index)
	{
		bool answerIsCorrect = false;

		for (int i = 0; i < answerButtons.Length; i++)
		{
			if (activeAnswers[i].IsCorrect)
			{
				answerButtons[i].style.backgroundColor = rightAnswerColor;

				answerIsCorrect = index == i;

				continue;
			}

			if (index == i)
				answerButtons[i].style.backgroundColor = wrongAnswerColor;
		}

		OnQuestionAnswered?.Invoke (answerIsCorrect);
	}

	private void CheckAnswerA () => CheckAnswer (0);

	private void CheckAnswerB () => CheckAnswer (1);

	private void CheckAnswerC () => CheckAnswer (2);
}
