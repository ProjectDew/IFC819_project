using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersistentData
{
	[SerializeField]
	private List<RecordsData> triviaRecords;

	[SerializeField]
	private List<int> robotsFound;
	
	[SerializeField]
	private bool tutorialCompleted;

	public bool TutorialCompleted
	{
		get => tutorialCompleted;
		set => tutorialCompleted = value;
	}

	public int TotalRobotsFound => (robotsFound != null) ? robotsFound.Count : 0;

	public void SetRobotFound (int idNumber)
	{
		if (robotsFound == null || robotsFound.Count == 0)
			robotsFound = new () { idNumber };

		if (!robotsFound.Contains (idNumber))
			robotsFound.Add (idNumber);
	}

	public int GetHighestScore (TriviaData triviaData)
	{
		CreateRecordsDataIfNonExistent (triviaData);

		for (int i = 0; i < triviaRecords.Count; i++)
			if (triviaData.ID == triviaRecords[i].TriviaData.ID)
				return triviaRecords[i].HighestScore;

		return 0;
	}

	public void SetHighestScore (TriviaData triviaData, int highestScore)
	{
		CreateRecordsDataIfNonExistent (triviaData);

		for (int i = 0; i < triviaRecords.Count; i++)
		{
			if (triviaData.ID != triviaRecords[i].TriviaData.ID)
				continue;

			if (highestScore > triviaRecords[i].HighestScore)
				triviaRecords[i].HighestScore = highestScore;

			return;
		}
	}

	public int GetTotalRightAnswers (TriviaData triviaData)
	{
		CreateRecordsDataIfNonExistent (triviaData);

		for (int i = 0; i < triviaRecords.Count; i++)
			if (triviaData.ID == triviaRecords[i].TriviaData.ID)
				return triviaRecords[i].RightAnswers;

		return 0;
	}

	public void SetTotalRightAnswers (TriviaData triviaData, int totalRightAnswers)
	{
		CreateRecordsDataIfNonExistent (triviaData);

		for (int i = 0; i < triviaRecords.Count; i++)
		{
			if (triviaData.ID != triviaRecords[i].TriviaData.ID)
				continue;

			if (totalRightAnswers > triviaRecords[i].RightAnswers)
				triviaRecords[i].RightAnswers = totalRightAnswers;

			return;
		}
	}

	private void CreateRecordsDataIfNonExistent (TriviaData triviaData)
	{
		if (triviaData == null)
			throw new ArgumentNullException ("The parameter of type TriviaData is null.");

		if (triviaRecords == null || triviaRecords.Count == 0)
		{
			triviaRecords = new () { new () { TriviaData = triviaData } };
			return;
		}

		for (int i = 0; i < triviaRecords.Count; i++)
			Debug.Log (triviaRecords[i].TriviaData);

		for (int i = 0; i < triviaRecords.Count; i++)
			if (triviaRecords[i].TriviaData != null && triviaData.ID == triviaRecords[i].TriviaData.ID)
				return;

		triviaRecords.Add (new () { TriviaData = triviaData });
	}

	[Serializable]
	public class RecordsData
	{
		[SerializeField]
		private TriviaData triviaData;
		
		[SerializeField]
		private int highestScore;
		
		[SerializeField]
		private int rightAnswers;

		/*public RecordsData (TriviaData triviaData)
		{
			this.triviaData = triviaData;

			highestScore = 0;
			rightAnswers = 0;
		}*/

		public TriviaData TriviaData
		{
			get => triviaData;
			set { triviaData = value; Debug.Log (triviaData); }
		}

		public int HighestScore
		{
			get => highestScore;
			set => highestScore = value;
		}

		public int RightAnswers
		{
			get => rightAnswers;
			set => rightAnswers = value;
		}
	}
}
