using System.Collections;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Pointers;
using UnityEngine;
using UnityEngine.Assertions;

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
    public Laser playerLaser;
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
    private Coroutine EnergyRefillRoutine;
    private GameObject[] Ex;
    private GameObject[] Gu;

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

        playerLaser.LaserRend = Instantiate(playerLaser.LaserPrefab, new Vector3(0, 0, 0), new Quaternion());
        playerLaser.SetLaserVisuals();

        _inputDevice = VRDevice.Device.PrimaryInputDevice;
        _pointer = VRDevice.Device.PrimaryInputDevice.Pointer;

        playerLaser.CurrentLaserCharge = playerLaser.MaxLaserCharge;
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

        if (_inputDevice.GetButton(VRButton.One) && playerLaser.CurrentLaserCharge > 0f && playerLaser.CanFire)
        {
            playerLaser.CanFire = false;
            playerLaser.DrainEnergy();

            LaserRaycast();
            playerLaser.UpdateLaserVisual();

            if(!Gunfire.isPlaying)
            {
                Gunfire.Play();
            }

            StartCoroutine(playerLaser.FireCooldownCoro(_pointer.Transform.position));

            _shotsFired++;
        }

        if (!_inputDevice.GetButton(VRButton.One))
        {
            playerLaser.ChargeLaser();
        }
        else if (_inputDevice.GetButton(VRButton.One) && playerLaser.CurrentLaserCharge <= 0f)
        {
            if (EnergyRefillRoutine != null)
                return;

            EnergyRefillRoutine = StartCoroutine(FreeEnergyCoro(playerLaser.LaserCooldownTime * 2));
        }
        else
        {
            if (EnergyRefillRoutine != null)
                StopCoroutine(EnergyRefillRoutine);
        }

        TargetingReticule.SetTargetFillAmount(playerLaser.NormalisedCharge);
    }

    private IEnumerator FreeEnergyCoro(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);

        playerLaser.CurrentLaserCharge += playerLaser.LaserDrainSpeedCurve.Evaluate(playerLaser.NormalisedCharge);
        EnergyRefillRoutine = null;
    }

    private void LaserRaycast()
    {
        if (!Physics.SphereCast(_pointer.Transform.position, playerLaser.LaserRadius, _pointer.Transform.forward, out var hit,
            Mathf.Infinity))
            return;

        playerLaser.LaserRend.SetPosition(1, hit.point);

        var killableObject = hit.collider.GetComponent<IKillable>();

        if (killableObject == null)
            return;

        killableObject.Kill();

        PlayerEffect(0, hit.transform.position);
        Explosion.Play();

        _enemiesKilled++;
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

