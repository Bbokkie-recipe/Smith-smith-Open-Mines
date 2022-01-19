using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMapNot : MonoBehaviour
{
    //이동할 씬의 이름
    public string transferSceneName;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerFinger")
        {
            SceneManager.LoadScene(transferSceneName);
        }
    }
}
