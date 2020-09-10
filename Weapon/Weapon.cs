using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    
    public enum state
    {
        uzing,
        reloading,
        ready,
        empty
    }
    public state currentState;
    
    
    public enum weaponType
    {
        coldWeapon,
        Ranged,
        catapult
    }
    public weaponType type;
    
    //Shooting spesial ammo, and it's creation pos
    public GameObject pos;
    public GameObject ammoObj;

    //parameters
    public float shotsPerSecond;
    public float ammo;
    public float currentAmmo;
    public int damage;

    //methods
    public abstract void Shoot();
    public abstract void Reload();
    public abstract void DropMagazine();

}
