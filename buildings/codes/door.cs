using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{	
    Animator animator;
    public bool wanted = false;
    public SpriteRenderer charbodyRenderer;
    public Sprite characterSprite;
    public Transform lookTransform,posTransform;
    hands hands;
    charCont charCont;
    public bool open;
    float openSec;
    public float openSecond;
    bool waiting;
    public int siparisTip;
    int talkIndex;
    bool talking;
    public List<string> goodEndingMessages;
    public List<string> badEndingMessages;
    public string evTarif,katTarif,sagSol;
    bool good;
    public void Awake()
    {
        charbodyRenderer = transform.Find("Character").GetComponent<SpriteRenderer>();
        hands = FindAnyObjectByType<hands>();
        charCont = FindAnyObjectByType<charCont>();
        animator = GetComponent<Animator>();
        evTarif = transform.parent.parent.GetComponent<tarif>().tarifMsg;
        int maxKat = transform.parent.parent.GetComponent<build>().roofCount;
        switch ((int.Parse(transform.parent.name[transform.parent.name.Length - 1].ToString())))
        {
            case 0:
                katTarif = "giriş"; return;
            case 1:
                katTarif = "en altın bir üstü"; return;
            case 2:

                if (maxKat == 3)
                {
                    katTarif = "en üst"; return;
                }
                else if (maxKat == 4)
                {
                    katTarif = "en üstün bir altı"; return;
                }
                else if (maxKat == 5)
                {
                    katTarif = "en üstün iki altı"; return;
                }return;
            case 3:

                if (maxKat == 4)
                {
                    katTarif = "en üst"; return;
                }
                else if (maxKat == 5)
                {
                    katTarif = "en üstün bir altı"; return;
                }
                return;
            case 4:

                katTarif = "en üst"; return;
        }
        
    }
    private void Update()
    {
        if (characterSprite != null && charbodyRenderer != null)
        {
            charbodyRenderer.sprite = characterSprite;
        }
        if (waiting)
        {
            openSec += Time.deltaTime;

            if (openSec > openSecond)
            {
                waiting = false;
                openDoor();
            }
        }
    }
    public void openDoor()
    {
        open = true;
        animator.SetBool("open", open);
        charCont.lookTarget = lookTransform;
        charCont.posTarget = posTransform;
        charCont.control = false;
    }
    public void knock()
    {
        Debug.Log("knock");
        if (charCont.control && !open)
        {
            waiting = true;
            hands.knock();
            charCont.lookTarget = lookTransform;
            charCont.posTarget = posTransform;
            charCont.control = false;
            myMath.waitAndStart(1.5f, () => {
                charCont.lookTarget = null;
                charCont.posTarget = null;
                charCont.control = true;
            });
        }
        if (open && !talking)
        {
            if (hands.siparis && hands.siparisTip == siparisTip && wanted)
            {
                good = true;
            }
            else
            {
                good = false;
            }
            talking = true;
            talk();
        }
        else if (talking && hands.skipable)
        {
            talkIndex++;
            talk();
        }
    }
    void talk()
    {
        if (good)
        {
            if (talkIndex >= goodEndingMessages.Count)
            {
                goodEnd();
            }
            else
            {
                hands.messagesTalk(goodEndingMessages[talkIndex]);
            }

                
        }
        else
        {
            if (talkIndex >= badEndingMessages.Count)
            {
                badEnd();
            }
            else
            {
                hands.messagesTalk(badEndingMessages[talkIndex]);
            }
            
        }
    }

    void goodEnd()
    {
        hands.siparisAl(false);
        hands.interact();
        hands.clearText();
        myMath.waitAndStart(0.4f, () =>
        {
            open = false;
            animator.SetBool("open", open);
            waiting = false;
            charCont.lookTarget = null;
            charCont.posTarget = null;
            charCont.control = true;
            Destroy(this);
        });
        FindAnyObjectByType<mainCodes>().siparisVerildi = false;
    }

    void badEnd()
    {
        hands.siparisAl(false);
        open = false;
        hands.clearText();
        animator.SetBool("open", open);
        waiting = false;
        charCont.lookTarget = null;
        charCont.posTarget = null;
        charCont.control = true;
        Destroy(this);
        FindAnyObjectByType<mainCodes>().siparisVerildi = false;

    }

}

