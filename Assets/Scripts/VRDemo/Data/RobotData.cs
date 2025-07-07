using UnityEngine;

[CreateAssetMenu(fileName = "RobotData", menuName = "Scriptable Objects/RobotData")]
public class RobotData : ScriptableObject
{
	[SerializeField]
	private int idNumber;

	[SerializeField]
	private Vector3 position;
	
	[SerializeField]
	private Vector3 rotation;
	
	[SerializeField]
	private Vector3 scale;

	[SerializeField]
	private string animationTriggerID;

	public int IDNumber => idNumber;

	public Vector3 Position => position;

	public Vector3 Rotation => rotation;

	public Vector3 Scale => scale;

	public string AnimationTriggerID => animationTriggerID;
}
