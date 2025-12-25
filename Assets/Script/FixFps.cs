using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFPS : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private float fps;
    private GUIStyle guiStyle = new GUIStyle();


    private void Start()
    {
        Application.targetFrameRate = 60;
    }


    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        if (deltaTime > 0)
        {
            fps = 1.0f / deltaTime;
        }
        else
        {
            fps = 0;
        }

        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.green;

        GUI.Label(new Rect(10, 10, 200, 50), "FPS: " + Mathf.Ceil(fps).ToString(), guiStyle);
    }
}

