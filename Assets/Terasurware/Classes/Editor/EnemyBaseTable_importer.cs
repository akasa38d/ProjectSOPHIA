using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class EnemyBaseTable_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/Data/EnemyBaseTable.xls";
	private static readonly string exportPath = "Assets/Resources/Data/EnemyBaseTable.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			TempEnemyBase data = (TempEnemyBase)AssetDatabase.LoadAssetAtPath (exportPath, typeof(TempEnemyBase));
			if (data == null) {
				data = ScriptableObject.CreateInstance<TempEnemyBase> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					TempEnemyBase.Sheet s = new TempEnemyBase.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						TempEnemyBase.Param p = new TempEnemyBase.Param ();
						
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.Name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.MaxHP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.MaxSP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.Atk = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.Def = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.Spd = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.Exp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.NextExp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.MoneyDrop = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.Money = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.NormalDrop = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.NormalItem = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.RareDrop = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(14); p.RareItem = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(15); p.ActType = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(16); p.RankType = (cell == null ? "" : cell.StringCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
