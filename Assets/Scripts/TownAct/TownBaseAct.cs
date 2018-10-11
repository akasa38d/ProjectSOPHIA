using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TownBaseAct : AbstractTownAct
{
    public List<Facility> Facilities;

    TownBaseIterator advIterator;

    VerticalSelect select { get { return AdvPartManager.Instance.GetBasicSelect; } }
    GameObject facilityImage { get { return AdvPartManager.Instance.FacilityImage; } }

    //コンストラクタ
    public TownBaseAct()
    {
        Facilities = new List<Facility>() { new Atelier(), new Coffee(), new Market(), new Dungeon() };
        advIterator = new TownBaseIterator(Facilities.Select(n => n.FacilityActs.Count).ToArray());
    }

    //AdvPartManagerでの起動
    public override void StartUp()
    {
        base.StartUp();

        AdvPartManager.Instance.ActivateFacilityImage(true);

        enterFacility(advIterator.Facility);

        selectFacilityActs(advIterator.FacilityAct);
    }

    //AdvPartManagerでの一時消し
    public override void Close()
    {
        select.Close();
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

        facilityImage.GetComponent<ImageUI>().SwitchImage(advIterator.TempFacility);
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
        for (int i = 0; i < select.Buttons.Count; i++)
        {
            if (i < facility.FacilityActs.Count)
            {
                select.Buttons[i].SetActive(true);
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = facility.FacilityActs[i].Name;
            }
        }

        PlayerDataManager.Instance.CurrentPlace = facility.Name;
        FrameUIManager.Instance.UpdateMessageText("", "何をしようか？");
    }

    //行動選択アニメーター
    void selectFacilityActs(int ActNumber)
    {
        for (int i = 0; i < select.Buttons.Count; i++)
        {
            if (i == ActNumber)
            {
                select.Buttons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }
            else if (select.Buttons[i].activeSelf == true)
            {
                select.Buttons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach (var n in Facilities) { n.Refresh(); }
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
