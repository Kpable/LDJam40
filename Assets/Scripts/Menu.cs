using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Menu.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuDisabledNotice;
    [SerializeField]
    private GameObject menuContainer;

    /// <summary>
    /// Disables the Menu.
    /// </summary>
    public virtual void DisableMenu()
    {
        menuDisabledNotice.SetActive(true);
        menuContainer.SetActive(false);
    }

    /// <summary>
    /// Enables the Menu.
    /// </summary>
    public virtual void EnableMenu()
    {
        menuDisabledNotice.SetActive(false);
        menuContainer.SetActive(true);
    }
}
