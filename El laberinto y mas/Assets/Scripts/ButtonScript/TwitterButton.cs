using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TwitterButton : MonoBehaviour
{
    public string twitterNameParamter = "Descarga este increible juego de resolver laberintos. Yo ya he completado el nivel ";
    public string twitterDescriptionParam = "";
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWITTER_LANGUAGE = "en";

    public string LINK_GAME = "https://freesstylers.github.io/District-Dance-Battle/";
    public GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(shareOnTwitter);
    }

    // Update is called once per frame
    void shareOnTwitter()
    {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(twitterNameParamter + (gm.levelToPlay+1) + twitterDescriptionParam + "\n" + LINK_GAME));
        gm.buyHint();
    }
}
