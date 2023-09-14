using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour
{
    [SerializeField]
    private float addTime;

    private AdsService adsService;
    private Timer timer;
    private Button button;

    public void ShowAd()
    {
        button.interactable = false;

        adsService.AdShowComplete += OnAdShowComplete;
        adsService.ShowAd();
    }

    private void OnAdShowComplete()
    {
        adsService.AdShowComplete -= OnAdShowComplete;
        timer.AddTime(addTime);
    }

    private void OnDestroy()
    {
        adsService.AdShowComplete -= OnAdShowComplete;
    }

    private void Start()
    {
        button = GetComponent<Button>();

        adsService.LoadAd();
    }

    [Inject]
    private void Init(AdsService adsService, Timer timer)
    {
        this.adsService = adsService;
        this.timer = timer;
    }
}