using System.Collections;
using System;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Pointers;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    [Header("Events")]
    [Tooltip("Raised when Button Pressed")]
    public UnityEngine.Events.UnityEvent OnPressed = new UnityEngine.Events.UnityEvent();
 
    public GameObject obj;
    public GameObject ExplosionEffect;
    public GameObject FireEffect;
    public GameObject earth;
    public LineRenderer LaserPrefab;
    private AudioSource Gunfire;
    private AudioSource Explosion;

    [Header("Laser Details")]
    public float MaxLaserCharge;
    public AnimationCurve LaserDrainSpeedCurve;
    public float LaserChargeSpeed;
    public Color ChargedColor;
    public Color DrainedColor;
    public float BeamChargedWidth;
    public float BeamDrainedWidth;

    private float _currentLaserCharge;
    private IVRInputDevice _inputDevice;
    private IVRPointer _pointer;
    private LineRenderer _laserRend;
    private Material _laserMaterial;
    private float _normalisedCharge => _currentLaserCharge / MaxLaserCharge;

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

        _laserRend = Instantiate(LaserPrefab, new Vector3(0, 0, 0), new Quaternion());
        _laserMaterial = _laserRend.material;
        _laserMaterial.color = ChargedColor;

        _inputDevice = VRDevice.Device.PrimaryInputDevice;
        _pointer = VRDevice.Device.PrimaryInputDevice.Pointer;
        _currentLaserCharge = MaxLaserCharge;

    }

    private void Update()
    {
        this.gameObject.transform.position = obj.transform.position;
        this.gameObject.transform.rotation = obj.transform.rotation;

        //FireGun();

        FireLaser();
    }

    private void FireLaser()
    {
        if (_pointer == null)
            return;

        if (_inputDevice.GetButton(VRButton.One) && _currentLaserCharge > 0f)
        {
            _currentLaserCharge = Mathf.Clamp(_currentLaserCharge - (Time.deltaTime * LaserDrainSpeedCurve.Evaluate(_normalisedCharge)), 0, MaxLaserCharge);

            LaserRaycast();
            UpdateLaserVisual();
        }
        else
        {
            _laserRend.SetPosition(0, _pointer.Transform.position);
            _laserRend.SetPosition(1, _pointer.Transform.position);
        }

        if (!_inputDevice.GetButton(VRButton.One))
        {
            _currentLaserCharge = Mathf.Clamp(_currentLaserCharge + (Time.deltaTime * LaserChargeSpeed), 0, MaxLaserCharge);
        }

        print(_currentLaserCharge);
    }

    private void LaserRaycast()
    {
        if (!Physics.SphereCast(_pointer.Transform.position, 2f, _pointer.Transform.forward, out var hit,
            Mathf.Infinity))
            return;

        _laserRend.SetPosition(1, hit.point);

        var killableObject = hit.collider.GetComponent<IKillable>();

        Gunfire.Play();

        if (killableObject != null)
        {
            killableObject.Kill();

            PlayerEffect(0, hit.transform.position);
            Explosion.Play();

            // Enemy Count ///////////////////////
            enemyCount++;
            textBox.text = enemyCount.ToString();
        }
    }

    private void UpdateLaserVisual()
    {
        if (_laserMaterial == null)
            return;

        _laserMaterial.color = Color.Lerp(DrainedColor, ChargedColor, _normalisedCharge);
        _laserRend.widthMultiplier = Mathf.Lerp(BeamDrainedWidth, BeamChargedWidth, _normalisedCharge);
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

