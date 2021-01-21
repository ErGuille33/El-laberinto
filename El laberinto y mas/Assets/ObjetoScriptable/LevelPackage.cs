using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Paquetes de nivel
[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObjects/LevelGroup", order=1)]
public class LevelPackage : ScriptableObject
{
    public TextAsset[] levels;
    public bool isIce;
}
