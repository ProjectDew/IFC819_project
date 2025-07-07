using UnityEngine;

[CreateAssetMenu(fileName = "HideAreaData", menuName = "Scriptable Objects/HideAreaData")]
public class HideAreaData : ScriptableObject
{
    [SerializeField]
	private RobotData[] robotData;

	public RobotData GetRandomData ()
	{
		int dataIndex = Random.Range (0, robotData.Length);
		return robotData[dataIndex];
	}
}
