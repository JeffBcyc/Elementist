using UnityEngine;

public class ElementSlot : MonoBehaviour
{

    [SerializeField] ElementType magic;
    [SerializeField] ParticleSystem[] magicParticles;

    public ElementType Element
    {
        get { return magic; }
        set { magic = value; }
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
            }
            else
            {
                particle.gameObject.SetActive(false);
            }
        }
    }


}
