using System.Collections;
using System;
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
    public GameObject ExplosionEffect;
    public GameObject FireEffect;
    public GameObject FireEffectSP;
    public GameObject earth;
    public GameObject longboi;
    private AudioSource Gunfire;
    private AudioSource Explosion;
    public bool fired;
        

    private GameObject[] Ex;

    private GameObject[] Gu;
    // Enemy Count //////////////
    public Text textBox;
    private int enemyCount = 0;
    ////////////////////////////
    private void Start()
    {
        Gunfire = GetComponent<AudioSource>();
        Explosion = GetComponent<AudioSource>();
        Ex = new GameObject[50];
        Gu = new GameObject[10];
        for (int i = 0; i < 50; i++)
        {
            Ex[i] = GameObject.Instantiate(ExplosionEffect, new Vector3(0, 0, 0), new Quaternion());
            Ex[i].SetActive(false);
        }
        for (int i = 0; i < 10; i++)
        {
            Gu[i] = GameObject.Instantiate(FireEffect, new Vector3(0, 0, 0), new Quaternion());
            Gu[i].SetActive(false);
        }
    }




    public void FireGun()
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
            ///////////////////
            // Instantiate(longboi, pointer.Transform.position, pointer.Transform.rotation);
            fired = true;

            ///////////////////////

            Debug.Log(string.Format("[InputHandler] Input detected: {0}", Liminal.SDK.VR.Input.VRButton.One), this);
            if (Physics.Raycast(pointer.Transform.position, pointer.Transform.forward, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(pointer.Transform.position, pointer.Transform.forward * hit.distance, Color.yellow, 40, false);
                //PlayerEffect(1, FireEffectSP.transform.position);
                Gunfire.Play();
                if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Button") ||
                    hit.collider.CompareTag("Enemy2") ||
                    hit.collider.CompareTag("Enemy3"))
                {
                    hit.collider.gameObject.SetActive(false);
                   // Instantiate(longboi, hit.transform.position, pointer.Transform.rotation);
                    PlayerEffect(0, hit.transform.position);
                    Explosion.Play();

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
                Debug.DrawRay(pointer.Transform.position, pointer.Transform.forward * 1000, Color.white, 40, false);
                Debug.Log("Did not Hit");
                fired = false;
                //Instantiate(longboi, pointer.Transform.position, pointer.Transform.rotation);
            }
            
            OnPressed.Invoke();
        }

    }

    private void PlayerEffect(int effect, Vector3 pos)
    {
        switch (effect)
        {
            case 0:

                for (int i = 0; i < 50; i++)
                {
                    if (Ex[i].activeInHierarchy)
                    {
                        Ex[i].SetActive(false);
                    }
                    else
                    {
                        Ex[i].transform.position = pos;
                        Ex[i].SetActive(true);
                        i = 50;


                    }
                }
                break;
            case 1:
                for (int i = 0; i < 10; i++)
                {
                    if (Gu[i].activeInHierarchy)
                    {
                        Gu[i].SetActive(false);
                    }
                    else
                    {
                        Gu[i].transform.position = pos;
                        Gu[i].SetActive(true);
                        i = 10;
                    }
                }

                break;

        }
    }
}

