using System.Collections;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public Color ColorTint;
    [SerializeField] private int enemyCount;

    public float FadeSpeed = 17.0F;
    [SerializeField] private RevealText hideText;
    [SerializeField] private RevealText revealText;

    private bool rollingShouldStart = true;
    public int RolloverCharacterSpread = 10;


    [SerializeField] private SceneController sceneController;

    [SerializeField] private Canvas sceneEndCanvas;

    private void Awake()
    {
        sceneEndCanvas.gameObject.SetActive(false);

        //textToDisplayAfterClearingLevel = GetComponent<TMP_Text>();
        enemyCount = FindObjectsOfType<EnemyHealth>().Length;
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        if (enemyCount == 0 && rollingShouldStart)
        {
            revealText.gameObject.SetActive(true);
            revealText.StartRollingText();
            rollingShouldStart = false;
            StartCoroutine(LoadSceneEndCanvas());
        }
    }

    private IEnumerator LoadSceneEndCanvas()
    {
        hideText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        sceneEndCanvas.gameObject.SetActive(true);
    }

    public void EnemyCountDecrease()
    {
        enemyCount--;
    }
}