using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameObject homeButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Play);
    }

    // Update is called once per frame
    void Play()
    {
        GameManager._instance.state = GameManager.State.PACK;
        homeButton.SetActive(true);
    }
}
