using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class adManager : MonoBehaviour
{
    // "ca-app-pub-3940256099942544/1033173712"; // test id
    // "ca-app-pub-1419825942572265/3689203384";// updated app id 
    public string interstitialID = "ca-app-pub-3940256099942544/1033173712"; // test od
    InterstitialAd interstitial;
    public bool adClosed;
    bool adFailedToLoad;
    //  

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        
        // deviceIds.Add("070DC918DA7653E44A77BAA0543DFE12");

        // RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTestDeviceIds(deviceIds).build();

        // MobileAds.SetRequestConfiguration(requestConfiguration);

        RequestInterstitial();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(interstitialID);
        // handle ad closing
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        
        // Create an empty ad request. for test device
        // AdRequest request = new AdRequest.Builder().AddTestDevice("070DC918DA7653E44A77BAA0543DFE12").Build();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void displayInterstitial()
    {
        if (interstitial.IsLoaded()) 
        {
            interstitial.Show();
        }
        // if(!adFailedToLoad)
        // {
        //     if (interstitial.IsLoaded()) 
        //     {
        //         interstitial.Show();
        //     }
        // } else 
        // {
        //     levelManager.instance.closeLevel();
        // }
        
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // MonoBehaviour.print("HandleAdClosed event received");
        adClosed = true;
        levelManager.instance.closeLevel();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // string msg = args.AdError.GetMessage();
        // MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
        Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
        // adClosed = true;
        adFailedToLoad = true;
        // levelManager.instance.closeLevel();
    }

    public void destroyInterStitial()
    {
        interstitial.Destroy();
    }
}
