using UnityEngine;
using UnityEngine.UIElements;

public class UISensorSupport : MonoBehaviour
{
	[SerializeField]
	private UIDocument sensorSupportMenu;

	private VisualElement sensorSupportRoot;

	private void Awake ()
	{
		sensorSupportRoot = sensorSupportMenu.rootVisualElement;

		sensorSupportRoot.Q<Button> ("CloseButton").clicked += CloseSensorMenu;

		sensorSupportRoot.style.display = DisplayStyle.None;
	}

	private void CloseSensorMenu ()
	{
		sensorSupportRoot.style.display = DisplayStyle.None;
	}
}
