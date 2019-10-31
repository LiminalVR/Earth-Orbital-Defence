using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Health Health;
    public Image HexGridFill;
    public Image ColoredFill;
    public Gradient HealthFillGradient;
    public MeshRenderer EarthMeshRenderer;

    private float _normalisedHealth;
    private Material _earthMat;
    private static readonly int HealthRemaining = Shader.PropertyToID("_HealthRemaining");

    private void Start()
    {
        _earthMat = EarthMeshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health == null)
            return;

        _normalisedHealth = Health.NormalisedHealth;

        HexGridFill.fillAmount = _normalisedHealth;
        ColoredFill.fillAmount = _normalisedHealth;

        ColoredFill.color = HealthFillGradient.Evaluate(_normalisedHealth);

        if (_earthMat == null)
            return;

        _earthMat.SetFloat(HealthRemaining, _normalisedHealth);
    }
}
