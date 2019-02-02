using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kpable/Performance Review/Drive", fileName = "New Drive")]
public class Drive : Model
{
    // On off
    // Cartridge inserted/ not inserted
    [SerializeField]
    private Cartridge[] supportedCartridges;
    [SerializeField]
    private Sprite off, on, offInserted, onInserted;



    public Sprite Off { get { return off; } }
    public Sprite On { get { return on; } }
    public Sprite OffInserted { get { return offInserted; } }
    public Sprite OnInserted { get { return onInserted; } }
    public Cartridge[] SupportedCartridges { get { return supportedCartridges; } }

    public DrivePowerState CurrentPowerState { get; set; }
    public DriveInsertionState CurrentInsertionState { get; set; }

    public DriveCanvasElement CanvasElement { get; set; }
}

public enum DrivePowerState { Off, On}
public enum DriveInsertionState { Ejected, Inserted }