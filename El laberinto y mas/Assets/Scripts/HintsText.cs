using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsText : MonoBehaviour
{

    [SerializeField]
    Text textComponent;

    public void updateHints(int numHints)
    {
        textComponent.text = numHints.ToString();
    }


}
