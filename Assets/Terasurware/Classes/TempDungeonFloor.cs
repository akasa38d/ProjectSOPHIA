using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TempDungeonFloor : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int Floor;
		public string Type;
		public int RoomCountX;
		public int RoomCountY;
		public int MaxRoomCellX;
		public int MaxRoomCellY;
		public int MinRoomCellX;
		public int MinRoomCellY;
		public int MaxEnemyNum;
		public int MinEnemyNum;
	}
}

