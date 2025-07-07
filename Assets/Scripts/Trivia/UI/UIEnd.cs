using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIEnd : ManagedBehaviour
{
	[SerializeField]
	private UIDocument endMenu;

	private VisualElement endRoot;

	public void Show ()
	{
		Label scoreLabel = endRoot.Q<Label> ("ScoreLabel");

		scoreLabel.text = DataManager.Instance.Score.ToString ();

		endRoot.style.display = DisplayStyle.Flex;
	}

	public override void ManagedAwake ()
	{
		endRoot = endMenu.rootVisualElement;

		Button playAgain = endRoot.Q<Button> ("PlayAgainButton");
		Button returnToMenu = endRoot.Q<Button> ("ReturnToMenuButton");

		playAgain.clicked += PlayAgain;
		returnToMenu.clicked += ReturnToMenu;

		endRoot.style.display = DisplayStyle.None;
	}

	private void PlayAgain ()
	{
		SceneManager.LoadScene ("Trivia");
	}

	private void ReturnToMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}
}
