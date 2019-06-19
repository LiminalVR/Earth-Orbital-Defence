using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticule : MonoBehaviour
{
    public SpriteRenderer crosshairsRenderer;
    public Sprite crosshairsSprite;
    public GameObject cannonPrefab;
    public Button button;
    public GameObject l;
    public GameObject lazer;


    private bool fire;

  
    private bool start = false;
    private void Start()
    {
        button.onClick.AddListener(StartG);
        crosshairsRenderer.gameObject.SetActive(false);

        fire = l.GetComponent<Fire>().fired;

        Instantiate(lazer);
        lazer.SetActive(false);
    }


    private void FixedUpdate()
    {
        var VRDevice = Liminal.SDK.VR.VRDevice.Device; 
        var pointer = VRDevice.PrimaryInputDevice.Pointer;


        if (start == true)
        crosshairsRenderer.transform.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);
        cannonPrefab.transform.rotation = pointer.Transform.rotation;

        if(fire)
        {
            Debug.Log("Im in");
            lazer.SetActive(true);
            lazer.transform.position = pointer.Transform.position;
            lazer.transform.rotation = pointer.Transform.rotation;
        }

        else
        {
            lazer.SetActive(false);
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


