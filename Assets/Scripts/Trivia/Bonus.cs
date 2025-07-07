using UnityEngine;

public class Bonus : MonoBehaviour
{
	private const float PICKED_UP_POSITION = -100;

	[SerializeField]
	private int bonusScore;

	private Transform tr;

	private void Awake ()
	{
		tr = transform;
	}

	private void OnTriggerEnter (Collider col)
	{
		int layer = col.gameObject.layer;

		if (layer != LayerMask.NameToLayer ("Character"))
			return;

		DataManager.Instance.Score += bonusScore * (int)DataManager.Instance.MoveSpeed;

		tr.position = new (PICKED_UP_POSITION, tr.position.y, tr.position.z);
	}
}
