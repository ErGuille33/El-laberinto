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

        float nivelespasados = GameManager._instance.getPackLevels()[id];
        float total = GameManager._instance.getLevelPackages()[id].levels.Length;
        float div = nivelespasados / total;
        int percentage = Mathf.RoundToInt(div*100.0f);

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
