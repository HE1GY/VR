using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivate : MonoBehaviour
{
    [SerializeField] private GameObject _lazerBlade;

    public void ShootGun()
    {
        print("piv piv");
    }

    public void OnLazer()
    {
        _lazerBlade.SetActive(true);
    }
    public void OffLazer()
    {
        _lazerBlade.SetActive(false);
    }
}
