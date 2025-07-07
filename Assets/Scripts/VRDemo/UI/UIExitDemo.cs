using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIExitDemo : MonoBehaviour
{
	[SerializeField]
	private UIDocument exitDemoMenu;

	public void ShowMenu ()
	{
		exitDemoMenu.rootVisualElement.style.display = DisplayStyle.Flex;
	}

	public void HideMenu ()
	{
		exitDemoMenu.rootVisualElement.style.display = DisplayStyle.None;
	}

	private void Awake ()
	{
		exitDemoMenu.rootVisualElement.Q<Button> ("BackToMenuButton").clicked += GoBackToMenu;
	}

	private void GoBackToMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}
}
