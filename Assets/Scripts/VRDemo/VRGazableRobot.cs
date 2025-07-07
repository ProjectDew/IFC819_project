using UnityEngine;
using UnityEngine.UIElements;

public class VRGazableRobot : MonoBehaviour
{
	[SerializeField]
	private HideAreaData[] hideAreaData;
	
	[SerializeField]
	private MeshRenderer gazePointerRenderer;

	[SerializeField]
	private UIPopUpMenu congratsMenu;

	[SerializeField]
	private UIDocument vrMenu;

	[SerializeField]
	private Vector3 feedbackStartPosition;

	[SerializeField]
	private Vector3 feedbackEndPosition;

	[SerializeField]
	private Color feedbackColor;

	private State currentState;

	private Transform tr;
	private Animator animator;
	
	private VisualElement findRobotsContainer;
	private VisualElement feedbackContainer;

	private ProgressBar timeGazing;
	
	private Label robotsFoundLabel;

	private int robotsFound;

	private int idNumber;
	
	private enum State
	{
		Idle,
		Gazing
	}

	public void OnPointerEnter ()
	{
		timeGazing.value = 0;

		HideFindRobotsContainer ();
		PrepareFeedback ();

		timeGazing.style.display = DisplayStyle.Flex;

		currentState = State.Gazing;
	}

	public void OnPointerExit ()
	{
		timeGazing.value = 0;

		timeGazing.style.display = DisplayStyle.None;

		currentState = State.Idle;
	}

	private void Awake ()
	{
		VisualElement vrRoot = vrMenu.rootVisualElement;

		tr = transform;
		animator = GetComponent<Animator> ();

		timeGazing = vrRoot.Q<ProgressBar> ("TimeGazing");
		
		findRobotsContainer = vrRoot.Q<VisualElement> ("FindRobotsContainer");
		feedbackContainer = vrRoot.Q<VisualElement> ("FeedbackContainer");

		robotsFoundLabel = vrRoot.Q<Label> ("RobotsFoundLabel");

		UpdatePosition ();

		feedbackContainer.style.display = DisplayStyle.None;

		timeGazing.style.display = DisplayStyle.None;

		currentState = State.Idle;
	}

	private void Start ()
	{
		timeGazing.highValue = DataManager.Instance.GameData.GazeTime;
	}

	private void Update ()
	{
		if (currentState == State.Gazing)
			Gaze ();
	}

	private void Gaze ()
	{
		timeGazing.value += Time.deltaTime;

		if (timeGazing.value < timeGazing.highValue)
			return;

		UpdateRobotsFound ();
		UpdatePosition ();

		ShowRobotFoundFeedback ();

		if (robotsFound >= DataManager.Instance.GameData.HiddenRobots)
		{
			gazePointerRenderer.enabled = false;
			congratsMenu.ShowMenu ();
		}

		timeGazing.style.display = DisplayStyle.None;

		currentState = State.Idle;
	}

	private void UpdateRobotsFound ()
	{
		int hiddenRobots = DataManager.Instance.GameData.HiddenRobots;

		robotsFound++;
		robotsFound = Mathf.Clamp (robotsFound, 0, hiddenRobots);

		DataManager.Instance.SaveRobotFound (idNumber);
		
		robotsFoundLabel.text = $"{robotsFound}/{DataManager.Instance.GameData.HiddenRobots}";
	}

	private void UpdatePosition ()
	{
		if (robotsFound < 0 || robotsFound >= hideAreaData.Length)
		{
			tr.position = new (1000, 1000, 1000);
			return;
		}

		RobotData robotData = hideAreaData[robotsFound].GetRandomData ();

		idNumber = robotData.IDNumber;

		tr.SetPositionAndRotation (robotData.Position, Quaternion.Euler (robotData.Rotation));
		tr.localScale = robotData.Scale;

		animator.SetTrigger (robotData.AnimationTriggerID);
	}

	private void HideFindRobotsContainer () => findRobotsContainer.style.display = DisplayStyle.None;

	private void PrepareFeedback ()
	{
		Color opaqueColor = new (feedbackColor.r, feedbackColor.g, feedbackColor.b, 1);

		feedbackContainer.style.display = DisplayStyle.None;
		
		feedbackContainer.style.backgroundColor = new (opaqueColor);

		feedbackContainer.transform.position = feedbackStartPosition;
	}

	private void ShowRobotFoundFeedback ()
	{
		Color transparentColor = new (feedbackColor.r, feedbackColor.g, feedbackColor.b, 0);

		feedbackContainer.style.display = DisplayStyle.Flex;

		feedbackContainer.style.backgroundColor = new (transparentColor);
		
		feedbackContainer.transform.position = feedbackEndPosition;
	}
}
