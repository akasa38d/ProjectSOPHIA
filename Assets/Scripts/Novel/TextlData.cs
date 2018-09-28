using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//メッセージの定義を行うクラス
public class TextData
{
    public string Name;

    //TextTypeとTextBoxChainが必要

    //通常の会話、複数人から選択して会話、選択肢のある会話
    public enum Type { Single, Multi, Choice };
    public Type TextType;

    public List<string> GroupName = new List<string>();
    public int GroupNumber = 0;

    public List<TextBox> TextBoxChain = new List<TextBox>();
    public List<TestChain> TestChains = new List<TestChain>();

    public TextData(string textFile)
    {
        Name = "";
        AnalyzeTextFile(textFile);
    }

    public void AnalyzeTextFile(string textFile)
    {
        //空白行で分割
        var splitTexts = new List<string>(textFile.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries));

        //タイプを判断
        if (Enum.TryParse(splitTexts[0], out TextType) == false) { Debug.Log("読み取り失敗"); }

        //TextBoxChainの作成
        for (int i = 1; i < splitTexts.Count; i++)
        {
            var textBox = new TextBox();

            var splitByLine = new List<string>(splitTexts[i].Split('\n'));

            string groupName = "";

            //タグの列挙
            for (int j = 0; j < splitByLine.Count; j++)
            {
                analyzeBuTag(ref textBox, splitByLine[j]);

                if (splitByLine[j].Contains("@@Group"))
                {
                    GroupNumber++;
                    groupName = splitByLine[j].Replace("@@Group ", "");
                    GroupName.Add(groupName);
                }                

                if (TextType == Type.Multi)
                {
                    textBox.GroupNumber = GroupNumber;
                    textBox.GroupName = GroupName.Last();
                }                
            }

            TextBoxChain.Add(textBox);
        }

        if(TextType == Type.Multi)
        {
            groupByName();      
        }        
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
            var testChain = new TestChain() { Name = person.Key };

            foreach (var textBox in person)
            {
                testChain.TextBoxChain.Add(textBox);
            }

            TestChains.Add(testChain);
        }
    }
}

