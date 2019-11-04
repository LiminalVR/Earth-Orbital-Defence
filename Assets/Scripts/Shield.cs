using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shield : MonoBehaviour, IDamagable
{
    public MeshRenderer ShieldRenderer;
    public float StartingPulseStrength;
    public float PulseLength;
    public Vector2 offsetDelta;
    public int HitsBeforeDisappearing;
    public float FadeSpeed;
    [GradientUsage(true)]
    public Gradient ShieldColorGradient;
    public float GradientFadeSpeed;

    private int _totalHits;
    private float _pulseStrength;
    private float _cachedGradientTime;
    private Coroutine _pulseRoutine;
    private Coroutine _fadeRoutine;
    private static readonly int PulseStrength = Shader.PropertyToID("_PulseStrength");
    private static readonly int HitOffset = Shader.PropertyToID("_HitOffset");
    private static readonly int HitPoint = Shader.PropertyToID("_HitPoint");
    private static readonly int PulseProgress = Shader.PropertyToID("_PulseProgress");
    private static readonly int ShieldOpacity = Shader.PropertyToID("_ShieldOpacity");
    private static readonly int ShieldColor = Shader.PropertyToID("_ShieldColor");

    public void Update()
    {
        if (ShieldRenderer == null)
            return;

        var gradientTime = (float) _totalHits / HitsBeforeDisappearing;

        if (Mathf.Approximately(_cachedGradientTime, gradientTime))
            return;

        _cachedGradientTime = Mathf.MoveTowards(_cachedGradientTime, gradientTime, GradientFadeSpeed * Time.deltaTime);

        var color = ShieldColorGradient.Evaluate(_cachedGradientTime);
        ShieldRenderer.material.SetColor(ShieldColor, color);
    }

    public void Damage(int damageToTake, GameObject origin = null)
    {
        if (ShieldRenderer == null)
            return;

        if (_fadeRoutine != null)
            return;

        _totalHits++;

        if (_pulseRoutine !=null)
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

        if (_totalHits >= HitsBeforeDisappearing)
            _fadeRoutine = StartCoroutine(ShieldFade());
    }

    private IEnumerator ShieldFade()
    {
        var shieldOpacity = 1f;

        while (shieldOpacity > 0f)
        {
            shieldOpacity -= Time.deltaTime * FadeSpeed;
            ShieldRenderer.material.SetFloat(ShieldOpacity, shieldOpacity);
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
        yield break;
    }
}
