using UnityEngine;
using UnityEngine.UI;

public class Reticule : MonoBehaviour
{
    public Transform Crosshairs;
    public Button button;
    public Image EnergyFill;
    public float FillSpeed;

    private float _targetFillValue;
    private bool start = false;
    private void Start()
    {
        button.onClick.AddListener(StartG);
        SetCrosshairVisibility(false);
        EnergyFill.fillAmount = 1;
    }


    private void FixedUpdate()
    {
        var VRDevice = Liminal.SDK.VR.VRDevice.Device;
        var pointer = VRDevice.PrimaryInputDevice.Pointer;


        if (start == true)
            Crosshairs.SetPositionAndRotation(pointer.CurrentRaycastResult.worldPosition, pointer.Transform.rotation);
    }

    void StartG()
    {
        if (start == false)
        {
            start = true;
            button.gameObject.SetActive(false);
            SetCrosshairVisibility(true);
            return;
        }
        if (start == true)
        {
            start = false;
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



