using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.AppUpdate;
using Google.Play.Common;
using UnityEngine.UI;

public class InAppUpdate : MonoBehaviour
{
    [SerializeField] private Text inAppStatus;
    AppUpdateManager appUpdateManager;

    private void Start()
    {
        StartCoroutine(CheckForUpdate());
    }

    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
            appUpdateManager.GetAppUpdateInfo();

        // wait until this asynchronous operation completes.
        yield return appUpdateInfoOperation;

        if(appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            // Check AppUpdateInfo's UpdateAvailability, UpdatePriority,
            // IsUpdateTypeAllowed(), etc. and decide whether to ask the user.
            // to start an in-app update.

            // display if there is an update or not
            if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                inAppStatus.text = UpdateAvailability.UpdateAvailable.ToString();
            }
            else
            {
                inAppStatus.text = "No Update Available";
            }

            // Creates an AppUpdateOptions defining an immediate in-app.
            // update flow and its parameters.
            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
        }
    }

    IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = appUpdateManager.StartUpdate(
            appUpdateInfoOp_i,
            appUpdateOptions_i
            );

        yield return startUpdateRequest;


    }
}
