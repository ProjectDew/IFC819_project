using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
	[SerializeField]
	private UIDocument pauseMenu;

	private void Awake ()
	{
		VisualElement pauseRoot = pauseMenu.rootVisualElement;

		pauseRoot.Q<Button> ("ContinueButton").clicked += Continue;
		pauseRoot.Q<Button> ("RestartButton").clicked += Restart;
		pauseRoot.Q<Button> ("LoadMenuButton").clicked += LoadMenu;
		
		pauseRoot.style.display = DisplayStyle.None;
	}

	private void Continue ()
	{
		pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
		DataManager.Instance.SetTriviaIngame ();
	}

	private void Restart ()
	{
		SceneManager.LoadScene ("Trivia");
	}

	private void LoadMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}
}
