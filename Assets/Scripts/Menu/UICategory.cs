using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UICategory : MonoBehaviour
{
	[SerializeField]
	private UIDocument categoryMenu;

	[SerializeField]
	private TriviaData[] triviaData;

	private Dictionary<string, Action> triviaDelegates;

	private void Awake ()
	{
		VisualElement categoryRoot = categoryMenu.rootVisualElement;

		InitializeTriviaDictionary ();
		
		for (int i = 0; i < triviaData.Length; i++)
		{
			string triviaDataID = triviaData[i].ID;

			int highestScore = DataManager.Instance.PersistentData.GetHighestScore (triviaData[i]);
			int totalRightAnswers = DataManager.Instance.PersistentData.GetTotalRightAnswers (triviaData[i]);

			int totalQuestions = triviaData[i].TotalQuestions;

			string triviaButtonName = $"TriviaData{i}Button";

			Button triviaButton = categoryRoot.Q<Button> (triviaButtonName);

			triviaButton.text = triviaData[i].ID;
			triviaButton.clicked += triviaDelegates[triviaButtonName];

			categoryRoot.Q<Label> ($"HighestLabel_{i}").text = highestScore.ToString ();
			categoryRoot.Q<Label> ($"RightAnswersLabel_{i}").text = $"{totalRightAnswers}/{totalQuestions}";
		}

		categoryRoot.Q<Button> ("BackToMenuButton").clicked += BackToMenu;

		categoryMenu.rootVisualElement.style.display = DisplayStyle.None;
	}

	private void InitializeTriviaDictionary ()
	{
		triviaDelegates = new ()
		{
			{ "TriviaData0Button", LoadTriviaData0 },
			{ "TriviaData1Button", LoadTriviaData1 }
		};
	}

	private void LoadTriviaData0 ()
	{
		DataManager.Instance.TriviaData = triviaData[0];
		SceneManager.LoadScene ("Trivia");
	}

	private void LoadTriviaData1 ()
	{
		DataManager.Instance.TriviaData = triviaData[1];
		SceneManager.LoadScene ("Trivia");
	}

	private void BackToMenu ()
	{
		categoryMenu.rootVisualElement.style.display = DisplayStyle.None;
	}
}
