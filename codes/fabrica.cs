using UnityEngine;

public class fabrica : MonoBehaviour
{
    public int type;
    public void take()
    {
        hands h = FindAnyObjectByType<hands>();
        h.siparisAl(true,type);
    }
}

