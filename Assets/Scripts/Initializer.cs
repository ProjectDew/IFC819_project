using UnityEngine;

public class Initializer : MonoBehaviour
{
	private const int MIN_FRAME_RATE = 30;
	private const int MAX_FRAME_RATE = 60;

	[SerializeField]
	private int targetFrameRate;

	private void Awake ()
	{
		Application.targetFrameRate = Mathf.Clamp (targetFrameRate, MIN_FRAME_RATE, MAX_FRAME_RATE);
	}
}
