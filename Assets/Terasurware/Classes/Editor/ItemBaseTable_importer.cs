using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ItemBaseTable_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/Data/ItemBaseTable.xls";
	private static readonly string exportPath = "Assets/Resources/Data/ItemBaseTable.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			TempItemBase data = (TempItemBase)AssetDatabase.LoadAssetAtPath (exportPath, typeof(TempItemBase));
			if (data == null) {
				data = ScriptableObject.CreateInstance<TempItemBase> ();
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

					TempItemBase.Sheet s = new TempItemBase.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						TempItemBase.Param p = new TempItemBase.Param ();
						
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.Name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.Type = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.ActionName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.Material = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.Value = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.Price = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.Quality = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.Num = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.Description = (cell == null ? "" : cell.StringCellValue);
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
