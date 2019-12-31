using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriveCanvasElement : CanvasElement<Drive>
{
    [Header("UI")]
    [SerializeField]
    private Image driveImage;

    private bool _on = false;
    private bool _inserted = false;

    public delegate void PowerStateChange(DrivePowerState state, Drive item);
    public PowerStateChange OnPowerStateChange;

    public delegate void InsertionStateChange(DriveInsertionState state, Drive item);
    public InsertionStateChange OnInsertionStateChange;

    List<CartridgeCanvasElement> InsertedCarts = new List<CartridgeCanvasElement>();

    public bool On
    {
        set
        {
            _on = value;
            if (_on)
            {
                model.CurrentPowerState = DrivePowerState.On;
            }
            else
            {
                model.CurrentPowerState = DrivePowerState.Off;
            }

            driveImage.sprite = ((On) ? model.On : model.Off);

            if (OnPowerStateChange != null)
                OnPowerStateChange(((On) ? DrivePowerState.On : DrivePowerState.Off), model);
        }
        private get { return _on; }
    }

    public bool Inserted
    {
        set
        {
            _inserted = value;
            if (_inserted)
            {
                model.CurrentInsertionState = DriveInsertionState.Inserted;
                if (!On) On = true;
                driveImage.sprite = model.OnInserted;
            }
            else
            {
                model.CurrentInsertionState = DriveInsertionState.Ejected;
                On = false;
            }            

            if (OnInsertionStateChange != null)
                OnInsertionStateChange(model.CurrentInsertionState, model);
        }
        private get { return _inserted; }
    }

    public bool inserting = false;

    void HandleButton(Drive model)
    {
        if (model.CurrentInsertionState == DriveInsertionState.Ejected)
        {
            On = !On;            
        }
        else
        {
            if (!inserting)
            {
                Inserted = !Inserted;
            }
            else
            {
                inserting = false;
            }
        }
        
    }

    public void InsertCartidge(CartridgeCanvasElement cart)
    {
        inserting = true;
        Inserted = true;
        InsertedCarts.Add(cart);
    }

    void Start()
    {
        OnButtonPress += HandleButton;
        model.CanvasElement = this;
        model.CurrentPowerState = DrivePowerState.Off;
        model.CurrentInsertionState = DriveInsertionState.Ejected;

    }


}
