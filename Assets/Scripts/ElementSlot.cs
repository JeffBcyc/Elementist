using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSlot : MonoBehaviour
{

    [SerializeField] MagicType magic;
    [SerializeField] ParticleSystem[] magicParticles;


    public MagicType GetMagicType()
    {
        return magic;
    }

    private void Start()
    {
        //print(magic.ToString());
    }

    private void Update()
    {

        //print(magic.ToString());
        foreach (ParticleSystem particle in magicParticles)
        {
            //print(particle.name);
            if (particle.name == magic.ToString())
            {
                particle.gameObject.SetActive(true);
            } else
            {
                particle.gameObject.SetActive(false);
            }
        }
    }
}
