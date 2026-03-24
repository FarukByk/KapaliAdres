using UnityEngine;
using System.Threading.Tasks;
using System;
public class myMath : MonoBehaviour
{
    //mal sait
    public static async void waitAndStart(float second, Action action)
    {
        await Task.Delay((int)(second * 1000));
        action?.Invoke();
    }


}
