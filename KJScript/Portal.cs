using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    float time = 0f;
    public Image endPanel;
    float fadeTime = 2f;

    public int monsterCount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (monsterCount == 0)
            {
                StartCoroutine(FadeIn());
            }
        }
    }
    IEnumerator FadeIn()
    {
        Color alpha = endPanel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            endPanel.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Forge");

        yield return null;
    }
}
