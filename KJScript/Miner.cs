using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public int hitCount;

    public GameObject minePiece;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            hitCount++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(hitCount>=5)
        {
            Destroy(gameObject);
            Instantiate(minePiece, transform.position, transform.rotation);
            Instantiate(minePiece, transform.position, transform.rotation);
        }
    }
}
