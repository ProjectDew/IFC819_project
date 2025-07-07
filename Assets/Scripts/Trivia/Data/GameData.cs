using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
	[SerializeField]
	private TriviaState triviaStateIngame;
	
	[SerializeField]
	private TriviaState triviaStatePause;

	[SerializeField]
	private float maxSpeed;

	[SerializeField]
	private float minSpeed;

	[SerializeField]
	private float timeToAnswer;

	[SerializeField]
	private float minTouchDistance;

	[SerializeField]
	private float minTutorialScore;

	[SerializeField]
	private float gazeTime;

	[SerializeField]
	private int hiddenRobots;

	public TriviaState TriviaIngame => triviaStateIngame;

	public TriviaState TriviaPause => triviaStatePause;

	public float MaxSpeed => maxSpeed;

	public float MinSpeed => minSpeed;

	public float TimeToAnswer => timeToAnswer;

	public float MinTouchDistance => minTouchDistance;

	public float MinTutorialScore => minTutorialScore;

	public float GazeTime => gazeTime;

	public int HiddenRobots => hiddenRobots;
}
