using Unity.VisualScripting;
using UnityEngine;

public class mainCodes : MonoBehaviour
{
    public Sprite[] phoneCalls, charBodys;
    public door[] doors;
    int index = 0;
    public bool siparisVerildi;
    private void Start()
    {
        
    }
    public void randomSip()
    {
        siparisVerildi = true;
        index = Random.Range(0, phoneCalls.Length);
        doors = FindObjectsOfType<door>();
        hands hands = FindAnyObjectByType<hands>();
        int i = Random.Range(0, doors.Length);
        string evTarif = doors[i].evTarif;
        string katTarif = doors[i].katTarif;
        string sagSol = doors[i].sagSol;
        int type = Random.Range(1, 4);
        string typeName = "";
        if (type == 1)
            typeName = "Pizza";
        if (type == 2)
            typeName = "Döner";
        if (type == 3)
            typeName = "Burger";
        string metin = $"{typeName} istiyorum. {evTarif} binanın {katTarif} katındayım. Merdivenden sonra {sagSol}daki kapı.";

        hands.takeSiparis(phoneCalls[index], metin);
        doors[i].characterSprite = charBodys[index];
        doors[i].wanted = true;
        doors[i].siparisTip = type;
        Debug.Log(i);
    }
}

