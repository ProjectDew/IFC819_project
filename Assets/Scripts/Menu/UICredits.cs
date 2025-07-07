using UnityEngine;
using UnityEngine.UIElements;

public class UICredits : MonoBehaviour
{
	[SerializeField]
	private UIDocument creditsMenu;

	private void Awake ()
	{
		VisualElement creditsRoot = creditsMenu.rootVisualElement;
		creditsRoot.Q<Button> ("CloseCreditsButton").clicked += CloseCredits;

		creditsRoot.style.display = DisplayStyle.None;
	}

	private void CloseCredits ()
	{
		creditsMenu.rootVisualElement.style.display = DisplayStyle.None;
	}
}
