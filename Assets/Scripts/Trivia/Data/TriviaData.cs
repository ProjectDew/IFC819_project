using UnityEngine;

[CreateAssetMenu(fileName = "TriviaData", menuName = "Scriptable Objects/TriviaData")]
public class TriviaData : ScriptableObject
{
	[SerializeField]
	private string id;

    [SerializeField]
	private QuestionData[] questions;

	public string ID => id;

	public int TotalQuestions => questions.Length;

	public QuestionData[] GetRandomizedQuestions () => (QuestionData[])questions.Shuffle ();
}
