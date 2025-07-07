using UnityEngine;

public abstract class ManagedBehaviour : MonoBehaviour
{
	public virtual void ManagedAwake () { }
	
	public virtual void ManagedOnEnable () { }
	
	public virtual void ManagedStart () { }
	
	public virtual void ManagedUpdate () { }
	
	public virtual void ManagedFixedUpdate () { }
	
	public virtual void ManagedLateUpdate () { }
	
	public virtual void ManagedOnDisable () { }
}