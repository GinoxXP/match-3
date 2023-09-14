using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsService : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] 
    string androidAdUnitId = "Rewarded_Android";
    [SerializeField]
    string iOSAdUnitId = "Rewarded_iOS";

    string adUnitId = null;

    public event Action AdLoaded;

    public event Action AdShowComplete;

    public event Action AdFailedToLoad;

    public event Action AdShowFailure;

    public void LoadAd()
        => Advertisement.Load(adUnitId, this);

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(this.adUnitId))
            AdLoaded?.Invoke();
    }

    public void ShowAd()
    {
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(this.adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            AdShowComplete?.Invoke();
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        AdFailedToLoad?.Invoke();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        AdShowFailure?.Invoke();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void Awake()
    {
#if UNITY_IOS
        adUnitId = iOSAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }
}
