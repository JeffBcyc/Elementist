using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSlot : MonoBehaviour
{

    [SerializeField] MagicType magic;

    public MagicType GetMagicType()
    {
        return magic;
    }

}
