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

    public delegate void DriveEject();
    public DriveEject OnDriveEject;

    public bool On
    {
        set
        {
            _on = value;
            if (_on)
                driveImage.sprite = ((Inserted) ? model.OnInserted : model.On);
            else
                driveImage.sprite = ((Inserted) ? model.OffInserted : model.Off);
        }
        private get { return _on; }
    }

    public bool Inserted
    {
        set
        {
            _inserted = value;
            if (_inserted)
                driveImage.sprite = ((On) ? model.OnInserted : model.OffInserted);
            else
                driveImage.sprite = ((On) ? model.On : model.Off);
        }
        private get { return _inserted; }
    }

    void HandleButton(Drive model)
    {
        if (!Inserted)
            On = !On;
        else
        {
            if (OnDriveEject != null) OnDriveEject();
            Inserted = false;
        }
    }

    void Start()
    {
        OnButtonPress += HandleButton;
    }


}
