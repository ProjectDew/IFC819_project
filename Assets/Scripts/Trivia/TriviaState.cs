using UnityEngine;

[CreateAssetMenu(fileName = "TriviaStates", menuName = "Scriptable Objects/TriviaStates")]
public class TriviaState : ScriptableObject
{
    [SerializeField]
	private string id;

	public string ID => id;
}
