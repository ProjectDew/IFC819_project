using UnityEngine;

public class MovementBonus : ManagedBehaviour
{
	[SerializeField]
	private GameObject bonusPrefab;

	[SerializeField]
	private float distanceBetweenObjects;

	[SerializeField]
	private float spawnPositionY;

	[SerializeField]
	private float offsetPositionZ;

	[SerializeField]
	private float minPosition;

	[SerializeField]
	private float speedMultiplier;

	[SerializeField]
	private int totalObjects;

	private enum State
	{
		Waiting,
		Looping
	}

	private State currentState;

	private Transform[] bonusTransforms;
	private Transform lastBonus;

	private Transform tr;

	private Vector3 initialPosition;

	public void StartLoop ()
	{
		currentState = State.Looping;
	}

	public void StopLoop ()
	{
		if (currentState == State.Waiting)
			return;

		float offsetPositionX = 0;
		
		tr.position = initialPosition;

		for (int i = 0; i < bonusTransforms.Length; i++)
		{
			bonusTransforms[i].position = new Vector3 (tr.position.x + offsetPositionX, spawnPositionY, offsetPositionZ * Random.Range (-1, 2));
			offsetPositionX += distanceBetweenObjects;
		}

		lastBonus = bonusTransforms[^1];

		currentState = State.Waiting;
	}

	public override void ManagedAwake ()
	{
		Vector3 offsetPosition = new (0, spawnPositionY, offsetPositionZ);
		
		tr = transform;

		initialPosition = tr.position;

		bonusTransforms = new Transform[totalObjects];

		for (int i = 0; i < bonusTransforms.Length; i++)
		{
			GameObject bonusObj = Instantiate (bonusPrefab, tr.position + offsetPosition, Quaternion.identity);

			bonusTransforms[i] = bonusObj.transform;
			bonusTransforms[i].SetParent (tr);

			offsetPosition.x += distanceBetweenObjects;
			offsetPosition.z = offsetPositionZ * Random.Range (-1, 2);
		}

		lastBonus = bonusTransforms[^1];
		
		currentState = State.Waiting;
	}

	public override void ManagedUpdate ()
	{
		if (currentState != State.Looping)
			return;
		
		float movement = DataManager.Instance.MoveSpeed * speedMultiplier * Time.deltaTime;

		tr.position += movement * Vector3.left;

		for (int i = 0; i < bonusTransforms.Length; i++)
		{
			if (bonusTransforms[i].position.x > minPosition)
				continue;

			float positionX = lastBonus.position.x + distanceBetweenObjects;
			float positionY = bonusTransforms[i].position.y;
			float positionZ = offsetPositionZ * Random.Range (-1, 2);
			
			bonusTransforms[i].position = new Vector3 (positionX, positionY, positionZ);

			lastBonus = bonusTransforms[i];
		}
	}
}
