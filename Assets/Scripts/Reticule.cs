using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Reticule : MonoBehaviour
{
    public Transform Crosshairs;
    public Button Button;
    public Image EnergyFill;
    public float FillSpeed;

    private float _targetFillValue;
    private bool _start = false;

    private void Start()
    {
        Button.onClick.AddListener(StartG);
        SetCrosshairVisibility(false);
        EnergyFill.fillAmount = 1;
    }


    private void FixedUpdate()
    {
        var VRDevice = Liminal.SDK.VR.VRDevice.Device;
        var pointer = VRDevice.PrimaryInputDevice.Pointer;

        if (_start == true)
            Crosshairs.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);
    }

    void StartG()
    {
        if (_start == false)
        {
            _start = true;
            Button.gameObject.SetActive(false);
            SetCrosshairVisibility(true);
            return;
        }

        if (_start == true)
        {
            _start = false;
            return;
        }
    }

    private void Update()
    {
        EnergyFill.fillAmount = Mathf.MoveTowards(EnergyFill.fillAmount, _targetFillValue, FillSpeed * Time.deltaTime);
    }

    public void SetTargetFillAmount(float targetValue)
    {
        _targetFillValue = targetValue;
    }

    public void SetCrosshairVisibility(bool state)
    {
        Crosshairs.gameObject.SetActive(state);
    }
}
