public class Direction
{
	public readonly static Direction Up = new Direction(Name = "Up", Id = 0, XDirection = 0, YDirection = 1, ZDirection = 0);
	public readonly static Direction Down = new Direction(){Name = "Down", Id = 1, XDirection = 0, YDirection = -1, ZDirection = 0};
	public readonly static Direction North = new Direction(){Name = "North", Id = 2, XDirection = 0, YDirection = 0, ZDirection = 1};
	public readonly static Direction South = new Direction(){Name = "South", Id = 3, XDirection = 0, YDirection = 0, ZDirection = -1};
	public readonly static Direction Left = new Direction(){Name = "Left", Id = 4, XDirection = -1, YDirection = 0, ZDirection = 0};
	public readonly static Direction Right = new Direction(){Name = "Right", Id = 5, XDirection = 1, YDirection = 0, ZDirection = 0};
	
	public Direction();

	public Direction(string Name, int XDirection, int YDirection, int ZDirection)
	{
		this.Name = Name;
		this.XDirection = XDirection;
		this.YDirection = YDirection;
		this.ZDirection = ZDirection;
		if (XDirection != 0) {
			this.Vertices = new int[][]
			{
				{XDirection, 1, 1},
				{XDirection, 1, -1},
				{XDirection, -1, 1},
				{XDirection, -1, -1}
			}
		}
		if (YDirection != 0) {
			this.Vertices = new int[][]
			{
				{1, YDirection, 1},
				{1, YDirection, -1},
				{-1, YDirection, 1},
				{-1, YDirection, -1}
			}
		}
		if (ZDirection != 0) {
			this.Vertices = new int[][]
			{
				{1, 1,  ZDirection},
				{1, -1, ZDirection},
				{-1, 1, ZDirection},
				{-1, -1,ZDirection}
			}
		}
	}

	public string Name {get; set;}

	public int Id { get; set; }
	
	public int XDirection { get; set; }
	
	public int YDirection { get; set; }
	
	public int ZDirection { get; set; }

	public int[][] Vertices { get; set; }
}
