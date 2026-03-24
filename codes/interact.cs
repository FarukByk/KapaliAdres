using UnityEngine;
using UnityEngine.Events;

public class interact : MonoBehaviour
{
    public UnityEvent action;

    public void interaction()
    {
        action?.Invoke();
    }
}

