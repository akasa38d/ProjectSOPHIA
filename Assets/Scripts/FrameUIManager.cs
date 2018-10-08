using UnityEngine;
using UnityEngine.UI;
using MyUtility;

public class FrameUIManager : SingletonMonoBehaviour<FrameUIManager>
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

    PlayerDataManager playerDataManager { get { return PlayerDataManager.Instance; } }

    //    public void SwitchingBGImage(bool switching)
    //    {
    //        BImage.gameObject.SetActive(switching);
    //    }

    public void UpdateDay()
    {
        dateText.text = playerDataManager.Day + "日目";
    }

    public void UpdatePlaceText()
    {
        placeText.text = playerDataManager.CurrentPlace;
    }

    public void UpdateMoneyText()
    {
        moneyText.text = playerDataManager.Money.ToStringMoney() + " M";
    }

    public void UpdateStamina()
    {
        staminaText.text = "スタミナ：" + playerDataManager.Stamina + " / " + playerDataManager.MaxStamina;
    }

    public void UpdateMessageText(string name, string message)
    {
        nameText.text = name;
        messageText.text = message;
    }
}
