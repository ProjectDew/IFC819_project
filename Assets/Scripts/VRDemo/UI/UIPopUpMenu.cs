using UnityEngine;
using UnityEngine.UIElements;

public class UIPopUpMenu : MonoBehaviour
{
	[SerializeField]
	private UIDocument popUpMenu;

	[SerializeField]
	private float timeOnScreen;

	private VisualElement popUpRoot;

	private State currentState;

	private float counter;

	private enum State
	{
		Hidden,
		Displayed
	}

	public void ShowMenu ()
	{
		popUpRoot.style.display = DisplayStyle.Flex;
		currentState = State.Displayed;
	}

	public void HideMenu ()
	{
		currentState = State.Hidden;
		
		popUpRoot.style.display = DisplayStyle.None;

		Destroy (this);
	}

	private void Awake ()
	{
		popUpRoot = popUpMenu.rootVisualElement;
		popUpRoot.style.display = DisplayStyle.None;

		currentState = State.Hidden;
	}

	private void Update ()
	{
		if (currentState != State.Displayed)
			return;

		counter += Time.deltaTime;

		if (counter < timeOnScreen)
			return;

		HideMenu ();
	}
}
