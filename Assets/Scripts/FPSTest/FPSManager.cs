using UnityEngine;

public class FPSManager : MonoBehaviour
{
	[SerializeField]
	private ManagedBehaviour[] managedBehaviours;

	private void Awake ()
	{
		for (int i = 0; i < managedBehaviours.Length; i++)
			managedBehaviours[i].ManagedAwake ();
		
		DataManager.Instance.MoveSpeed = 2;
	}

	private void Start ()
	{
		for (int i = 0; i < managedBehaviours.Length; i++)
			managedBehaviours[i].ManagedStart ();
	}

	private void Update ()
	{
		for (int i = 0; i < managedBehaviours.Length; i++)
			managedBehaviours[i].ManagedUpdate ();
	}
}
