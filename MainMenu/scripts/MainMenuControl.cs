using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MainMenuControl : MonoBehaviour
{
    public charCont cC;
    public GameObject canvas;

    public RectTransform defter;
    public RectTransform AcikDefter;
    public float moveSpeed = 5f;

    public GameObject Menu;
    public GameObject Ekip;

    public bool isMenu = true;
    public bool isEkip = false;
    public bool isStarted = false;
    
    private void Update()
    {
        if (isMenu)
        {
            defter.localPosition = Vector3.Lerp(defter.localPosition,new Vector3(0,-30,0),Time.deltaTime * moveSpeed);
            AcikDefter.localPosition = Vector3.Lerp(AcikDefter.localPosition, new Vector3(0, -150, 0), Time.deltaTime * moveSpeed);
        }
        if(isStarted && !isMenu)
        {
            defter.localPosition = Vector3.Lerp(defter.localPosition, new Vector3(0, -700, 0), Time.deltaTime * moveSpeed);
            AcikDefter.localPosition = Vector3.Lerp(AcikDefter.localPosition, new Vector3(0, -150, 0), Time.deltaTime * moveSpeed);
        }
        if(isEkip && !isMenu)
        {
            AcikDefter.localPosition = Vector3.Lerp(AcikDefter.localPosition, new Vector3(0, 800, 0), Time.deltaTime * moveSpeed);
            defter.localPosition = Vector3.Lerp(defter.localPosition, new Vector3(0, -700, 0), Time.deltaTime * moveSpeed);
        }
        
    }
    public void basla()
    {
        isMenu = false;
        isEkip = false;
        isStarted = true;

        myMath.waitAndStart(1, () => { 
           Destroy(gameObject);
            cC.enabled = true;
            canvas.SetActive(true);
        });
    }
    public void credits()
    {
        isMenu = false;
        isEkip = true;
    }
    public void backCredits()
    {
        isMenu = true;
        isEkip = false;
    }
    public void close()
    {
        Application.Quit();
    }
}
