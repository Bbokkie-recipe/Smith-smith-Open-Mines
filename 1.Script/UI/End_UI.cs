using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_UI : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private GameObject gameTable;
    private void Start()
    {
        gameTable.transform.GetChild(0).gameObject.SetActive(false);
        gameTable.transform.GetChild(1).gameObject.SetActive(false);
        gameTable.transform.GetChild(2).gameObject.SetActive(false);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            GoEnding();
        }
    }

    public void GoEnding()
    {
        int openedMines = SaveNLoad.instance.saveData.playerMineLv;
        int finalFame = SaveNLoad.instance.saveData.playerFame;
        if(openedMines!=6) SceneManager.LoadScene("BadEnd");
        else if(finalFame < 4000) SceneManager.LoadScene("NormalEnd");
        else if(finalFame > 4000) SceneManager.LoadScene("SmithEnd");
    }
}
