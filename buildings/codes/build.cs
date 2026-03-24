using System;
using System.Linq;
using UnityEngine;




[ExecuteInEditMode]
public class build : MonoBehaviour
{
    public bool apply;
    Transform roofTop;
    GameObject[] roof = new GameObject[0];
    
    [Range(1, 2)]
    public int roofTopType = 1;
    [Range(1,5)]
    public int roofCount = 1;
    public Material refMat;

    public colorSystem colorSystem;

    private void Start()
    {
        if (Application.isPlaying)
        {
            Destroy(this);
        }
    }
    void Update()
    {
        if (apply)
        {
            transforms();
            apply = false;
            color();
        }
    }

    void transforms()
    {
        roofTop = transform.Find($"roofTop{roofTopType}");
        transform.Find($"roofTop{(roofTopType == 1 ? 2 : 1)}").gameObject.SetActive(false);
        transform.Find($"roofTop{roofTopType}").gameObject.SetActive(true);
        if (roof != null)
        {
            for (int i = 1; i < roof.Length; i++)
            {
                DestroyImmediate(roof[i]);
            }
        }
        roof = new GameObject[roofCount];
        roof[0] = transform.Find("roof0").gameObject;
        for (int i = 1; i < roofCount; i++)
        {
            GameObject go = Instantiate(roof[0], transform);
            go.name = $"roof{i}";
            go.transform.localPosition = new Vector3(0, 5 + (i * 10), 0);
            roof[i] = go;
        }
        roofTop.localPosition = new Vector3(0, 5 + (roofCount * 10), 0);
        
    }

    void color()
    {
        if (transform.Find("enterance").gameObject != null)
        {
            MaterialApply(transform.Find("enterance").gameObject);

        }
        if (roofTop.gameObject != null)
        {
            MaterialApply(roofTop.gameObject);

        }
        else
        {
            Debug.Log("enterance Yok");
        }
        

        foreach (GameObject go in roof)
        {
            MaterialApply(go);
        }


    }
    void MaterialApply(GameObject obj)
    {
        Transform ent = obj.transform;
        MeshRenderer mr1 = ent.Find("ladders").GetComponent<MeshRenderer>();
        MeshRenderer mr2 = ent.Find("wall").GetComponent<MeshRenderer>();

        Material[] m1 = mr1.sharedMaterials;
        Material[] m2 = mr2.sharedMaterials;

        mr1.sharedMaterials = setMats(m1);
        mr2.sharedMaterials = setMats(m2);
    }
    Material[] setMats(Material[] mats)
    {
        Material[] newMats = new Material[mats.Length];
        for (int i = 0; i < mats.Length; i++)
        {
            newMats[i] = selectColor(mats[i]);
        }
        return newMats;
    }
    Material selectColor(Material material)
    {
        Material mat = new Material(refMat);
        
        if (Application.isEditor)
        {

            mat.name = material.name;
            switch (material.name)
            {
                case "black":
                    mat.color = colorSystem.black; break;
                case "gray":
                    mat.color = colorSystem.grey; break;
                case "wallInside":
                    mat.color = colorSystem.insideWall; ; break;
                case "white":
                    mat.color = colorSystem.white; break;
                case "wood":
                    mat.color = colorSystem.wood; break;
                case "wall0":
                    mat.color = colorSystem.wall[0]; break;
                case "wall1":
                    mat.color = colorSystem.wall[1]; break;
                case "roof0":
                    mat.color = colorSystem.roofTop[0]; break;
                case "roof1":
                    mat.color = colorSystem.roofTop[1]; break;
            }
        }
        else
        {
            mat.name = material.name.Substring(0, material.name.Length - 11);
            switch (material.name)
            {
                case "black (Instance)":

                    mat.color = colorSystem.black; break;
                case "gray (Instance)":
                    mat.color = colorSystem.grey; break;
                case "wallInside (Instance)":
                    mat.color = colorSystem.insideWall; ; break;
                case "white (Instance)":
                    mat.color = colorSystem.white; break;
                case "wood (Instance)":
                    mat.color = colorSystem.wood; break;
                case "wall0 (Instance)":
                    mat.color = colorSystem.wall[0]; break;
                case "wall1 (Instance)":
                    mat.color = colorSystem.wall[1]; break;
                case "roof0 (Instance)":
                    mat.color = colorSystem.roofTop[0]; break;
                case "roof1 (Instance)":
                    mat.color = colorSystem.roofTop[1]; break;
            }
        }
        return mat;
    }
}
[Serializable]
public class colorSystem
{
    public Color[] wall = new Color[2];
    public Color insideWall = Color.green;
    public Color[] roofTop = new Color[2];
    public Color black = Color.black, white = Color.white,grey = Color.grey,wood = Color.white;

}