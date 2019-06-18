using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    ObjectPooler objectPooler;

    public bool isEnemy;
    public bool isGameObject;
    public bool isSpecialEffect;

    [System.Serializable]
    public class Target
    {
        //public string targetTag;
        public GameObject targetPosition;
    }

    public Target[] targets;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }


    private void FixedUpdate()
    {
        var vrDevice = Liminal.SDK.VR.VRDevice.Device;
        var pointer = vrDevice.PrimaryInputDevice.Pointer;
        var input = vrDevice.PrimaryInputDevice;
        RaycastHit hit;

        if (input.GetButtonDown(Liminal.SDK.VR.Input.VRButton.One))
            if (Physics.Raycast(pointer.Transform.position, pointer.Transform.forward, out hit, Mathf.Infinity))
        {
            if (isSpecialEffect && hit.collider.CompareTag("Enemy"))
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    ObjectPooler.Instance.SpwanFromPool("Explossions",
                     hit.transform.position,
                     hit.transform.rotation);
                }
            }
        }
    }
}
