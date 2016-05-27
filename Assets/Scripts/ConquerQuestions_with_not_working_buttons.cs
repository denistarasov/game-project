/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConquerQuestions : MonoBehaviour {

    private Texture question;
    private Texture first_answer;
    private Texture second_answer;

    private int random_question_number = 1;

    public bool is_right_answer = false;

    void Start () {
        // Выбираем рандомный вопрос
        random_question_number = Random.Range(1, 1);
        question = (Texture)Resources.Load(random_question_number + "-question");
        first_answer = (Texture)Resources.Load(random_question_number + "-first-answer");
        second_answer = (Texture)Resources.Load(random_question_number + "-second-answer");
    }

    void Update () {
        //if (Input.GetButtonDown("Fire1")) {
        //Button MyButton = button.AddComponent<Button>();

        
    }

    void onFirstAnswerClick() {
        is_right_answer = true;
    }

    void onSecondAnswerClick() {
        is_right_answer = false;
    }

    void OnGUI () {
        int que_x_pos = 180;
        int que_y_pos = 320;
        GUI.DrawTexture (new Rect (que_x_pos, que_y_pos, 500, 75), question);
        Button first_answer_button = GUI.Button (new Rect (que_x_pos + 30, que_y_pos + 100, 100, 50), first_answer);
        Button second_answer_button = GUI.Button (new Rect (que_x_pos + 500 - 100 - 30, que_y_pos + 100, 100, 50), second_answer);
        first_answer_button.onClick.AddListener(onFirstAnswerClick);
        second_answer_button.onClick.AddListener(onSecondAnswerClick);
    }
}
*/