using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class OreBox : MonoBehaviour
{
    public Ore pooledObject;
    public int maxPoolSize;
    public int initialPoolSize;
    public int additionalPoolSize;
    public int currentOre;
    public int oreLevel;
    [SerializeField]
    public int oreInBox;
    public ObjectPoolBase<Ore> pool;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPoolBase<Ore>(this, pooledObject, maxPoolSize, 
            initialPoolSize, additionalPoolSize);
        oreInBox = 0;
        StartCoroutine(GetOre());
        TextUpdate();
    }
    void TextUpdate()
    {
        text.text = "이름: " + SaveNLoad.instance.oreName[oreLevel]+"\n"+
            "가치: "+ SaveNLoad.instance.oreValue[oreLevel] + "\n" +
            "갯수: " + (SaveNLoad.instance.saveData.currentOre[oreLevel]-currentOre+oreInBox);
    }
    IEnumerator GetOre()
    {
        while (true)
        {
            if (oreInBox < 3 && SaveNLoad.instance.saveData.currentOre[oreLevel] > currentOre)
            {
                GetObject();
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TextUpdate();
        if (other.tag == "Ore")
        {
            oreInBox -= 1;
            if (SaveNLoad.instance.saveData.currentOre[oreLevel] > currentOre)
            {
                Ore ore = GetObject().GetComponent<Ore>();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TextUpdate();
        if (other.tag == "Ore")
        {
            Ore ore = other.GetComponent<Ore>();
            if (oreInBox>3|| ore.oreLevel != oreLevel)
            {
                ore.oreBox.currentOre -= 1;
                ore.Return();
            }
            else
            {
                oreInBox +=1;
            }
        }
    }
    public GameObject GetObject()
    {
        currentOre += 1;
        Ore ore= pool.GetObject().GetComponent<Ore>();
        ore.oreLevel = oreLevel;
        ore.oreBox = this;
        ore.oreName = SaveNLoad.instance.oreName[oreLevel];
        ore.name = ore.oreName;
        ore.maxValue = SaveNLoad.instance.oreValue[oreLevel];
        ore.minValue = SaveNLoad.instance.oreValue[oreLevel];
        ore.color = SaveNLoad.instance.oreColor[oreLevel];
        ore.GetComponent<Renderer>().material.color = ore.color;
        return ore.gameObject;
    }
}
