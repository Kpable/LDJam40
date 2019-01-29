using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{
    // On off
    // Cartridge inserted/ not inserted
    [SerializeField]
    private Cartridge[] supportedCartridges;

    [SerializeField]
    private Image driveImage;

    [SerializeField]
    private Sprite off, on, offInserted, onInserted;

    private bool _on = false;
    private bool _inserted = false;

    public bool On
    {
        set
        {
            _on = value;
            if (_on)
                driveImage.sprite = ((Inserted) ? onInserted : on);
            else
                driveImage.sprite = ((Inserted) ? offInserted : off);
        }
        private get { return _on; }
    }

    public bool Inserted
    {
        set
        {
            _inserted = value;
            if (_inserted)
                driveImage.sprite = ((On) ? onInserted : offInserted);
            else
                driveImage.sprite = ((On) ? on : off);
        }
        private get { return _inserted; }
    }

    public void HandleButton()
    {
        On = !On;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
