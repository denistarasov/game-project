using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConquerQuestions : MonoBehaviour {

    private Texture question;

    public static bool is_right_answer = false;
    public static int random_question;
    public static int random_answer;
    public static bool is_answered = false;
    private static GUIStyle question_style = new GUIStyle();

    public static string[,] questions = new string[,] {
        {"Год Бородинского сражения", "1812", "1813"},
        {"Начало Второй Мировой", "1939", "1945"},
        {"Год смерти Петра I", "1725", "1726"},
        {"Начало Первой Мировой", "1914", "1913"},
        {"Год краха СССР", "1991", "1990"},
        {"В каком году родился Пушкин?", "1799", "1798"}
    };

    public static void askNewQuestion() {
        random_question = (random_question + 1) % questions.GetLength(0);
        is_answered = false;
    }

    public static void randomizeAnswerOrder() {
        // Определяем, какой рандомный ответ где будет
        random_answer = Random.Range(1, 3);
    }

    void Start () {
        random_question = Random.Range(0, questions.GetLength(0));
        randomizeAnswerOrder();
        question = (Texture)Resources.Load("blank_question_table");
    }

    void Update () {
    }

    void OnGUI () {
        int que_x_pos = 180;
        int que_y_pos = 320;
        GUI.DrawTexture (new Rect (que_x_pos, que_y_pos, 500, 75), question);
        question_style.fontSize = 30;
        if (is_answered) {
            GUI.Label (new Rect (que_x_pos + 40, que_y_pos + 20, 500, 75),
                       "Ответ записан",
                       question_style);
        } else {
            GUI.Label (new Rect (que_x_pos + 40, que_y_pos + 20, 500, 75),
                       questions[random_question, 0],
                       question_style);
        }

        // Проверка, какую из двух кнопок нажал игрок
        if (GUI.Button (new Rect (que_x_pos + 30, que_y_pos + 100, 100, 50), 
                        questions[random_question, random_answer])) {
            is_right_answer = random_answer == 1 ? true : false;
            is_answered = true;
        }
        if (GUI.Button (new Rect (que_x_pos + 500 - 100 - 30, que_y_pos + 100, 100, 50),
                        questions[random_question, 3 - random_answer])) {
            is_right_answer = random_answer == 1 ? true : false;
            is_answered = true;
        }
    }
}
