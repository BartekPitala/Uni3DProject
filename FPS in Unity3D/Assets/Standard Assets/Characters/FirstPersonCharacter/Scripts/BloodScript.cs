using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour {

    public Texture2D bloodTexture;
    private bool hit = false;
    private float opacity = 0.0f;

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.P))
        {
            hit = true;
            opacity = 0.6f;
        }

        if (hit)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, opacity);
            GUI.DrawTexture(new Rect(-640 ,-340, Screen.width*2f, Screen.height*2f), bloodTexture, ScaleMode.ScaleToFit);
            StartCoroutine("waitAndChangeOpacity");
        }

        if (opacity <= 0)
        {
            hit = false;
        }
    }

    IEnumerator waitAndChangeOpacity()
    {
        yield return new WaitForEndOfFrame();
        opacity -= 0.01f;
    }
}
