using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageUI : MonoBehaviour
{
    Image ObjectImage;

    //施設の画像
    [SerializeField]
    public List<Sprite> SpriteList;

    public void SwitchImage(int num)
    {
        if(num < SpriteList.Count)
        {
            ObjectImage.sprite = SpriteList[num];
        }
        else { throw new ArgumentException("設定されたスプライトの範囲外！"); }        
    }    

    public virtual void Awake()
    {
        ObjectImage = GetComponent<Image>();
    }
}
