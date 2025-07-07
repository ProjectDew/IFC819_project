using UnityEngine;
using UnityEngine.UIElements;

public class UIIngame : ManagedBehaviour
{
	[SerializeField]
	private UIDocument ingameMenu;
	
	[SerializeField]
	private UIDocument pauseMenu;
	
	private Label scoreLabel;
	private ProgressBar timerBar;

	public void UpdateTimer (float time)
	{
		timerBar.value = time;
	}

	private void DisplayScore (int score)
	{
		scoreLabel.text = score.ToString ();
	}

	public override void ManagedAwake ()
	{
		VisualElement ingameRoot = ingameMenu.rootVisualElement;

		Button pauseButton = ingameRoot.Q<Button> ("PauseButton");
		
		scoreLabel = ingameRoot.Q<Label> ("ScoreLabel");
		timerBar = ingameRoot.Q<ProgressBar> ("TimeLeft");

		timerBar.value = 0;
		timerBar.highValue = DataManager.Instance.GameData.TimeToAnswer;
		
		scoreLabel.text = "0";

		DataManager.Instance.OnUpdateScore += DisplayScore;
		
		pauseButton.clicked += PauseGame;
	}

	private void PauseGame ()
	{
		pauseMenu.rootVisualElement.style.display = DisplayStyle.Flex;

		DataManager.Instance.SaveRecords ();

		DataManager.Instance.SetTriviaPause ();
	}
}
