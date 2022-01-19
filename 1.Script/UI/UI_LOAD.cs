using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_LOAD : UIKey
{
    [SerializeField] private GameObject LoadPanel;
    [SerializeField] private GameObject StatusPanel;
    public UI_LOAD instance;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            if (SceneManager.GetActiveScene().name == "Title")
            {
                StartCoroutine(LoadCo());
            }
            else
            {
                Debug.Log("로드시도");
                LoadPanel.SetActive(false);
                StatusPanel.SetActive(true);
            }
        }
    }
    IEnumerator LoadCo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Forge",LoadSceneMode.Single);
        while (!operation.isDone)
        {
            yield return null;
        }
        SaveNLoad.instance.LoadData();
        //SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

}