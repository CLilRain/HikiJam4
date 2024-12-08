using System.ComponentModel;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Transform handRoot;

    public Transform targetRotateReference;
    public float rotationSpeed;

    Weapon activeWeapon;

    public Weapon ActiveWeapon => activeWeapon;

    Quaternion strikeRot;
    Quaternion originalRot;
    Quaternion targetRotation;

    void Awake()
    {
        originalRot = handRoot.localRotation;
        strikeRot = targetRotateReference.rotation;

        targetRotation = originalRot;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetRotation = strikeRot;
            if (activeWeapon != null) 
            {
                activeWeapon.Strike();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            targetRotation = originalRot;
            if (activeWeapon != null)
            {
                activeWeapon.Sheeth();
            }
        }
    }

    void FixedUpdate()
    {
        handRoot.localRotation = Quaternion.Lerp(handRoot.localRotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public void AssignWeapon(Weapon weapon)
    {
        if (activeWeapon != null)
        {
            // 
        }

        activeWeapon = weapon;

        weapon.transform.parent = handRoot;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
}