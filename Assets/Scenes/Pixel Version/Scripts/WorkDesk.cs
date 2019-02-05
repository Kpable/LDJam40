using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kpable.Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class WorkDesk : MonoBehaviour
{
    public Transform incoming, outgoing, currentItem;

    public DriveCanvasElement[] drives;
    bool ejection;
    int modulesConnected = 0;
    int maxModulesSupported = 1;
    public ObservedStringVariable modulesText, errorsText;
    public Slider slider;
    Timer timer = new Timer();
    public GameObject cartridgePrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in drives)
        {
            item.OnButtonPress += HandleItemPress;
            item.OnPowerStateChange += HandleDrivePowerStateChange;
            item.OnInsertionStateChange += HandleDriveInsertionStateChange;
        }

        slider.maxValue = 3;
        slider.value = 0;
    }
    private void Update()
    {
        timer.Update();
    }


    private void HandleDriveInsertionStateChange(DriveInsertionState state, Drive item)
    {
        switch (state)
        {
            case DriveInsertionState.Ejected:
                if (currentItem.childCount > 0)
                    currentItem.GetChild(0).gameObject.SetActive(true);
                break;
            case DriveInsertionState.Inserted:
                currentItem.GetChild(0).gameObject.SetActive(false);
                StartScanning(item);
                break;
            default:
                break;
        }
    }

    internal void LoadDay(WorkDay workDay)
    {
        foreach (var item in workDay.cartridges)
        {
            var go = Instantiate(cartridgePrefab);
            go.transform.SetParent(incoming, false);
        }
    }

    private void StartScanning(Drive item)
    {
        Debug.Log("Begining Scan");
        timer.OnSecondsChanged += UpdateSlider;
        timer.OnTimeUp += ScanComplete;
        timer.Set(item.SupportedCartridges[0].ScanTime, true);
    }

    private void ScanComplete()
    {
        Debug.Log("Scan Complete");
        ReportErrors();
    }

    private void ReportErrors()
    {
        Debug.Log("Report errors");
        errorsText.Value = "Erros dectected: 0";
    }

    void UpdateSlider( int prog)
    {
        slider.value = 3 - prog;
    }

    private void HandleDrivePowerStateChange(DrivePowerState state, Drive item)
    {
        switch (state)
        {
            case DrivePowerState.Off:
                if (currentItem.childCount > 0)                
                    currentItem.GetChild(0).gameObject.SetActive(true);

                modulesConnected--;
                modulesText.Value = "Modules Connected: " + modulesConnected.ToString() + "/" + maxModulesSupported.ToString();

                break;
            case DrivePowerState.On:
                modulesConnected++;
                modulesText.Value = "Modules Connected: " + modulesConnected.ToString() + "/" + maxModulesSupported.ToString();
                break;
            default:
                break;
        }
    }

    public void TakeFromIncoming()
    {
        if (incoming.childCount > 0 && currentItem.childCount == 0)
        {
            var item = (RectTransform)incoming.GetChild(0);
            item.SetParent(currentItem, false);
            item.anchorMin = Vector2.one * 0.5f;
            item.anchorMax = Vector2.one * 0.5f;
            item.localPosition = Vector2.zero;
        }
    }

    public void SendToOutgoing()
    {
        if (currentItem.childCount > 0)
        {
            var item = (RectTransform)currentItem.GetChild(0);
            item.SetParent(outgoing, false);
            //item.anchorMin = Vector2.one * 0.5f;
            //item.anchorMax = Vector2.one * 0.5f;
            //item.localPosition = Vector2.zero;
        }
    }

    void HandleItemPress(Drive item)
    {
        if (currentItem.childCount > 0)
        {
            if (item.CurrentInsertionState != DriveInsertionState.Inserted)
            {
                var cart = currentItem.GetChild(0).GetComponent<CartridgeCanvasElement>();
                if (item.SupportedCartridges.Contains(cart.Model))
                {
                    //item.CanvasElement.Inserted = true;     
                    item.CanvasElement.InsertDrive();
                }
            }
        }
    }
}
