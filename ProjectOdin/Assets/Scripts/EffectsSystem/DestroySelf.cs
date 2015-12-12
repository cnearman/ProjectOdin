using UnityEngine;

public class DestroySelf : BaseClass
{
	public IDestructible Self { get; set; }
	
	public void ApplyEffect(GameObject target)
	{
		if (target is IDestructible)
		{
			target.Destroy();
		}
	}
}