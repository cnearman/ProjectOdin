using UnityEngine;

public class LifeSpan : BaseClass
{
	private double TimePassed;
	
	public double Duration { get; set; }
	
	void Update()
	{	
		this.TimePassed += Time.deltaTime;
		
		if (TimePassed > Duration)
		{
			Destroy(gameObject);
		}
	}
} 