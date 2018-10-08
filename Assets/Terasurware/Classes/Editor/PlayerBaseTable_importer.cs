using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class PlayerBaseTable_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/Data/PlayerBaseTable.xls";
	private static readonly string exportPath = "Assets/Resources/Data/PlayerBaseTable.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			TempPlayerBase data = (TempPlayerBase)AssetDatabase.LoadAssetAtPath (exportPath, typeof(TempPlayerBase));
			if (data == null) {
				data = ScriptableObject.CreateInstance<TempPlayerBase> ();
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

					TempPlayerBase.Sheet s = new TempPlayerBase.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						TempPlayerBase.Param p = new TempPlayerBase.Param ();
						
					cell = row.GetCell(0); p.LV = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.MaxHP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.Atk = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.Def = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.NextExp = (int)(cell == null ? 0 : cell.NumericCellValue);
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
