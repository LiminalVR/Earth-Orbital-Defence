using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{
    public SpriteRenderer crosshairsRenderer;
    public Sprite crosshairsSprite;
 
    private void FixedUpdate()
    {
    
        var VRDevice = Liminal.SDK.VR.VRDevice.Device; 
        var pointer = VRDevice.PrimaryInputDevice.Pointer;
        
        crosshairsRenderer.sprite = crosshairsSprite;
        crosshairsRenderer.transform.LookAt(pointer.Transform.position);
        crosshairsRenderer.transform.forward = pointer.Transform.forward * 1000.0f;
        crosshairsRenderer.transform.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);      
    }            
}
