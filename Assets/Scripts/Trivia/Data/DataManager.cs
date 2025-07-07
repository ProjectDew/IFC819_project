using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	private static DataManager dataManager;

	[SerializeField]
	private GameData gameData;

	[SerializeField]
	private string fileName;

	private float moveSpeed;

	private int score;

	public static DataManager Instance => dataManager;

	public GameData GameData => gameData;

	public PersistentData PersistentData { get; private set; }

	public TriviaState TriviaState { get; private set; }

	public TriviaData TriviaData { get; set; }

	public float MoveSpeed
	{
		get => moveSpeed;
		set => moveSpeed = Mathf.Clamp (value, GameData.MinSpeed, GameData.MaxSpeed);
	}

	public int Score
	{
		get => score;
		set
		{
			score = value;
			OnUpdateScore?.Invoke (score);
		}
	}
	
	public int RightAnswers { get; set; }

	public delegate void UpdateScore (int score);
	public event UpdateScore OnUpdateScore;

	private string FilePath => Path.Combine (Application.persistentDataPath, fileName);

	public void ResetGameData ()
	{
		score = 0;
		RightAnswers = 0;
		MoveSpeed = GameData.MinSpeed;

		SetTriviaIngame ();
	}

	public void SetTriviaIngame () => TriviaState = GameData.TriviaIngame;

	public void SetTriviaPause () => TriviaState = GameData.TriviaPause;

	public void SaveRobotFound (int idNumber)
	{
		PersistentData.SetRobotFound (idNumber);
		SaveData ();
	}

	public void CheckTutorialCompleted ()
	{
		if (PersistentData.TutorialCompleted)
			return;

		if (score < GameData.MinTutorialScore)
			return;
		
		PersistentData.TutorialCompleted = true;
		
		SaveData ();
	}

	public void SaveRecords ()
	{
		PersistentData.SetHighestScore (TriviaData, Score);
		PersistentData.SetTotalRightAnswers (TriviaData, RightAnswers);
		
		SaveData ();
	}

	private void Awake ()
	{
		if (dataManager == null)
			dataManager = this;
		else
			Destroy (this);

		if (PersistentData == null)
		{
			if (File.Exists (FilePath))
				LoadData ();
			else
				CreateData ();
		}

		ResetGameData ();

		DontDestroyOnLoad (gameObject);
	}

	private void CreateData ()
	{
		PersistentData = new ();
		SaveData ();
	}

	private void LoadData ()
	{
		using StreamReader sr = new (FilePath);
		
		PersistentData = JsonUtility.FromJson<PersistentData> (sr.ReadToEnd ());

		PersistentData ??= new ();
	}

	private void SaveData ()
	{
		using StreamWriter sw = new (FilePath);
		
		string data = JsonUtility.ToJson (PersistentData, true);
		
		sw.Write (data);
	}
}
