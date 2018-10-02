using System.Collections.Generic;

//メッセージの最小単位
public class TextBox
{
    //グループ
    public int GroupNumber;
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

//test
public class GroupChain
{
    public string Name;
    public List<TextBox> TextBoxChain = new List<TextBox>();
}
