using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TempPlayerBase : ScriptableObject
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
		
		public int LV;
		public int MaxHP;
		public int Atk;
		public int Def;
		public int NextExp;
	}
}

