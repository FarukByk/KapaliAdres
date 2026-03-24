using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hands : MonoBehaviour
{
    public TMP_Text note;
    public Image phoneCall;
    public TMP_Text text;
    float writeSpeed = 0.07f;
    public Animator rightAnimator,leftAnimator;
    public bool siparis;
    public bool defter;
    [HideInInspector]public bool skipable;
    charCont charCont;
    fpsCont fpsCont;
    public int siparisTip = 0;
    bool siparising;
    public GameObject[] types;
    private void Start()
    {
        fpsCont = FindAnyObjectByType<fpsCont>();
    }
    public void Update()
    {
        leftAnimator.SetBool("defter", defter);
        if (Input.GetKeyDown("q"))
        {
            defter = !defter;
        }
        fpsCont.dont = defter;
        if (siparising && Input.GetMouseButtonDown(0) && skipable)
        {
            phoneCall.gameObject.SetActive(false);
            siparising = false;
            clearText();
        }
        for (int i = 0; i < 3; i++)
        {
            if (i+1 == siparisTip)
            {
                types[i].SetActive(true);
            }
            else
            {
                types[i].SetActive(false);
            }
        }
    }
    public int siparisAl(bool al,int tip = 0)
    {
        if (!defter)
        {
            siparis = al;
            if (al)
            {
                siparisTip = tip;
            }
            leftAnimator.SetBool("siparis", siparis);
            return siparisTip;
        }
        return tip;
    }
    public void knock()
    {
        if (!defter)
        {
            rightAnimator.SetTrigger("knock");
        }
    }
    public void interact()
    {
        if (!defter)
        {
            rightAnimator.SetTrigger("interact");
        }
    }
    public void messagesTalk(string msg)
    {
        text.text = "";
        StopAllCoroutines();
        StartCoroutine(write(msg));
    }
    public void clearText()
    {
        text.text = ".";
    }
    IEnumerator write(string msg)
    {
        foreach (char a in msg)
        {
            text.text += a;
            yield return new WaitForSeconds(writeSpeed);
        }
        skipable = true;
    }

    public void takeSiparis(Sprite phoneCall,string msg)
    {
        skipable = false;
        this.phoneCall.gameObject.SetActive(true);
        this.phoneCall.sprite = phoneCall;
        siparising = true;
        messagesTalk(msg);
        note.text = msg;
    }
}

