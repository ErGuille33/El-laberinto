using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3974381";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowRewardedVideo()
    {
        if(Advertisement.IsReady(myPlacementId))
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
        if(showResult == ShowResult.Finished)
        {
            // Dar una Hint supongo
        }
        else if(showResult == ShowResult.Skipped)
        {
            // Te queda sin hint tonto
        }
        else if(showResult == ShowResult.Failed)
        {
            Debug.LogWarning("No ha terminado el ad por error");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if(placementId == myPlacementId)
        {
            // Para cuando ya puedes usar el boton de anuncio
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
