using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Smelter : MonoBehaviour
{
    public Stack<Ore> ores;
    public Stack<Coal> coals;
    public int maxOre;
    public int coalCount;
    public int firePower;
    public Animator animator;
    public GameObject ingot;
    public Transform iron_Molding_Form;
    public AudioClip molding_open;
    public AudioClip molding_close;
    public GameObject firePartical;
    public Transform[] coalPosition;
    public TextMeshProUGUI oreText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI coalText;
    public TextMeshProUGUI fireText;


    private AudioSource moldAudioSource;
    private Animator moldAnimator;
    private Color color;
    private float valueMax;
    private float valueMin;
    private float value;
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        ores = new Stack<Ore>();
        coals = new Stack<Coal>();
        moldAnimator = iron_Molding_Form.GetComponent<Animator>();
        moldAudioSource = iron_Molding_Form.GetComponent<AudioSource>();
    }
    public void Smelt()
    {
        StartCoroutine(Melting());
    }
    public void AddOre(Ore ore)
    {
        value += ore.maxValue;
        ores.Push(ore);
        ore.isInSmelter = true;
        if(firePower>0)
            firePower -= 1;
        UpdateText();
    }
    public void AddFire()
    {
        if (coals.Count > 0)
        {
            Coal coal = coals.Pop();
            Instantiate(firePartical, coal.transform.position,coal.transform.rotation);
            coal.Return();
            firePower += 1;
            if (firePower >= 3&& ores.Count > 0)
            {
                animator.Play("Play0");
                Smelt();
            }
            UpdateText();
        }
    }
    public void UpdateText()
    {
        oreText.text = ores.Count + " / " + maxOre;
        valueText.text = value.ToString();
        coalText.text = coals.Count + " / " + coalPosition.Length;
        fireText.text = firePower + " / 3";
    }
    private IEnumerator Melting()
    {
        yield return null;
        firePower = 0;
        if (moldAnimator.GetCurrentAnimatorStateInfo(0).IsName("Play0"))
        {
            moldAnimator.Play("Play1");
            moldAudioSource.clip = molding_close;
            moldAudioSource.Play();
            yield return new WaitUntil(() => moldAnimator.GetCurrentAnimatorStateInfo(0).IsName("Play1"));
        }
        int num = ores.Count;
        valueMax = 0;
        valueMin = 0;
        while (ores.Count > 0)
        {
            Ore ore = ores.Pop();
            valueMax += ore.maxValue;
            valueMin += ore.minValue;
            color += ore.color;
            ore.Return(isUsed: true);
        }
        color *= 1.0f / (float)num;
        valueMax /= (float)num;
        valueMin /= (float)num;
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Iron_Smelter_Idle"));
        if (!moldAudioSource.isPlaying)
        {
            moldAudioSource.clip = molding_open;
            moldAudioSource.Play();
        }
        moldAnimator.Play("Play0");
        Ingot ingotScript = Instantiate(ingot, iron_Molding_Form.position + 0.15f * Vector3.up, 
            iron_Molding_Form.rotation).GetComponent<Ingot>();
        //ingotScript.value = valueMin + Random.value * (valueMax - valueMin);
        ingotScript.value = value;
        ingotScript.GetComponent<Renderer>().material.color = color;
        ingotScript.color = color;
        value = 0;
        UpdateText();
    }
}
