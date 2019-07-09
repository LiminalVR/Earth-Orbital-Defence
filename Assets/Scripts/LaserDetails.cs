using System;
using System.Collections;

using UnityEngine;

/// <summary>
/// Laser is used by <see cref="Fire"/> to store details about the player's laser.
/// </summary>
[Serializable]
public class LaserDetails
{
    public float MaxLaserCharge;
    public AnimationCurve LaserChargeSpeed;
    public AnimationCurve LaserDrainSpeedCurve;
    public Color ChargedColor;
    public Color DrainedColor;
    public float BeamChargedWidth;
    public float BeamDrainedWidth;
    public float LaserCooldownTime;
    public float LaserRadius;
    public float CurrentLaserCharge;
    public float NormalisedCharge => CurrentLaserCharge / MaxLaserCharge;
    public bool CanFire;

    public void DrainEnergy()
    {
        CurrentLaserCharge = Mathf.Clamp(CurrentLaserCharge - (LaserDrainSpeedCurve.Evaluate(NormalisedCharge)), 0, MaxLaserCharge);
    }

    public void ChargeLaser()
    {
        CurrentLaserCharge = Mathf.Clamp(CurrentLaserCharge + (Time.deltaTime * LaserChargeSpeed.Evaluate(NormalisedCharge)), 0, MaxLaserCharge);
    }

    public IEnumerator FireCooldownCoro(LineRenderer lineRend, Vector3 pointerPos)
    {
        yield return new WaitForSeconds(LaserCooldownTime / 2f);

        lineRend.SetPosition(0, pointerPos);
        lineRend.SetPosition(1, pointerPos);
        yield return new WaitForSeconds(LaserCooldownTime / 2f);

        CanFire = true;
    }
}
