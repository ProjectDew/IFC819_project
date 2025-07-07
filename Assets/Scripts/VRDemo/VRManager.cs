using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;

public class VRManager : MonoBehaviour
{
	[SerializeField]
	private UISensorWarning sensorWarningMenu;

	[SerializeField]
	private UIPopUpMenu tutorialVRSwitchMenu;

	[SerializeField]
	private UIExitDemo exitDemoMenu;

	private XRManagerSettings managerSettings;

	private Coroutine initializerCoroutine;

	private int tapCountOnSwitchVRMode;

	private void Awake ()
	{
		XRGeneralSettings xrSettings = XRGeneralSettings.Instance;

		if (xrSettings != null)
			managerSettings = xrSettings.Manager;

		tapCountOnSwitchVRMode = 0;

		if (Touchscreen.current != null)
			InputSystem.EnableDevice (Touchscreen.current);
	}

	private void Start ()
	{
		if (AttitudeSensor.current != null)
			sensorWarningMenu.HideWarningMessage ();
		else
			sensorWarningMenu.ShowWarningMessage ();
	}

	private void OnEnable ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	private void Update ()
	{
		SwitchVRMode ();
	}

	private void OnDisable ()
	{
		Screen.sleepTimeout = SleepTimeout.SystemSetting;
		StopVR ();
	}

	private void SwitchVRMode ()
	{
		if (Touchscreen.current == null)
			return;

		int tapCount = Touchscreen.current.primaryTouch.tapCount.ReadValue ();

		if (tapCount == 0)
			tapCountOnSwitchVRMode = 0;

		if (tapCount == 0 || tapCount == tapCountOnSwitchVRMode || tapCount % 2 == 1)
			return;

		if (tutorialVRSwitchMenu != null)
			tutorialVRSwitchMenu.HideMenu ();

		if (initializerCoroutine == null)
		{
			exitDemoMenu.HideMenu ();
			StartVR ();
		}
		else
		{
			exitDemoMenu.ShowMenu ();
			StopVR ();
		}

		tapCountOnSwitchVRMode = tapCount;
	}

	private void StartVR ()
	{
		if (initializerCoroutine != null)
			return;

		initializerCoroutine = StartCoroutine (InitializeLoader ());
	}

	private void StopVR ()
	{
		if (initializerCoroutine != null)
			StopCoroutine (initializerCoroutine);

		initializerCoroutine = null;

		if (managerSettings == null || managerSettings.activeLoader == null)
			return;

		managerSettings.StopSubsystems ();
		managerSettings.DeinitializeLoader ();
	}

	private IEnumerator InitializeLoader ()
	{
		if (managerSettings == null)
			yield return null;

		yield return managerSettings.InitializeLoader ();

		if (managerSettings.activeLoader != null)
			managerSettings.StartSubsystems ();
	}
}
