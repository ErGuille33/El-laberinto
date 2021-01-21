using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowHintsButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(showHints);
    }
    public void showHints()
    {
        GameManager._instance.showHintsPanel();
    }
}
