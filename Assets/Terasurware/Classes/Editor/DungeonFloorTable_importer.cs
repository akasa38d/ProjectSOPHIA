using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class DungeonFloorTable_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/Data/DungeonFloorTable.xls";
	private static readonly string exportPath = "Assets/Resources/Data/DungeonFloorTable.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			TempDungeonFloor data = (TempDungeonFloor)AssetDatabase.LoadAssetAtPath (exportPath, typeof(TempDungeonFloor));
			if (data == null) {
				data = ScriptableObject.CreateInstance<TempDungeonFloor> ();
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

					TempDungeonFloor.Sheet s = new TempDungeonFloor.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						TempDungeonFloor.Param p = new TempDungeonFloor.Param ();
						
					cell = row.GetCell(0); p.Floor = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.Type = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.RoomCountX = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.RoomCountY = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.MaxRoomCellX = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.MaxRoomCellY = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.MinRoomCellX = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.MinRoomCellY = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.MaxEnemyNum = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.MinEnemyNum = (int)(cell == null ? 0 : cell.NumericCellValue);
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
