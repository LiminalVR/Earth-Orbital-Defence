using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamagable
{
    public MeshRenderer ShieldRenderer;
    public float StartingPulseStrength;
    public float PulseLength;
    public Vector2 offsetDelta;

    private float _pulseStrength;
    private Coroutine _pulseRoutine;
    private static readonly int PulseStrength = Shader.PropertyToID("_PulseStrength");
    private static readonly int HitOffset = Shader.PropertyToID("_HitOffset");
    private static readonly int HitPoint = Shader.PropertyToID("_HitPoint");
    private static readonly int PulseProgress = Shader.PropertyToID("_PulseProgress");

    public void Damage(int damageToTake, GameObject origin = null)
    {
        if (ShieldRenderer == null)
            return;

        if(_pulseRoutine !=null)
            StopCoroutine(_pulseRoutine);

        var localHitPoint = ShieldRenderer.transform.InverseTransformPoint(origin.transform.position);

        _pulseRoutine = StartCoroutine(PulseCoro(localHitPoint));
    }

    private IEnumerator PulseCoro(Vector3 hitPoint)
    {
        var elapsedTime = 0f;
        var pulseDecayRate = StartingPulseStrength / PulseLength;
        var pulseProgress = 0f;
        var hitOffset = ShieldRenderer.material.GetVector(HitOffset);

    _pulseStrength = StartingPulseStrength;

        while (elapsedTime < PulseLength)
        {
            yield return new WaitForEndOfFrame();
            ShieldRenderer.material.SetVector(HitPoint, hitPoint);
            ShieldRenderer.material.SetFloat(PulseStrength, _pulseStrength);
            ShieldRenderer.material.SetFloat(PulseProgress, pulseProgress);
            ShieldRenderer.material.SetVector(HitOffset, hitOffset);

            hitOffset.x += Time.deltaTime * offsetDelta.x;
            hitOffset.y += Time.deltaTime * offsetDelta.y;
            elapsedTime += Time.deltaTime;
            _pulseStrength -= Time.deltaTime * pulseDecayRate;
            pulseProgress = elapsedTime / PulseLength;
        }

        _pulseStrength = 0f;
        _pulseRoutine = null;

        yield return new WaitForEndOfFrame();
    }
}
