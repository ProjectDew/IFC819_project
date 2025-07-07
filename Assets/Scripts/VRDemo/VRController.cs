using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Google.XR.Cardboard;

public class VRController : MonoBehaviour
{
	[SerializeField]
	private float rotationXSpeed;
	
	[SerializeField]
	private float rotationYSpeed;
	
	[SerializeField]
	private float rotationZSpeed;

	[SerializeField]
	private float horizontalRotationAngleOffset;

	[SerializeField]
	private float verticalRotationAngleOffset;

	private Transform tr;

	private void Awake ()
	{
		tr = transform;
	}

	private void OnEnable ()
	{
		if (AttitudeSensor.current != null)
			InputSystem.EnableDevice (AttitudeSensor.current);
	}

	private void Update ()
	{
		LookAround ();
	}

	private void OnDisable ()
	{
		if (AttitudeSensor.current != null)
			InputSystem.DisableDevice (AttitudeSensor.current);
	}

	private void LookAround ()
	{
		/*if (Gyroscope.current != null)
		{
			if (!Gyroscope.current.enabled)
				InputSystem.EnableDevice (Gyroscope.current);

			return;
		}*/

		Quaternion attitude = (AttitudeSensor.current != null) ? AttitudeSensor.current.attitude.ReadValue () : Quaternion.identity;
		Quaternion correctedAttitude = new (attitude.x, attitude.y, -attitude.z, -attitude.w);

		Quaternion horizontalRotationCorrection = Quaternion.AngleAxis (horizontalRotationAngleOffset, Vector3.up);
		Quaternion verticalRotationCorrection = Quaternion.AngleAxis (verticalRotationAngleOffset, Vector3.left);

		tr.rotation = horizontalRotationCorrection * verticalRotationCorrection * correctedAttitude;

		if (Api.IsCloseButtonPressed)
			SceneManager.LoadScene ("Menu");
	}
}
