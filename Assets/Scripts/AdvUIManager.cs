using UnityEngine;
using UnityEngine.UI;
using MyUtility;

public class AdvUIManager : SingletonMonoBehaviour<AdvUIManager>
{
    [SerializeField]
    Text dateText;

    [SerializeField]
    Text placeText;

    [SerializeField]
    Text moneyText;

    [SerializeField]
    Text staminaText;

    [SerializeField]
    Text nameText;

    [SerializeField]
    Text messageText;

    [SerializeField]
    Image BImage;

//    public void SwitchingBGImage(bool switching)
//    {
//        BImage.gameObject.SetActive(switching);
//    }

    public void UpdatePlaceText(string place)
    {
        placeText.text = place;
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToStringMoney() + " M";
    }

    public void UpdateMessageText(string name, string message)
    {
        nameText.text = name;
        messageText.text = message;
    }

    public void UpdateText()
    {
        var playerData = PlayerDataManager.Instance;
        dateText.text = playerData.currentDate.Month + "月  " + playerData.currentDate.Week + "週  " + playerData.currentDate.Day + "日目";
        UpdatePlaceText(playerData.CurrentPlace);
        UpdateMoneyText(playerData.Money);
        staminaText.text = "スタミナ：" + playerData.Stamina + " / " + playerData.MaxStamina;
    }
}
