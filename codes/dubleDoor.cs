using UnityEngine;

public class dubleDoor : MonoBehaviour
{
    public bool open;
    public Transform door1,door2;
    void Update()
    {
        door1.localRotation = Quaternion.Lerp(door1.localRotation,Quaternion.Euler(0,open?-90:0,0),Time.deltaTime*5);
        door2.localRotation = Quaternion.Lerp(door2.localRotation,Quaternion.Euler(0,open?90:0,0),Time.deltaTime*5);
    }
    public void interact()
    {
        open = !open;
    }
}

