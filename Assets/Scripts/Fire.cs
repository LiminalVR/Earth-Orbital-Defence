using System.Collections;
using System;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Pointers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public GameObject obj;
    public GameObject ExplosionEffect;
    public GameObject FireEffect;
    public GameObject earth;
    
    private AudioSource Gunfire;
    private AudioSource Explosion;

    [Header("Laser Details")]
    public GameObject CannonObject;
    public LaserDetails PlayerLaserDetails;
    public Reticule TargetingReticule;
    public float ReticuleFillSpeed;
    public int GetShotsFired() 
        => _shotsFired;
    public int GetEnemiesKilled()
        => _enemiesKilled;

    private int _shotsFired;
    private int _enemiesKilled;
    private IVRInputDevice _inputDevice;
    private IVRPointer _pointer;
    private LineRenderer _laserRend;
    private Material _laserMaterial;
    private Coroutine EnergyRefillRoutine;
    private GameObject[] Ex;
    private GameObject[] Gu;

    // Enemy Count //////////////
    public Text textBox;
    private int enemyCount = 0;
    ////////////////////////////

    private void OnValidate()
    {
        Assert.IsNotNull(TargetingReticule, "TargetingReticule must not be null.");
    }

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

        _laserRend = Instantiate(PlayerLaserDetails.LaserPrefab, new Vector3(0, 0, 0), new Quaternion());
        _laserMaterial = _laserRend.material;
        _laserMaterial.color = PlayerLaserDetails.ChargedColor;

        _inputDevice = VRDevice.Device.PrimaryInputDevice;
        _pointer = VRDevice.Device.PrimaryInputDevice.Pointer;

        PlayerLaserDetails.CurrentLaserCharge = PlayerLaserDetails.MaxLaserCharge;
        TargetingReticule.FillSpeed = ReticuleFillSpeed;
    }

    private void Update()
    {
        this.gameObject.transform.position = obj.transform.position;
        this.gameObject.transform.rotation = obj.transform.rotation;

        FireLaser();

        CannonObject.transform.rotation = _pointer.Transform.rotation;
    }

    private void FireLaser()
    {
        if (_pointer == null)
            return;

        if (_inputDevice.GetButton(VRButton.One) && PlayerLaserDetails.CurrentLaserCharge > 0f && PlayerLaserDetails.CanFire)
        {
            PlayerLaserDetails.CanFire = false;
            PlayerLaserDetails.DrainEnergy();

            LaserRaycast();
            UpdateLaserVisual();

            if(!Gunfire.isPlaying)
            {
                Gunfire.Play();
            }

            StartCoroutine(PlayerLaserDetails.FireCooldownCoro(_laserRend, _pointer.Transform.position));

            _shotsFired++;
        }

        if (!_inputDevice.GetButton(VRButton.One))
        {
            PlayerLaserDetails.ChargeLaser();
        }
        else if (!_inputDevice.GetButton(VRButton.One) || PlayerLaserDetails.CurrentLaserCharge <= 0f)
        {
            if (EnergyRefillRoutine != null)
                return;

            EnergyRefillRoutine = StartCoroutine(FreeEnergyCoro(PlayerLaserDetails.LaserCooldownTime * 2));
        }
        else
        {
            if (EnergyRefillRoutine != null)
                StopCoroutine(EnergyRefillRoutine);
        }

        TargetingReticule.SetTargetFillAmount(PlayerLaserDetails.NormalisedCharge);
    }

    private IEnumerator FreeEnergyCoro(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        PlayerLaserDetails.CurrentLaserCharge += PlayerLaserDetails.LaserDrainSpeedCurve.Evaluate(PlayerLaserDetails.NormalisedCharge);
        EnergyRefillRoutine = null;
    }

    private void LaserRaycast()
    {
        if (!Physics.SphereCast(_pointer.Transform.position, PlayerLaserDetails.LaserRadius, _pointer.Transform.forward, out var hit,
            Mathf.Infinity))
            return;

        _laserRend.SetPosition(1, hit.point);

        var killableObject = hit.collider.GetComponent<IKillable>();

        if (killableObject == null)
            return;

        killableObject.Kill();

        PlayerEffect(0, hit.transform.position);
        Explosion.Play();

        _enemiesKilled++;
        
        // Enemy Count ///////////////////////
        enemyCount++;
        textBox.text = enemyCount.ToString();
    }

    private void UpdateLaserVisual()
    {
        if (_laserMaterial == null)
            return;

        _laserMaterial.color = Color.Lerp(PlayerLaserDetails.DrainedColor, PlayerLaserDetails.ChargedColor, PlayerLaserDetails.NormalisedCharge);
        _laserRend.widthMultiplier = Mathf.Lerp(PlayerLaserDetails.BeamDrainedWidth, PlayerLaserDetails.BeamChargedWidth, PlayerLaserDetails.NormalisedCharge);
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

