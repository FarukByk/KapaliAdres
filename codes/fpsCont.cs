using System;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class fpsCont : MonoBehaviour
{
    public Camera cam;
    public bool dont;
    public CharInfo charInfo;
    public GameObject holdableImage;
    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, charInfo.interactDistance))
        {

            if (hit.collider.GetComponent<interact>() != null)
            {
                holdableImage.SetActive(true);
                if (Input.GetKeyDown(charInfo.interactKey0) && !dont)
                {
                    hit.collider.GetComponent<interact>().interaction();
                }

            }
            else
            {
                holdableImage.SetActive(false);
            }
        }
        else
        {
            holdableImage.SetActive(false);
        }
    }
}
[Serializable]
public class CharInfo
{
    public float interactDistance;
    public LayerMask interactLayer;
    public KeyCode interactKey0 = KeyCode.Mouse0;
}
