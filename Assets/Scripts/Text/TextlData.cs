using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//メッセージの定義を行うクラス
public class TextData
{
    //通常の会話、複数人から選択して会話、選択肢のある会話
    public enum Type { Single, Multi, Choice };
    public Type TextType;

    public List<string> GroupName = new List<string>();
    public int GroupCount { get { return GroupName.Count(); } }

    public List<TextBox> TextBoxChain = new List<TextBox>();
    public List<GroupChain> GroupChains = new List<GroupChain>();

    public TextData(string textFile)
    {
        AnalyzeTextFile(textFile);
    }

    public void AnalyzeTextFile(string textFile)
    {
        //空白行で分割
        var text = textFile.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        var splitTexts = new List<string>(text);

        //タイプを判断
        if (Enum.TryParse(splitTexts[0], out TextType) == false) { Debug.Log("読み取り失敗"); }

        //TextBoxChainの作成（0番はタイプ判断で使用）
        for (int i = 1; i < splitTexts.Count; i++)
        {
            var textBox = new TextBox();

            var splitByLine = new List<string>(splitTexts[i].Split('\n'));

            //タグの列挙
            for (int j = 0; j < splitByLine.Count; j++)
            {
                analyzeBuTag(ref textBox, splitByLine[j]);

                if (splitByLine[j].Contains("@@Group"))
                {
                    var groupName = splitByLine[j].Replace("@@Group ", "");
                    GroupName.Add(groupName);
                    textBox.GroupName = GroupName.Last();
                }
            }

            TextBoxChain.Add(textBox);
        }

        if (TextType == Type.Multi) { groupByName(); }
    }

    void analyzeBuTag(ref TextBox textBox, string splitByLine)
    {
        if (splitByLine.Contains("@Choice"))
        {
            textBox.Choice = splitByLine.Replace("@Choice ", "");
        }

        if (splitByLine.Contains("@Name"))
        {
            textBox.Person = splitByLine.Replace("@Name ", "");
        }

        if (splitByLine.Contains("@Text"))
        {
            textBox.Message = splitByLine.Replace("@Text ", "");
        }

        if (splitByLine.Contains("@PImage"))
        {
            textBox.PlayerImageName = splitByLine.Replace("@PImage ", "");
        }

        if (splitByLine.Contains("@CImage"))
        {
            textBox.CharacterImageName = splitByLine.Replace("@CImage ", "");
        }

        if (splitByLine.Contains("@Music"))
        {
            textBox.MusicName = splitByLine.Replace("@Music ", "");
        }

        if (splitByLine.Contains("@Sound"))
        {
            textBox.SoundEffectName = splitByLine.Replace("@Sound ", "");
        }
    }

    void groupByName()
    {
        //グループ名によって分別
        var people = TextBoxChain.GroupBy(x => x.GroupName);

        foreach (var person in people)
        {
            var testChain = new GroupChain(person.Key, person.ToList());
            GroupChains.Add(testChain);
        }
    }
}

//メッセージの最小単位
public class TextBox
{
    //グループ
    public string GroupName;

    //選択肢
    public string Choice;

    //メッセージボックス・名前
    public string Person;

    //メッセージボックス・テキスト
    public string Message;

    //プレイヤーの顔グラ
    public string PlayerImageName;

    //キャラクターの顔グラ
    public string CharacterImageName;

    //BGM
    public string MusicName;

    //効果音
    public string SoundEffectName;

    //フラグ
    public string FlagName;
}

public class GroupChain
{
    public string Name;
    public List<TextBox> TextBoxChain = new List<TextBox>();
    public GroupChain(string name, List<TextBox> chain) { Name = name; TextBoxChain = chain; }
}
