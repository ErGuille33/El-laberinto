using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackHintButton : MonoBehaviour
{
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(hideHintsPanel);
    }
    public void hideHintsPanel()
    {
        gm.hideHintsPanel();
    }

}
