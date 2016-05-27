using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    private Texture play_button;
    private Texture main_title;

    // Здесь будет код для главного меню
    void Start () {
        GetComponent<FieldCreation>().enabled = false;
        GetComponent<MouseClick>().enabled = false;
        GetComponent<ConquerQuestions>().enabled = false;
        Screen.SetResolution(2 * FieldCreation.shift_horizontally + FieldCreation.cell_size * FieldCreation.number_cells_x, 2 * FieldCreation.shift_vertically + FieldCreation.cell_size * FieldCreation.number_cells_y + 200, false);
        play_button = (Texture)Resources.Load("play_button");
        main_title = (Texture)Resources.Load("main_title");
    }

    void Update () {
        if (Input.GetButtonDown("Fire1")) {
            GetComponent<FieldCreation>().enabled = true;
            GetComponent<MouseClick>().enabled = true;
            GetComponent<ConquerQuestions>().enabled = true;
            GetComponent<MainMenu>().enabled = false;
        }
    }

    void OnGUI () {
        GUI.DrawTexture (new Rect (0, 0, 800, 600), main_title);
        GUI.DrawTexture (new Rect (500, 80, 150, 50), play_button);
    }
}
