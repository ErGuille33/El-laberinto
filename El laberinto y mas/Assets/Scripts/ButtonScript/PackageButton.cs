using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageButton : MonoBehaviour
{

    public Text name_;
    public Text percentage_;
    public Image image_;
    int id_;

    PackageContent content_;

    public void Init(string package, int id, PackageContent content)
    {
        name_.text = package;

        int percentage = (GameManager._instance.getPackLevels()[id] / GameManager._instance.getLevelPackages()[id].levels.Length)*100;

        percentage_.text = percentage.ToString() + "%";
        id_ = id;
        content_ = content;
    }

    public void SetImage(Sprite image)
    {
        image_.sprite = image;
    }

    public void CreateLevels()
    {
        content_.CreateLevels(id_);
    }

}
