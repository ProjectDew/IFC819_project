using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
	private const int TOTAL_ROBOTS = 16;

	[SerializeField]
	private SensorDetector sensorDetector;

	[SerializeField]
	private TriviaData[] triviaData;

	[SerializeField]
	private UIDocument mainMenu;

	[SerializeField]
	private UIDocument categoryMenu;
	
	[SerializeField]
	private UIDocument creditsMenu;

	private VisualElement mainMenuRoot;

	private void Awake ()
	{
		mainMenuRoot = mainMenu.rootVisualElement;

		Button triviaButton = mainMenuRoot.Q<Button> ("TriviaButton");
		Button vrDemoButton = mainMenuRoot.Q<Button> ("VRDemoButton");
		Button fpsTestButton = mainMenuRoot.Q<Button> ("FPSTestButton");
		Button sensorListButton = mainMenuRoot.Q<Button> ("SensorListButton");
		Button creditsButton = mainMenuRoot.Q<Button> ("CreditsButton");
		Button quitAppButton = mainMenuRoot.Q<Button> ("QuitAppButton");

		triviaButton.clicked += OpenCategoryMenu;
		vrDemoButton.clicked += LoadVRDemo;
		fpsTestButton.clicked += OpenFPSTest;
		sensorListButton.clicked += OpenSensorList;
		creditsButton.clicked += ShowCredits;
		quitAppButton.clicked += QuitApp;
	}

	private void Start ()
	{
		Label robotsFoundLabel = mainMenuRoot.Q<Label> ("RobotsFoundLabel");
		Label rightAnswersLabel = mainMenuRoot.Q<Label> ("RightAnswersLabel");
		
		int totalQuestions = 0;
		int totalRightAnswers = 0;

		for (int i = 0; i < triviaData.Length; i++)
		{
			totalQuestions += triviaData[i].TotalQuestions;
			totalRightAnswers += DataManager.Instance.PersistentData.GetTotalRightAnswers (triviaData[i]);
		}

		robotsFoundLabel.text = $"{DataManager.Instance.PersistentData.TotalRobotsFound}/{TOTAL_ROBOTS}";
		rightAnswersLabel.text = $"{totalRightAnswers}/{totalQuestions}";
	}

	private void OpenCategoryMenu ()
	{
		categoryMenu.rootVisualElement.style.display = DisplayStyle.Flex;
	}

	private void LoadVRDemo ()
	{
		SceneManager.LoadScene ("VR Demo");
	}

	private void OpenFPSTest ()
	{
		SceneManager.LoadScene ("FPS Test");
	}

	private void OpenSensorList ()
	{
		sensorDetector.Open ();
	}

	private void ShowCredits ()
	{
		creditsMenu.rootVisualElement.style.display = DisplayStyle.Flex;
	}

	private void QuitApp ()
	{
		Application.Quit ();
	}
}
