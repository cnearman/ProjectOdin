using System;
using UnityEngine;

public class DestroySelf : BaseClass, Effect
{
    public MonoBehaviour Owner
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void ApplyEffect(GameObject target)
    {
        Destroy(gameObject);
    }

    /*
    // What was I thinking here? Target Destroys itself? 
    // If its an activation then perhaps this makes sense, but if its just an on collision and you want the projectile
    // to destroy itself, there's definitely a simpler implementation above.
	public IDestructible Self { get; set; }
	
	public void ApplyEffect(GameObject target)
	{
        IDestructible targetInterface = target.GetComponent(typeof(IDestructible)) as IDestructible;
		if (targetInterface != null)
		{
			targetInterface.Destroy();
		}
	}*/
}