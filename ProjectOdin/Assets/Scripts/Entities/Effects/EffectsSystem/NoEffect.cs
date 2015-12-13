using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class NoEffect : BaseClass, Effect
{
    public MonoBehaviour Owner
    {
        get
        {
            return null;
        }

        set
        {
            
        }
    }

    public void ApplyEffect(GameObject target)
    {
        throw new NotImplementedException();
    }
}
