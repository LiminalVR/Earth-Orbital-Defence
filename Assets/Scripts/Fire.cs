using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [Header("Events")]
    [Tooltip("Raised when Button Pressed")]
    public UnityEngine.Events.UnityEvent OnPressed = new UnityEngine.Events.UnityEvent();
    #region MonoBehaviour
    private void Update()
    {
        this.gameObject.transform.position = obj.transform.position;
        this.gameObject.transform.rotation = obj.transform.rotation;
        var pointer = Liminal.SDK.VR.VRDevice.Device.PrimaryInputDevice.Pointer;
        
        //this.gameObject.transform.localPosition = obj.transform.localPosition;
        FireGun();
    }
    #endregion
    public GameObject obj;

    // Enemy Count //////////////
    public Text textBox;
    private int enemyCount = 0;
    ////////////////////////////

    private void FireGun()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        var vrDevice = Liminal.SDK.VR.VRDevice.Device;
        var pointer = vrDevice.PrimaryInputDevice.Pointer;
        
        if (vrDevice == null)
        {
            Debug.Log("VRDEVICE WAS NULL");
            return;
        }
        
        var input = vrDevice.PrimaryInputDevice;
        if (input == null)
        {
            Debug.Log("VR INPUT WAS NULL");
            return;
        }
        
        if (input.GetButtonDown(Liminal.SDK.VR.Input.VRButton.One))
        {
            
            Debug.Log(string.Format("[InputHandler] Input detected: {0}", Liminal.SDK.VR.Input.VRButton.One), this);
            if (Physics.Raycast(pointer.Transform.position, pointer.Transform.forward, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(pointer.Transform.position, pointer.Transform.forward * hit.distance, Color.yellow,40,false);
                 if(hit.collider.CompareTag("Enemy"))
                 {
                    hit.collider.gameObject.SetActive(false);

                    // Enemy Count ///////////////////////
                    enemyCount++;
                    textBox.text = enemyCount.ToString();
                    print("WE got one!!!");
                    //////////////////////////////////////
                }
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(pointer.Transform.position, pointer.Transform.forward * 1000, Color.white,40,false);
                Debug.Log("Did not Hit");
            }

            OnPressed.Invoke();
        }

    }
}

