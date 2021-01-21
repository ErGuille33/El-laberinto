using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackHintButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(hideHintsPanel);
    }
    public void hideHintsPanel()
    {
        GameManager._instance.hideHintsPanel();
    }

}
