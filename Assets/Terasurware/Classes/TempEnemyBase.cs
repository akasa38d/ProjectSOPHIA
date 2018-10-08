using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TempEnemyBase : ScriptableObject
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
		
		public int ID;
		public string Name;
		public int MaxHP;
		public int MaxSP;
		public int Atk;
		public int Def;
		public int Spd;
		public int Exp;
		public int NextExp;
		public int MoneyDrop;
		public int Money;
		public int NormalDrop;
		public int NormalItem;
		public int RareDrop;
		public int RareItem;
		public string ActType;
		public string RankType;
	}
}

