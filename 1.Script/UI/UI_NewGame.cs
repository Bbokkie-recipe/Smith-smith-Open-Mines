using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_NewGame : UIKey
{
    public string sceneName;
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            StartCoroutine(Vanish());
            Debug.Log("새게임");
            //기존코드
            //SceneManager.LoadScene("Forge");
            //오프닝씬 추가
            SceneManager.LoadScene(sceneName);
        }
    }
    public IEnumerator Vanish()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

}
