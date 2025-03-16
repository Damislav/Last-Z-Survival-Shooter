using System;
using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float destroyAfterTime;

    private void Start()
    {
        StartCoroutine(ProjectileDeath());
    }

    IEnumerator ProjectileDeath()
    {
        yield return new WaitForSeconds(destroyAfterTime);
        Destroy(this.gameObject);
    }
}
