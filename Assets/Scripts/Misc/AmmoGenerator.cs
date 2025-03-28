using UnityEngine;

public class AmmoGenerator : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            AmmoManager.SpawnAmmo(transform.position, transform.rotation);
        }
    }
}