using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum buttonType
{
    Start,
    Quit
}

public class ButtonBehavior : MonoBehaviour
{
    public buttonType type;

    private void OnMouseDown()
    {
        if(type == buttonType.Start)
        {
            SceneManager.LoadScene("Room1");
        }

        if(type == buttonType.Quit)
        {
            Application.Quit();
        }
    }

}
