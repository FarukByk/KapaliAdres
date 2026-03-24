using UnityEngine;

public class kolControl : MonoBehaviour
{
    public Transform target;
    public bool close;
    public GameObject go;
    void Update()
    {
        if (close)
        {
            go.SetActive(!FindAnyObjectByType<hands>().defter);
        }

        transform.LookAt(target);
    }
}

