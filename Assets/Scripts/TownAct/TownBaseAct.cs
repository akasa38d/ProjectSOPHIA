using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBaseAct : AbstractTownAct
{
    public List<Facility> Facilities;

    TownBaseIterator advIterator;

    TownBaseSet townBaseSet;

    //コンストラクタ
    public TownBaseAct(string name, TownBaseSet component)
    {
        Name = name;
        townBaseSet = component;

        Facilities = new List<Facility>() { new Atelier(), new Coffee(), new Market(), new Dungeon() };

        advIterator = new TownBaseIterator(this);
    }

    //AdvPartManagerでの起動
    public override void StartUp()
    {
        base.StartUp();

        OpenImage();

        enterFacility(advIterator.Facility);

        selectFacilityActs(advIterator.FacilityAct);
    }

    //AdvPartManagerでの一時消し
    public override void Close()
    {
        foreach (var a in townBaseSet.FacilityButtons)
        {
            a.transform.localScale = new Vector3(1, 1, 1);
            a.SetActive(false);
        }
    }

    public void CloseImage()
    {
        townBaseSet.FacilityImage.SetActive(false);
    }

    public void OpenImage()
    {
        townBaseSet.FacilityImage.SetActive(true);
    }

    //施設の移動（準備）
    void toVisitFacility()
    {
        //現在地！＝移動先なら
        if (advIterator.TempFacility != advIterator.Facility)
        {
            FrameUIManager.Instance.UpdateMessageText("", "移動する？");
            Close();
        }
        //現在地＝＝移動先なら
        else
        {
            visitFacility(advIterator.TempFacility);
            selectFacilityActs(advIterator.FacilityAct);
        }

        townBaseSet.BGFacility.sprite = townBaseSet.facilitySprites[advIterator.TempFacility];
    }

    void visitFacility(int facilityNumber)
    {
        var facility = Facilities[facilityNumber];

        //施設からテキストを取得
        var textBoxes = facility.firstFlag == false ? facility.welcomeFirst : facility.welcomeNext;

        //取得したテキストからメッセージを作成
        var proxyText = new SingleTextAct(textBoxes, () =>
        {
            enterFacility(facilityNumber);
            selectFacilityActs(advIterator.FacilityAct);
            facility.firstFlag = true;
            AdvPartManager.Instance.CurrentAct = this;
        });

        proxyText.StartUp();
        PlayerDataManager.Instance.CurrentPlace = facility.Name;
    }

    //施設の移動（確定）
    void enterFacility(int facilityNumber)
    {
        var facility = Facilities[facilityNumber];

        Close();

        //施設のActに合わせてボタンを更新
        for (int i = 0; i < townBaseSet.FacilityButtons.Count; i++)
        {
            if (i < facility.FacilityActs.Count)
            {
                townBaseSet.FacilityButtons[i].SetActive(true);
                townBaseSet.FacilityButtons[i].transform.GetChild(0).GetComponent<Text>().text = facility.FacilityActs[i].Name;
            }
        }

        PlayerDataManager.Instance.CurrentPlace = facility.Name;
        FrameUIManager.Instance.UpdateMessageText("", "何をしようか？");
    }

    //行動選択アニメーター
    void selectFacilityActs(int ActNumber)
    {
        for (int i = 0; i < townBaseSet.FacilityButtons.Count; i++)
        {
            if (i == ActNumber)
            {
                townBaseSet.FacilityButtons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }
            else if (townBaseSet.FacilityButtons[i].activeSelf == true)
            {
                townBaseSet.FacilityButtons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach(var n in Facilities) { n.Refresh(); }
    }

    //コントロール
    public override void Update()
    {
        //施設移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            advIterator.TempFacility--;
            toVisitFacility();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            advIterator.TempFacility++;
            toVisitFacility();
        }

        //行動選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (advIterator.TempFacility == advIterator.Facility)
            {
                advIterator.FacilityAct++;
                selectFacilityActs(advIterator.FacilityAct);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (advIterator.TempFacility == advIterator.Facility)
            {
                advIterator.FacilityAct--;
                selectFacilityActs(advIterator.FacilityAct);
            }
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (advIterator.TempFacility != advIterator.Facility)
            {
                advIterator.Facility = advIterator.TempFacility;
                advIterator.ResetFacilityAct();
                visitFacility(advIterator.TempFacility);
            }
            else
            {
                Facilities[advIterator.Facility].FacilityActs[advIterator.FacilityAct].Action();
            }
        }

        //マウス操作
        for (int i = 0; i < Facilities.Count; i++)
        {
            if (Controller.Instance.CatchButtonsEnter[i] == true)
            {
                advIterator.FacilityAct = i;
                selectFacilityActs(i);
            }

            if (Controller.Instance.CatchButtonsDown[i] == true)
            {
                advIterator.FacilityAct = i;
                selectFacilityActs(i);
                Facilities[advIterator.Facility].FacilityActs[advIterator.FacilityAct].Action();
            }
        }
    }
}
