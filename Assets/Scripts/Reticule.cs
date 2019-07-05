using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticule : MonoBehaviour
{
    public Transform Crosshairs;
    public Image EnergyFill;
    public Button button;

    private bool start = false;
    private void Start()
    {
        button.onClick.AddListener(StartG);
        Crosshairs.gameObject.SetActive(false);
    }


    private void FixedUpdate()
    {
        var VRDevice = Liminal.SDK.VR.VRDevice.Device; 
        var pointer = VRDevice.PrimaryInputDevice.Pointer;


        if (start == true)
        Crosshairs.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);
    }    
    
     void StartG()
    {
        if (start == false)
        {
            start = true;
            button.gameObject.SetActive(false);
            Crosshairs.gameObject.SetActive(true);
            return;
        }
        if (start == true)
        {
            start = false;
            return;
        }
    }
}   


