using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private ElementBall damage;
    [SerializeField] private ElementType dropElement;

    private ElementBag elementBag;
    [SerializeField] private ElementPickUp[] elementToSpawnAfterDeath;
    [SerializeField] private EnemyCounter enemyCounter;

    [SerializeField] private float enemyHealth = 2;
    [SerializeField] private EnemyStatus enemyStatus;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TextMesh textMesh;

    public float CurrentHealth => enemyHealth;


    private void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        enemyStatus = EnemyStatus.Nothing;
        elementBag = FindObjectOfType<ElementBag>();
        healthBar.SetMaxHealth(enemyHealth);
        textMesh.text = "";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBall") ProcessDamage(other);
    }


    private void ProcessDamage(Collider other)
    {
        damage = other.GetComponent<ElementBall>();
        var _modifiedDamage = ModifyDamageByStatus(damage.CombinedMagicDamage);
        enemyHealth -= _modifiedDamage;
        healthBar.SetHealth(enemyHealth);

        if (enemyHealth <= 0)
        {
            SpawnElement();
            enemyCounter.EnemyCountDecrease();
            Destroy(gameObject);
        }
        else
        {
            enemyStatus = ChangeEnemyStatus(damage.CombinedMagicType);
            textMesh.text = enemyStatus.ToString();
        }
    }

    private float ModifyDamageByStatus(float rawDamage)
    {
        var modifiedDamage = rawDamage;

        if (enemyStatus == EnemyStatus.Electrified)
            switch (damage.CombinedMagicType)
            {
                case ElementType.Ice:
                    modifiedDamage = damage.CombinedMagicDamage * 2;
                    break;
                case ElementType.Fire:
                    modifiedDamage = damage.CombinedMagicDamage * 2;
                    break;
            }
        else if (enemyStatus == EnemyStatus.Burning)
            switch (damage.CombinedMagicType)
            {
                case ElementType.Electric:
                    modifiedDamage = damage.CombinedMagicDamage / 2;
                    break;
                case ElementType.Ice:
                    modifiedDamage = damage.CombinedMagicDamage / 2;
                    break;
            }
        else if (enemyStatus == EnemyStatus.Frozen)
            switch (damage.CombinedMagicType)
            {
                case ElementType.Fire:
                    modifiedDamage = damage.CombinedMagicDamage / 2;
                    break;
                case ElementType.Electric:
                    modifiedDamage = damage.CombinedMagicDamage * 2;
                    break;
            }

        return modifiedDamage;
    }

    private EnemyStatus ChangeEnemyStatus(ElementType combinedMagicType)
    {
        EnemyStatus _newstatus;
        if (combinedMagicType == ElementType.Electric) _newstatus = EnemyStatus.Electrified;
        else if (combinedMagicType == ElementType.Fire) _newstatus = EnemyStatus.Burning;
        else if (combinedMagicType == ElementType.Ice) _newstatus = EnemyStatus.Frozen;
        else _newstatus = EnemyStatus.Nothing;
        return _newstatus;
    }

    private void SpawnElement()
    {
        foreach (var item in elementToSpawnAfterDeath)
            if (item.ElementFromThisBook == dropElement)
            {
                Instantiate(item, transform.position, Quaternion.identity);
                break;
            }
    }

    private enum EnemyStatus
    {
        Frozen,
        Burning,
        Electrified,
        Nothing
    }
}