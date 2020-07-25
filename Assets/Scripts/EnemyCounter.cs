using System.Collections;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{


    [SerializeField] SceneController sceneController;
    [SerializeField] int enemyCount;
    [SerializeField] RevealText revealText;
    [SerializeField] RevealText hideText;

    [SerializeField] Canvas sceneEndCanvas;

    public float FadeSpeed = 17.0F;
    public int RolloverCharacterSpread = 10;
    public Color ColorTint;

    bool rollingShouldStart = true;

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

    IEnumerator LoadSceneEndCanvas()
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
