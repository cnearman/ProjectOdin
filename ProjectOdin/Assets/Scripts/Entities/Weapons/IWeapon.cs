using UnityEngine;
using System.Collections;

public interface IWeapon {

    GunPriority GunPriority { get;}

    void Fire();

    void Fire(Vector3 startingPosition, Quaternion direction);
}
