using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ElementBall : MonoBehaviour
{
    private ElementBag _elementBag;

    public new Rigidbody rigidbody;


    [SerializeField] private ParticleSystem targetParticleSystem;


    public Dictionary<ElementType, float> DamageBookInTheBall { get; private set; }


    public ElementType CombinedMagicType { get; private set; }

    public float CombinedMagicDamage { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        _elementBag = FindObjectOfType<ElementBag>();
        gameObject.name = Time.time.ToString();
        DamageBookInTheBall = new Dictionary<ElementType, float>(_elementBag.DamageBook);
        CombinedMagicType = DamageBookInTheBall.First().Key;
        CombinedMagicDamage = DamageBookInTheBall.First().Value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer") || other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}