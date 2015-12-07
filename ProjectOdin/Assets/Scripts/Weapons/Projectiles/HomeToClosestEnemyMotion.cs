using UnityEngine;

public class HomeToClosestEnemyMotion : BaseClass, IProjectileMotion
{
    //for homing effects
    public bool homing;
    public float homeDistance; //distance to see target and start homing
    public float homeAngle; //angle to see target and start homing (cone in front)
    public float closeAngleFixed; //the rate at witch the projectile can turn
    public GameObject closest; //don't change, this is just here for testing
    int wallMask = 1 << 12; //this mask is used in the raycast that checks if the projectile
                            //can see the target
    public string enemyTag;

    public float Velocity { get; set; }

    void Update()
    {

        transform.Translate(Vector2.up * 1.0f * Time.deltaTime * Velocity);

        GameObject target = FindClosestEnemy();
        if (target != null)
        {
            Vector2 targetDir = target.transform.position - transform.position;
            Vector2 forward = Vector2.up;
            float angle = Vector3.Angle(targetDir, forward);
            Vector3 angle2 = Vector3.Cross(targetDir, forward);


            if (angle2.z > 0f)
            {
                angle = 360f - angle;
            }

            float closeAngle = closeAngleFixed * Time.deltaTime;

            if (Mathf.Abs(transform.localEulerAngles.z - angle) > 180f)
            {
                if (transform.localEulerAngles.z > angle)
                {
                    if ((transform.localEulerAngles.z - angle) < closeAngle)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + closeAngle);
                    }
                }
                else
                {
                    if ((angle - transform.localEulerAngles.z) < closeAngle)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - closeAngle);
                    }
                }
            }
            else
            {
                if (transform.localEulerAngles.z > angle)
                {
                    if ((transform.localEulerAngles.z - angle) < closeAngle)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - closeAngle);
                    }
                }
                else
                {
                    if ((angle - transform.localEulerAngles.z) < closeAngle)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + closeAngle);
                    }
                }
            }
        }
    }


    //finds the closest enemy the projectile can see
    private GameObject FindClosestEnemy()
    {
        // Find all game objects tagged to be enemies
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(enemyTag);
        float distance = Mathf.Infinity;
        closest = null;


        // Iterate through them and find the closest one
        foreach (GameObject go in gos)
        {
            //Fist check if its in the wedge where the projectile can see
            bool inSight = false;
            Vector2 targetDir = go.transform.position - transform.position;
            Vector2 forward = transform.up;
            float angle = Vector3.Angle(targetDir, forward);


            if (Mathf.Abs(angle) < homeAngle)
            {
                inSight = true;
            }

            //now check to see if there is anything in the way
            Vector3 diff = (go.transform.position - transform.position);
            float curDistance = diff.magnitude;
            bool clear = true;

            if (Physics2D.Raycast(transform.position, go.transform.position - transform.position, curDistance, wallMask))
            {
                clear = false;
            }

            //if the current distance is less than the distance the projectile can see and the sightlines
            //are clear then we see if its a better option than the one we already have.
            if (curDistance < distance && curDistance < homeDistance && clear && inSight)
            {
                closest = go;
                distance = curDistance;
            }

        }

        return closest;
    }

}
