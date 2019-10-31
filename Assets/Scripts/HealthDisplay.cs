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

    private float _normalisedHealth;

    // Update is called once per frame
    void Update()
    {
        if (Health == null)
            return;

        _normalisedHealth = Health.NormalisedHealth;

        HexGridFill.fillAmount = _normalisedHealth;
        ColoredFill.fillAmount = _normalisedHealth;

        ColoredFill.color = HealthFillGradient.Evaluate(_normalisedHealth);
    }
}
