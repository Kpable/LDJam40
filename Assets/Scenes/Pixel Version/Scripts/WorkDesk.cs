using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kpable.Mechanics;
using UnityEngine;

public class WorkDesk : MonoBehaviour
{
    public Transform incoming, outgoing, currentItem;

    public DriveCanvasElement[] drives;
    bool ejection;
    int modulesConnected = 0;
    int maxModulesSupported = 1;
    public ObservedStringVariable modulesText;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in drives)
        {
            item.OnButtonPress += HandleItemPress;
            item.OnPowerStateChange += HandleDrivePowerStateChange;
            item.OnInsertionStateChange += HandleDriveInsertionStateChange;
        }
    }

    private void HandleDriveInsertionStateChange(DriveInsertionState state)
    {
        switch (state)
        {
            case DriveInsertionState.Ejected:
                if (currentItem.childCount > 0)
                    currentItem.GetChild(0).gameObject.SetActive(true);
                break;
            case DriveInsertionState.Inserted:
                currentItem.GetChild(0).gameObject.SetActive(false);

                break;
            default:
                break;
        }
    }

    private void HandleDrivePowerStateChange(DrivePowerState state)
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
        if (incoming.childCount > 0)
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
