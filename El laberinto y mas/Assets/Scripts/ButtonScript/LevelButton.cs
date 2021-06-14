using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text text_;
    public int num_;
    int pack_;
    public GameObject lockObject_;
    public Image image_;

    bool locked_;

    public void Init(int num, int packNum)
    {
        num_ = num;
        pack_ = packNum;
        text_.text = (num_+1).ToString();
        if (num > GameManager._instance.getPackLevels()[packNum])
        {
            lockObject_.SetActive(true);
            locked_ = true;
            text_.text = " ";
        }
    }

    public void setImage(Sprite image)
    {
        image_.sprite = image;
    }

    public void selectLevel()
    {
        if(!locked_)
            GameManager._instance.selectLevel(pack_, num_);
    }
}
