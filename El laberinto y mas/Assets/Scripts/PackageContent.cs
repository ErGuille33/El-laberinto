using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageContent : MonoBehaviour
{
    public Sprite classicImage_;
    public Sprite iceImage_;

    LevelPackage[] packages_;

    public PackageButton packagePrefab_;
    PackageButton[] packageButtons_;

    public LevelButton levelPrefab_;
    LevelButton[] levelButtons_;

    public GameObject levelContent_;
    public GameObject scrollLevels_;

    public void Init()
    {
        packages_ = GameManager._instance.getLevelPackages();
        packageButtons_ = new PackageButton[packages_.Length];
        for(int i = 0; i < packageButtons_.Length; i++)
        {
            PackageButton package = Instantiate(packagePrefab_, transform);

            package.Init(packages_[i].name, i, this);
            if (packages_[i].isIce) package.SetImage(iceImage_);
            else package.SetImage(classicImage_);
        }
    }

    public void CreateLevels(int i)
    {
        levelButtons_ = new LevelButton[packages_[i].levels.Length];
        bool aux = packages_[i].isIce;
        for (int j = 0; j < levelButtons_.Length; j++)
        {
            LevelButton level = Instantiate(levelPrefab_, levelContent_.transform);

            level.Init(j, i);
            if (aux)  level.setImage(iceImage_);
            else level.setImage(classicImage_);
        }
        gameObject.SetActive(false);
        scrollLevels_.SetActive(true);
    }


}
