using UnityEngine;
using UnityEngine.UIElements;

public class UISensorWarning : MonoBehaviour
{
	[SerializeField]
	private UIDocument sensorWarningMenu;

	[SerializeField]
	private UIPopUpMenu tutorialVRSwitchMenu;

	[SerializeField, TextArea]
	private string sensorWarning;

	private VisualElement sensorWarningRoot;

	public void ShowWarningMessage ()
	{
		sensorWarningRoot.style.display = DisplayStyle.Flex;
	}

	public void HideWarningMessage ()
	{
		sensorWarningRoot.style.display = DisplayStyle.None;

		if (tutorialVRSwitchMenu != null)
			tutorialVRSwitchMenu.ShowMenu ();
	}

	private void Awake ()
	{
		sensorWarningRoot = sensorWarningMenu.rootVisualElement;

		sensorWarningRoot.Q<Label> ("WarningText").text = sensorWarning;

		sensorWarningRoot.Q<Button> ("UnderstoodButton").clicked += HideWarningMessage;
	}
}
