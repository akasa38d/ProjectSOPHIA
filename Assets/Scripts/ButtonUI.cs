using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField]
    int Number;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Number >= 0)
        {
            if (Number < Controller.CatchButtonsCount)
            {
                Controller.Instance.CatchButtonsEnter[Number] = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Number >= 0)
        {
            if (Number < Controller.CatchButtonsCount)
            {
                Controller.Instance.CatchButtonsEnter[Number] = true;
                Controller.Instance.CatchButtonsDown[Number] = true;
            }
        }
    }
}
