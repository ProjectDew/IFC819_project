using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorDetector : MonoBehaviour
{
	[SerializeField]
	private UIDocument sensorSupportMenu;

	private VisualElement sensorSupportRoot;

	public void Open ()
	{
		DetectSensor (Accelerometer.current, "Accelerometer");
		DetectSensor (Gyroscope.current, "Gyroscope");
		DetectSensor (GravitySensor.current, "GravitySensor");
		DetectSensor (AttitudeSensor.current, "AttitudeSensor");
		DetectSensor (LinearAccelerationSensor.current, "LinearAccelerationSensor");
		DetectSensor (MagneticFieldSensor.current, "MagneticFieldSensor");
		DetectSensor (LightSensor.current, "LightSensor");
		DetectSensor (PressureSensor.current, "PressureSensor");
		DetectSensor (ProximitySensor.current, "ProximitySensor");
		DetectSensor (HumiditySensor.current, "HumiditySensor");
		DetectSensor (AmbientTemperatureSensor.current, "AmbientTemperatureSensor");
		DetectSensor (StepCounter.current, "StepCounter");

		sensorSupportRoot.style.display = DisplayStyle.Flex;
	}

	private void Awake ()
	{
		sensorSupportRoot = sensorSupportMenu.rootVisualElement;

		Button closeButton = sensorSupportRoot.Q<Button> ("CloseButton");
		closeButton.clicked += Close;

		sensorSupportRoot.style.display = DisplayStyle.None;
	}

	private void DetectSensor (InputDevice sensor, string sensorName)
	{
		Label accelerometerLabel = sensorSupportRoot.Q<VisualElement> (sensorName).Q<Label> (className: "sensorSupported");
		accelerometerLabel.text = (sensor != null) ? "Sí" : "No";
	}

	private void Close ()
	{
		sensorSupportRoot.style.display = DisplayStyle.None;
	}
}
