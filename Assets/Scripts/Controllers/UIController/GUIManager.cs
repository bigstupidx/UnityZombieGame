using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public RenderTexture MiniMapTexture;
    public Material MiniMapMaterial;


    void OnGUI()
    {
        Rect Map_Rectangle = new Rect(0.001f * Screen.width, 0.7f * Screen.height, 0.24f * Screen.width, 0.30f * Screen.height);
        if(Event.current.type == EventType.Repaint)
        {
            Graphics.DrawTexture(Map_Rectangle, MiniMapTexture, MiniMapMaterial);
        }
    }
}
