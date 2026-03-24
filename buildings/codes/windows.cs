using System.Collections.Generic;
using UnityEngine;

public class windows : MonoBehaviour
{
    public Material glowMat;
    List<MeshRenderer> mrs = new List<MeshRenderer>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            mrs.Add(transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }

        int winCout = Random.Range(0, 5);
        for (int i = 0; i < winCout; i++)
        {
            int rand = Random.Range(0, mrs.Count);
            List<Material> mats = new List<Material>();

            foreach (Material ma in mrs[rand].materials)
            {
                if (ma.name == "black (Instance)")
                {
                    mats.Add(glowMat);
                }
                else
                {
                    mats.Add(ma);
                }
            }
            mrs[rand].SetMaterials(mats);
            mrs.Remove(mrs[rand]);

        }

        Destroy(this);
    }
    
}

