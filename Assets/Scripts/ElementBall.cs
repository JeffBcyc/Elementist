using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementBall : MonoBehaviour
{
    private ElementBag elementBag;
    private Dictionary<ElementType, float> elementBagDamageBook;
    ElementType combinedMagic;
    float combinedMagicDamage;

    [SerializeField] ParticleSystem targetParticleSystem;

    private void Awake()
    {
        elementBag = FindObjectOfType<ElementBag>();
        gameObject.name = Time.time.ToString();
        elementBagDamageBook = new Dictionary<ElementType, float>(elementBag.DamageBook);
        combinedMagic = elementBagDamageBook.First().Key;
        combinedMagicDamage = elementBagDamageBook.First().Value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer" || other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    public Dictionary<ElementType, float> DamageBookInTheBall
    { get { return elementBagDamageBook; } }


    public ElementType CombinedMagicType
    {
        get { return combinedMagic; }
    }

    public float CombinedMagicDamage
    {
        get { return combinedMagicDamage; }
    }

}
