using UnityEngine;
using UnityEngine.TextCore.Text;

public class motor : MonoBehaviour
{	
    Rigidbody rb;
    public bool control;
    public float minSpeed,maxSpeed;
    public float speed;
    public float turnSpeed;
    public float minTurnSpeed,maxTurnSpeed;
    public GameObject Arms;
    public Transform charPos;
    float plusTurnSpeed;
    float torque;
    float plusSpeed;
    public Transform gidon, t1, t2,body;
    charCont cc;
    hands hands;
    mainCodes mainCodes;
    AudioSource aS;
    void Start()
    {
        aS = GetComponent<AudioSource>();
        mainCodes = FindAnyObjectByType<mainCodes>();
        hands = FindAnyObjectByType<hands>();
        cc = FindAnyObjectByType<charCont>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        aS.pitch = plusSpeed/(maxSpeed-minSpeed);
        if (control)
        {
            if (!mainCodes.siparisVerildi)
            {
                mainCodes.randomSip();
            }
            cc.control = false;
        }
        if (speed <5 && Input.GetKeyDown("e") && control)
        {
            control = false;
            cc.control = true;
            cc.transform.parent = null;
            cc.transform.localRotation = Quaternion.Euler(0, cc.transform.localEulerAngles.y, 0);
        }
        Vector3 myInputs = Vector3.zero;
        if (control)
        {
            myInputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            cc.transform.localPosition = Vector3.zero;
            cc.transform.localRotation = Quaternion.Euler(0,cc.transform.localEulerAngles.y,0);
            cc.transform.parent = charPos;
        }
        Arms.SetActive(control);
        transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, torque * Time.deltaTime, 0);
        torque = myInputs.x * speed * (turnSpeed + plusTurnSpeed);

        if (myInputs.x != 0 && plusTurnSpeed < maxTurnSpeed-minTurnSpeed)
        {
            plusTurnSpeed += Time.deltaTime;
        }
        else if(plusTurnSpeed > 0)
        {
            plusTurnSpeed -= Time.deltaTime *20;

        }

        if (myInputs.z > 0 && plusSpeed < maxSpeed-minSpeed)
        {
            plusSpeed += Time.deltaTime;
        }
        else if(plusSpeed > 0)
        {
            plusSpeed -= Time.deltaTime * 5;
        }
        float currentSpeed = myInputs.z > 0? minSpeed +plusSpeed:myInputs.z < 0? -minSpeed:0;
        speed = Mathf.Lerp(speed, currentSpeed, Time.deltaTime * 5);

        rb.velocity = transform.rotation * new Vector3(0, 0, speed) + new Vector3(0,-10,0);

        rotes();
    }

    public void Interact()
    {
        if (!hands.siparis)
        {
            control = true;
        }
    }

    float tRotes;
    void rotes()
    {
        tRotes += speed * 20 * Time.deltaTime;

        float x = 0;
        if (control)
        {
            x = Input.GetAxis("Horizontal");
        }
        gidon.localRotation = Quaternion.Lerp(gidon.localRotation, Quaternion.Euler(0, x * 30, 0) , Time.deltaTime*10);
        t1.transform.localRotation = Quaternion.Euler(tRotes, x*30, 0);
        body.localRotation = Quaternion.Lerp(body.localRotation, Quaternion.Euler(0, 0, x * plusTurnSpeed * -5),Time.deltaTime*5);
        t2.transform.localRotation = Quaternion.Euler(tRotes, 0, 0);
    }
}

