public class Direction
{
	public readonly static Direction Up = new Direction("Up", 0, 1, 0, 0);
	public readonly static Direction Down = new Direction("Down", 0, -1, 0, 1);
	public readonly static Direction North = new Direction("North", 0, 0, 1, 2);
	public readonly static Direction South = new Direction("South", 0, 0, -1, 3);
	public readonly static Direction Left = new Direction("Left", -1, 0, 0, 4);
	public readonly static Direction Right = new Direction("Right", 1, 0, 0, 5);
	
	

	private Direction(string Name, int XDirection, int YDirection, int ZDirection, int Id)
	{
        this.Id = Id;

		this.Name = Name;
		this.XDirection = XDirection;
		this.YDirection = YDirection;
		this.ZDirection = ZDirection;
		if (XDirection == 1)
		{
			this.Vertices = new int[,]
            {
                {-1, -1, 1},
                {-1, 1, 1},
                {-1, 1, -1},
                {-1, -1, -1}
            };
		} 
		else if (XDirection == -1)
		{
			this.Vertices = new int[,]
			{
                {1, -1, -1},
                {1, 1, -1},
                {1, 1, 1},
                {1, -1, 1}
            };
		}
		if (YDirection == 1) {
			this.Vertices = new int[,]
			{
				{-1, 1, 1},
                {1, 1, 1},
                {1, 1, -1},
                {-1, 1, -1}
            };
		} else if (YDirection == -1) 
		{
			this.Vertices = new int[,]
			{
				{-1, -1, -1},
				{1, -1, -1},
				{1, -1, 1},
				{-1, -1, 1}
			};
		}
		if (ZDirection == 1) {
			this.Vertices = new int[,]
            {
                {1, -1, 1},
                {1, 1, 1},
                {-1, 1, 1},
                {-1, -1,1}
            };
		} 
		else if (ZDirection == -1) 
		{
			this.Vertices = new int[,]
			{
				{-1, -1,  -1},
				{-1, 1, -1},
				{1, 1, -1},
				{1, -1,-1}
			};
		}
	}
	
	public string Name {get; set;}
	
	public int Id { get; set; }
	
	public int XDirection { get; set; }
	
	public int YDirection { get; set; }
	
	public int ZDirection { get; set; }

	public int[,] Vertices { get; set; }
}
