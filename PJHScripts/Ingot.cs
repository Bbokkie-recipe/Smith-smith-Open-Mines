using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : OVRGrabbable
{
    public bool isSharpened;
    public int hammerCount;
    public int sharpenNum;
    public float value;
    public float Sharpened;
    public Color color;
    public GameObject explosion;
    public GameObject spark;
    public GameObject spark2;
    public GameObject normalSword;
    public GameObject broadSword;
    public GameObject longSword;

    private bool onEnvil;
    private bool isBeingSharpened;
    private bool isExitSharpener;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        base.GrabBegin(hand, grabPoint);
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
        hand.GetComponent<GrabValue>().value = value;
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        GetComponent<Rigidbody>().useGravity = true;
    }
    protected override void Start()
    {
        sharpenNum = 0;
        Sharpened = 0;
        hammerCount = 0;
        isBeingSharpened = false;
        isExitSharpener = false;
        isSharpened = false;
    }
    private void OnEnable()
    {
        Start();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Sharpener"&&!isSharpened&&!isBeingSharpened)
        {
            if (collision.transform.GetComponentInParent<Sharpener>().
                animator.GetCurrentAnimatorStateInfo(0).IsName("Wheel_Sharpener_Rotation_Loop"))
            {
                isBeingSharpened = true;
                StartCoroutine(Sharpening(collision));
            }
            else
            {
                isBeingSharpened = true;
                StartCoroutine(Sharpening(collision));
            }
        }
        if (collision.transform.tag == "Envil"&& !isGrabbed&&!isSharpened)
        {
            onEnvil = true;
            transform.parent = collision.transform;
            transform.localPosition = new Vector3(-0.17f, 0.35f, 0);
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        if (onEnvil)
        {
            if (collision.transform.tag == "Hammer" && OVRInput.GetLocalControllerVelocity
                (collision.transform.GetComponent<OVRGrabbable>().grabbedBy.Controller).magnitude>3f)
            {
                StartCoroutine(VibrateController(0.3f, 0.2f, 0.5f, collision.transform.GetComponent<OVRGrabbable>().grabbedBy.Controller));
                Instantiate(spark, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
                Vector3 temp = transform.localScale;
                temp.x *= 1.1f;
                transform.position = transform.position - Vector3.up * temp.y * 0.05f;
                temp.y *= 0.909f;
                transform.localScale = temp;
                if (hammerCount > 12)
                {
                    Instantiate(explosion,transform.position,transform.rotation);
                    Destroy(gameObject);
                }
                hammerCount += 1;
            }
        }
    }
    public void Update()
    {
        if(!SaveNLoad.instance.saveData.ingots.ContainsKey(GetInstanceID()))
            SaveNLoad.instance.saveData.ingots.Add(GetInstanceID(), new IngotSave(this));
        else
        {
            SaveNLoad.instance.saveData.ingots.Remove(GetInstanceID());
            SaveNLoad.instance.saveData.ingots.Add(GetInstanceID(), new IngotSave(this));
        }
    }
    public IEnumerator VibrateController(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
    private IEnumerator Sharpening(Collision collision)
    {
        float waitTime = 0;
        while (!isExitSharpener)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Instantiate(spark2, collision.GetContact(0).point, Quaternion.LookRotation(-collision.GetContact(0).normal));
            if (Sharpened > 1.5f)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                if (grabbedBy != null)
                    grabbedBy.ForceRelease(this);
                StartCoroutine(DelayedDestroy());
                break;
            }
            while (waitTime < 0.05f)
            {
                waitTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Sharpened += waitTime / 5;
            waitTime = 0;
        }
        if (Sharpened > 0.5f)
        {
            if (Sharpened > 1.05f)

            {
                value *= (2 - Sharpened);
                Debug.Log(Sharpened.ToString()+" 연마 가치 변화: " + value.ToString());
            }
            else if (Sharpened < 0.95f)
            {
                value *= Sharpened;
                Debug.Log(Sharpened.ToString() + "연마 가치 변화: " + value.ToString());
            }
            sharpenNum += 1;
            if (hammerCount<6)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                if (grabbedBy != null)
                    grabbedBy.ForceRelease(this);
                StartCoroutine(DelayedDestroy());
            }
            if (hammerCount < 9)
            {
                value *= (float)hammerCount/9f;
                Debug.Log(transform.localScale.x.ToString()+ " 사이즈 가치 변화" +value.ToString());
            }
            if (hammerCount > 10)
            {
                value *= (float)(17-hammerCount)/9f;
                Debug.Log(transform.localScale.x.ToString() + " 사이즈 가치 변화" + value.ToString());
            }
            Blade blade;
            if (hammerCount > 9)
            {
                longSword.SetActive(true);
                longSword.GetComponent<Renderer>().material.color = color;
                blade = longSword.GetComponent<Blade>();
            }
            else if (Sharpened < 1f)
            {
                broadSword.SetActive(true);
                broadSword.GetComponent<Renderer>().material.color = color;
                blade = broadSword.GetComponent<Blade>();
            }
            else
            {
                normalSword.SetActive(true);
                normalSword.GetComponent<Renderer>().material.color = color;
                blade = normalSword.GetComponent<Blade>();
            }
            blade.value = value;
            GetComponent<MeshRenderer>().enabled = false;
            transform.localScale = new Vector3(0.4f, 0.1f, 0.15f);
            GetComponent<BoxCollider>().enabled = false;
            isSharpened = true;
        }
        if (grabbedBy != null)
        {
            grabbedBy.GetComponent<GrabValue>().value = value;
        }
        isExitSharpener = false;
        isBeingSharpened = false;
        yield break;
    }
    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("터질 때 연마도: " + Sharpened.ToString());
        Debug.Log("터질 때 크기: " + transform.localScale.x.ToString());
        Destroy(gameObject);
        yield break;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Sharpener"&&!isSharpened)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            isExitSharpener = true;
        }

        if (collision.transform.tag == "Envil"&&onEnvil)
        {
            onEnvil = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
    private void OnDestroy()
    {
        SaveNLoad.instance.saveData.ingots.Remove(GetInstanceID());
    }
}
