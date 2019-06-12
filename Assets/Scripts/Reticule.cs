using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{
    public SpriteRenderer crosshairsRenderer;
    public Sprite crosshairsSprite;
    public GameObject cannonPrefab;
    

 
    private void FixedUpdate()
    {
        var VRDevice = Liminal.SDK.VR.VRDevice.Device; 
        var pointer = VRDevice.PrimaryInputDevice.Pointer;


        //cannonPrefab.transform.rotation = new Quaternion(pointer.Transform.rotation.x,
        //     pointer.Transform.rotation.y,
        //     pointer.Transform.rotation.z,
        //    0.0f);

        crosshairsRenderer.transform.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);
       // cannonPrefab.transform.LookAt(crosshairsRenderer.transform.position, pointer.Transform.right);
    }            
}


//   crosshairsRenderer.sprite = crosshairsSprite;
//   crosshairsRenderer.transform.LookAt(pointer.Transform.position);
//  crosshairsRenderer.transform.forward = pointer.Transform.forward * -1000.0f;