using UnityEngine;
using UnityEngine.UIElements;

public class UICongrats : MonoBehaviour
{
	[SerializeField]
	private UIDocument congratsMenu;

	private VisualElement congratsRoot;

	public void Show ()
	{
		congratsRoot.style.display = DisplayStyle.Flex;
	}

	public void Hide ()
	{
		congratsRoot.style.display = DisplayStyle.None;
	}

	private void Awake ()
	{
		congratsRoot = congratsMenu.rootVisualElement;
		Hide ();
	}
}
