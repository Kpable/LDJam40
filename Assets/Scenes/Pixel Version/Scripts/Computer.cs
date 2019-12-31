using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Kpable.Mechanics;
using UnityEngine.UI;
using System;

public class Computer : MonoBehaviour
{
    int modulesConnected = 0;
    int maxModulesSupported = 1;
    public ObservedStringVariable modulesText, errorsText;
    public Slider slider;
    Timer timer = new Timer();

    private List<DriveCanvasElement> attachedDrives = new List<DriveCanvasElement>();

    // Start is called before the first frame update
    void Start()
    {

        slider.maxValue = 3;
        slider.value = 0;
    }

    internal void AddDrive(DriveCanvasElement drive)
    {
        drive.OnPowerStateChange += HandleDrivePowerStateChange;
        drive.OnInsertionStateChange += HandleDriveInsertionStateChange;

        attachedDrives.Add(drive);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();

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

    void UpdateSlider(int prog)
    {
        slider.value = 3 - prog;
    }

    public void HandleDrivePowerStateChange(DrivePowerState state, Drive item)
    {
        switch (state)
        {
            case DrivePowerState.Off:
              

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

    private void HandleDriveInsertionStateChange(DriveInsertionState state, Drive item)
    {
        switch (state)
        {
            case DriveInsertionState.Ejected:
                
                break;
            case DriveInsertionState.Inserted:
                StartScanning(item);
                break;
            default:
                break;
        }
    }

}
