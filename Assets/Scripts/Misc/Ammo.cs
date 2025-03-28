using System;
using UnityEngine;


public class Ammo : MonoBehaviour
{
    public float lifeTime = 2f;


    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Die), lifeTime);
    }


    private void Die()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }


}