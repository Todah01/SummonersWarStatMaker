using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Common;
using Google.Play.AppUpdate;
using Cysharp.Threading.Tasks;
using System;

public class googleplay_manager : MonoBehaviour
{
    public static googleplay_manager Instance { get; private set; }
    AppUpdateManager appUpdateManager = null;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    // In App Update Function
    public async UniTask UpdateApp()
    {
        try
        {
            appUpdateManager = new AppUpdateManager();

            PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();
            await appUpdateInfoOperation;

            if (appUpdateInfoOperation.IsSuccessful)
            {
                var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                await startUpdateRequest;
            }
            else
            {
                Debug.Log(appUpdateInfoOperation.Error);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
