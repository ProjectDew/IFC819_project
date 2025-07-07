using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Scriptable Objects/QuestionData")]
public class QuestionData : ScriptableObject
{
	[SerializeField, TextArea]
	private string question;

	[SerializeField]
	private AnswerData[] answers;

	[Serializable]
	public class AnswerData
	{
		[SerializeField, TextArea]
		private string answer;

		[SerializeField]
		private bool isCorrect;

		public string Answer => answer;

		public bool IsCorrect => isCorrect;
	}

	public string Question => question;

	public AnswerData[] GetRandomizedAnswers () => (AnswerData[])answers.Shuffle ();
}
