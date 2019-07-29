using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Laser is used by <see cref="Fire"/> to store details about the player's laser, as well as set the laser's visuals, and set the amount of energy is available to use.
/// </summary>
[Serializable]
public class Laser
{
    public LineRenderer LaserPrefab;
    public LineRenderer LaserRend;
    public float BeamChargedWidth;
    public float BeamDrainedWidth;
    public float LaserCooldownTime;
    public float LaserRadius;
    public float CurrentLaserCharge;
    public float MaxLaserCharge;
    public AnimationCurve LaserChargeSpeed;
    public AnimationCurve LaserDrainSpeedCurve;
    public Color ChargedColor;
    public Color DrainedColor;
    
    public float NormalisedCharge => CurrentLaserCharge / MaxLaserCharge;
    public bool CanFire;

    private Material _laserMaterial;

    public void SetLaserVisuals()
    {
        _laserMaterial = LaserRend.material;
        _laserMaterial.color = ChargedColor;
    }

    public void DrainEnergy()
    {
        CurrentLaserCharge = Mathf.Clamp(CurrentLaserCharge - (LaserDrainSpeedCurve.Evaluate(NormalisedCharge)), 0, MaxLaserCharge);
    }

    public void ChargeLaser()
    {
        CurrentLaserCharge = Mathf.Clamp(CurrentLaserCharge + (Time.deltaTime * LaserChargeSpeed.Evaluate(NormalisedCharge)), 0, MaxLaserCharge);
    }

    public void UpdateLaserVisual()
    {
        if (_laserMaterial == null)
            return;

        _laserMaterial.color = Color.Lerp(DrainedColor, ChargedColor, NormalisedCharge);
        LaserRend.widthMultiplier = Mathf.Lerp(BeamDrainedWidth, BeamChargedWidth, NormalisedCharge);
    }

    public IEnumerator FireCooldownCoro(Vector3 pointerPos)
    {
        yield return new WaitForSeconds(LaserCooldownTime / 2f);

        LaserRend.SetPosition(0, pointerPos);
        LaserRend.SetPosition(1, pointerPos);
        yield return new WaitForSeconds(LaserCooldownTime / 2f);

        CanFire = true;
    }
}
