using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIFPS : MonoBehaviour
{
	[SerializeField]
	private UIDocument fpsMenu;

	private VisualElement fpsRoot;

	private SliderInt fpsBar;
	private Label fpsLabel;

	private void Awake ()
	{
		fpsRoot = fpsMenu.rootVisualElement;
		
		fpsBar = fpsRoot.Q<SliderInt> ("FPSBar");
		fpsLabel = fpsRoot.Q<Label> ("FPSLabel");


		fpsRoot.Q<Button> ("ReturnToMenuButton").clicked += ReturnToMenu;
	}

	private void Update ()
	{
		Application.targetFrameRate = fpsBar.value;
		fpsLabel.text = $"{fpsBar.value} fps";
	}

	private void ReturnToMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}
}
