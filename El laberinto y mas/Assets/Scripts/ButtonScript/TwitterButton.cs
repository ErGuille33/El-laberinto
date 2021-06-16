using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class TwitterButton : MonoBehaviour
{
    public string twitterNameParamter = "Descarga este increible juego de resolver laberintos.";
    public string twitterLevelCompleted = " Yo ya he completado el nivel ";
    public string twitterDescriptionParam = "";
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWITTER_LANGUAGE = "en";

    public string LINK_GAME = "https://freesstylers.github.io/District-Dance-Battle/";


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(shareOnTwitter);
    }

    // Update is called once per frame
    void shareOnTwitter()
    {
        /*if (GameManager._instance.state != GameManager.State.INI)
        {
            Application.OpenURL(TWITTER_ADDRESS + "?text=" + UnityWebRequest.EscapeURL(twitterNameParamter + twitterLevelCompleted + (GameManager._instance.getLevelNum() + 1) + twitterDescriptionParam + "\n" + LINK_GAME));
            GameManager._instance.buyHint();
        }
        else
        {
            Application.OpenURL(TWITTER_ADDRESS + "?text=" + UnityWebRequest.EscapeURL(twitterNameParamter + twitterDescriptionParam + "\n" + LINK_GAME));
            GameManager._instance.buyHint();
        }
        */
    }
}
