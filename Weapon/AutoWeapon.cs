using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : Weapon
{
    private bool shot;
    public float reloatTime;
    [SerializeField]private float reloat;
    public bool active;
    void Start()
    {
        InvokeRepeating(nameof(SetShot),0f,1f/shotsPerSecond);
    }

    void Update()
    {
        if (active)
            UpdateWeapon();
        if (!active && currentState == state.reloading)
            currentState = state.empty;
        CheckState();
    }
    private void UpdateWeapon()
    {

        if (Input.GetMouseButton(0) && shot && currentState == state.uzing)
            Shoot();
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmo != ammo)
            Reload();
    }
    private void CheckState()
    {
        if (currentState == state.empty)
            currentAmmo = 0;
        else if (active && currentState == state.ready)
            currentState = state.uzing;
        else if (!active)
            currentState = state.ready;
    }

    public void SetShot()
    { shot = true; }
    

    public override void Reload()
    {
        if (currentState == state.uzing || currentState == state.empty)
            StartCoroutine(ReloadGun());
    }

    private IEnumerator ReloadGun()
    {
        currentState = state.reloading;
        yield return new WaitForSeconds(reloat);
        currentState = state.ready;
        if (active)
        {
            currentAmmo = ammo;
        }
        else
        {
            currentState = state.empty;
            currentAmmo = 0;
        }
    }
    public override void DropMagazine()
    {
        //Instantiate
    }

    public override void Shoot()
    {
        if(currentAmmo > 0 && currentState == state.uzing)
            Fire();
        

    }
    private void Fire()
    {
        shot = false;
        currentAmmo--;
        RaycastHit hit;
        Physics.Raycast(pos.transform.position, pos.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

        try
        {
            if (hit.collider.gameObject.GetComponent<PlayerParams>())
            {
                hit.collider.gameObject.GetComponent<PlayerParams>().TakeDamage(damage);
            }
        }
        catch
        {
            Debug.Log("Fuck");
        }
    }
}
