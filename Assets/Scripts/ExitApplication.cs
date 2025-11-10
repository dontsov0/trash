#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ExitApplication : MonoBehaviour
{
     public void Exit()
     {
         Debug.Log("Exit triggered");
         
         #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
         #else
             Application.Quit();
         #endif
     }
}




