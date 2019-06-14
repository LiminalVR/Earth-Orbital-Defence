using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Reticule : MonoBehaviour
{
    public SpriteRenderer crosshairsRenderer;
    public Sprite crosshairsSprite;
    public Button button;
    private bool start = false;
    private void Start()
    {
        button.onClick.AddListener(StartG);
        crosshairsRenderer.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        var VRDevice = Liminal.SDK.VR.VRDevice.Device;
        var pointer = VRDevice.PrimaryInputDevice.Pointer;
        if (start == true)
        {
            crosshairsRenderer.sprite = crosshairsSprite;
            crosshairsRenderer.transform.LookAt(pointer.Transform.position);
            crosshairsRenderer.transform.forward = pointer.Transform.forward * 1000.0f;
            crosshairsRenderer.transform.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);
        }
        
        
           
    }
    void StartG()
    {
        if (start == false)
        {
            start = true;
            button.gameObject.SetActive(false);
            crosshairsRenderer.gameObject.SetActive(true);
            return;
        }
        if (start == true)
        {
            start = false;
            return;
        }
    }
}
