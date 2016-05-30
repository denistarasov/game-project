using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

    private Vector2 click_coordinates;
    public static int current_player_number = 1;
    public static int number_of_players = 2;

    public static bool is_game_over = false;
    // Если true -- то захватывает зону
    // Если false -- то выбирает свою зону, из которой будет захватывать
    public static bool is_conquering = false;
    public static int crossed_zone = -1;
    public static bool is_neighbor = false;

    // Стили
    private static GUIStyle current_player_number_style = new GUIStyle();
    private static GUIStyle game_over_style = new GUIStyle();
    private static GUIStyle winner_number_style = new GUIStyle();


    void Update () {
        if (Input.GetButtonDown("Fire1") && !is_game_over) {

            // Находим место, куда кликнул игрок, точка отсчета -- левый нижний угол
            click_coordinates = Input.mousePosition;

            // Переведем это значение в систему координат с отсчетом в левом верхнем углу
            click_coordinates.y = Screen.height - click_coordinates.y;

            // Учитываем сдвиг самого поля (сдвиг для выравнивания по центру)
            click_coordinates.x += FieldCreation.shift_horizontally;
            click_coordinates.y -= FieldCreation.shift_vertically;

            // Получаем координаты клетки, куда кликнул игрок
            click_coordinates.x = click_coordinates.x / FieldCreation.cell_size;
            click_coordinates.y = click_coordinates.y / FieldCreation.cell_size;

            // А теперь эти же координаты -- но в массиве клеток поля
            int coordinates_in_array = (int)click_coordinates.y * FieldCreation.number_cells_x + (int)click_coordinates.x;

            // Ищем зону
            int zone_number = FieldCreation.array_of_hexagons [coordinates_in_array];
            int owner_number = FieldCreation.belonging_to_player [FieldCreation.array_of_hexagons [coordinates_in_array] ];

            // Текущий игрок выбирает зону, с которой будет ходить
            if (owner_number == current_player_number && !is_conquering) {
                crossed_zone = zone_number;
                is_conquering = true;
            } else if (zone_number == crossed_zone && is_conquering) {
                crossed_zone = -1;
                is_conquering = false;
            }

            // Проверяем, что зона, куда будет ходить игрок,
            // соседняя с той, откуда ходит игрок
            is_neighbor = false;
            if (FieldCreation.neighborhood_graph[crossed_zone].Contains(zone_number)) {
                is_neighbor = true;
            } 

            // Текущий игрок пытается захватить соседнюю зону
            if (owner_number != current_player_number && is_conquering && is_neighbor && ConquerQuestions.is_answered) {
                bool is_right_answer = ConquerQuestions.is_right_answer;
                if (is_right_answer) {
                    FieldCreation.belonging_to_player [zone_number] = current_player_number;
                }
                ConquerQuestions.askNewQuestion();
                ConquerQuestions.randomizeAnswerOrder();

                // Переход хода к следующему игроку
                if (current_player_number == number_of_players)
                    current_player_number = 1;
                else
                    ++current_player_number;

                is_conquering = false;
                crossed_zone = -1;
            }
        }

        // Проверка, захвачены ли все зоны одним игроком
        // (код похож на костыль)
        bool temp_flag = false;
        int first_element = FieldCreation.belonging_to_player[0];
        foreach (int element in FieldCreation.belonging_to_player) {
            if (first_element != element && element != 0)
                temp_flag = true;
        }
        is_game_over = !temp_flag;
    }

    void OnGUI() {
        // Отрисовка номера текущего игрока
        if (!is_game_over && !MainMenu.is_main_menu) {
            current_player_number_style.fontSize = 30;
            GUI.Label(new Rect(630, 25, 100, 20), current_player_number.ToString() + " player's turn", current_player_number_style);
        }
        // Если игра завершена, то нужно вывести
        // "GAME OVER" и номер победителя
        if (is_game_over) {
            game_over_style.fontSize = 100;
            GUI.Label(new Rect(130, 100, 100, 20), "GAME OVER", game_over_style);
            winner_number_style.fontSize = 30;
            GUI.Label(new Rect(630, 25, 100, 20), "Player " + FieldCreation.belonging_to_player[0] + " won", winner_number_style);
        }
    }
}
