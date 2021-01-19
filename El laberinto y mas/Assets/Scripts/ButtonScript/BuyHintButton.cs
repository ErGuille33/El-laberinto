using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;

public class BuyHintButton : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3974381";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        gameObject.GetComponent<Button>().onClick.AddListener(ShowRewardedVideo);
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.Show(myPlacementId);
        }
        else
        {
            Debug.Log("No esta ready hermano");
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            gm.buyHint();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Te queda sin hint tonto
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("No ha terminado el ad por error");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == myPlacementId)
        {
            // Para cuando ya puedes usar el boton de anuncio
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        print("Ha habido un error con el anuncio");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Cosas opcionales que puedes hacer cuando el anuncio empieza
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
