using UnityEngine;

public class ElementSlot : MonoBehaviour
{
    [SerializeField] private ElementType magic;
    [SerializeField] private ParticleSystem[] magicParticles;

    public ElementType Element
    {
        get => magic;
        set => magic = value;
    }

    private void Update()
    {
        //print(magic.ToString());
        foreach (var particle in magicParticles)
            //print(particle.name);
            if (particle.name == magic.ToString())
                particle.gameObject.SetActive(true);
            else
                particle.gameObject.SetActive(false);
    }
}