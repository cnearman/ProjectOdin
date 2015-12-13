using System;
using UnityEngine;

public enum GunPriority
{
    Primary = 0,
    Secondary = 1
}

public class Gun : BaseClass, IWeapon
{
    public GameObject Distribution;

    public GameObject Owner { get; set; }

    private float RefireTime;
    public float FireRate;
    public string Projectile;

    public GunPriority Priority;

    public GunPriority GunPriority
    {
        get
        {
            return Priority;
        }
    }

    public bool CanFire
    {
        get
        {
            return RefireTime <= 0;
        }
    }

    public void Fire()
    {
        GameObject newProjectile = PhotonNetwork.Instantiate(Projectile, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), 0);
        TriggerOnCollision trigger = newProjectile.GetComponent<TriggerOnCollision>();
        trigger.Owner = this.Owner;
    }

    public void Fire(Vector3 startingPosition, Quaternion direction)
    {
        if (CanFire)
        {
            GameObject newProjectile = PhotonNetwork.Instantiate(Projectile, startingPosition, direction, 0);
            TriggerOnCollision trigger = newProjectile.GetComponent<TriggerOnCollision>();
            trigger.Owner = this.Owner;
            AddCoolDown(FireRate);
        }
    }

    public void AddCoolDown(float time)
    {
        RefireTime += time;
    }

    void Update()
    {
        if (RefireTime > 0)
        {
            RefireTime -= Time.deltaTime;
        }
    }
}

