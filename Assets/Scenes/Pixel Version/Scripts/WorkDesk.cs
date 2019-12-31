using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kpable.Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class WorkDesk : MonoBehaviour
{
    public Transform incoming, outgoing, current;

    public Computer computer;
    public DriveCanvasElement[] drives;
    bool ejection;


    public GameObject cartridgePrefab;

    public Transform CurrentItem { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Register for events on drives
        foreach (var d in drives)
        {
            d.OnButtonPress += HandleDrivePressed;
            computer.AddDrive(d);
            //d.OnPowerStateChange += HandleDrivePowerStateChange;
            //d.OnInsertionStateChange += HandleDriveInsertionStateChange;
        }

    }
    private void Update()
    {
    }

    internal void LoadDay(WorkDay workDay)
    {
        // Poupulate the incoming scroll view with a quantity of cartridges
        foreach (var item in workDay.cartridges)
        {
            var go = Instantiate(cartridgePrefab);
            go.transform.SetParent(incoming, false);
        }
    }

    #region Event Handlers

    void HandleDrivePressed(Drive item)
    {
        if (CurrentItem != null)
        {
            if (item.CurrentInsertionState != DriveInsertionState.Inserted)
            {
                var cart = current.GetChild(0).GetComponent<CartridgeCanvasElement>();
                if (item.SupportedCartridges.Contains(cart.Model))
                {
                    //item.CanvasElement.Inserted = true;     
                    item.CanvasElement.InsertCartidge(cart);
                    CurrentItem.SetParent(item.CanvasElement.transform, false);
                    CurrentItem.gameObject.SetActive(false);
                    CurrentItem = null;
                }
            }
        }
    }

    private void HandleDriveInsertionStateChange(DriveInsertionState state, Drive item)
    {
        switch (state)
        {
            case DriveInsertionState.Ejected:
                if (current.childCount > 0)
                    current.GetChild(0).gameObject.SetActive(true);
                break;
            case DriveInsertionState.Inserted:
                current.GetChild(0).gameObject.SetActive(false);
                //StartScanning(item);
                break;
            default:
                break;
        }
    }


    private void HandleDrivePowerStateChange(DrivePowerState state, Drive item)
    {
        //switch (state)
        //{
        //    case DrivePowerState.Off:
        //        if (current.childCount > 0)
        //            current.GetChild(0).gameObject.SetActive(true);

        //        modulesConnected--;
        //        modulesText.Value = "Modules Connected: " + modulesConnected.ToString() + "/" + maxModulesSupported.ToString();

        //        break;
        //    case DrivePowerState.On:
        //        modulesConnected++;
        //        modulesText.Value = "Modules Connected: " + modulesConnected.ToString() + "/" + maxModulesSupported.ToString();
        //        break;
        //    default:
        //        break;
        //}
    }

    #endregion Event Handlers 



    #region Canvas Event Handlers

    public void TakeFromIncoming()
    {
        if (incoming.childCount > 0 && CurrentItem == null)
        {
            var item = (RectTransform)incoming.GetChild(0);
            item.SetParent(current, false);
            item.anchorMin = Vector2.one * 0.5f;
            item.anchorMax = Vector2.one * 0.5f;
            item.localPosition = Vector2.zero;
            CurrentItem = item;
        }
    }

    public void SendToOutgoing()
    {
        if (CurrentItem != null)
        {
            CurrentItem.SetParent(outgoing, false);
            CurrentItem = null;
            //var item = (RectTransform)current.GetChild(0);
            //item.SetParent(outgoing, false);
            //item.anchorMin = Vector2.one * 0.5f;
            //item.anchorMax = Vector2.one * 0.5f;
            //item.localPosition = Vector2.zero;
        }
    }
    #endregion Canvas Event Handlers
}
