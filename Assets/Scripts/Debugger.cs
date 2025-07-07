using UnityEngine;
using UnityEngine.UIElements;

public class Debugger : MonoBehaviour
{
    [SerializeField]
	private UIDocument debugMenu;

	private static Label debugText;

	private void Awake ()
	{
		UIDocument debugMenu = GetComponent<UIDocument> ();
		VisualElement ingameRoot = debugMenu.rootVisualElement;
		
		debugText = ingameRoot.Q<Label> ("DebugText");
	}

	public static void Log (string message)
	{
		if (debugText == null)
			return;

		debugText.text = message;
	}

	public static void LogConcat (string message)
	{
		if (debugText == null)
			return;

		debugText.text += message;
	}
}
