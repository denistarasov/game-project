using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

    private Vector2 click_coordinates;
    public static int random_int;
    private static GUIStyle random_int_style = new GUIStyle();

    void Update () {
        if (Input.GetButtonDown("Fire1")) {

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

            // Запускаем рандом от 0 до 6 (потому что 7 -- невключительно)
            // Если >= 3, то зона за игроком, иначе -- нет
            // random_int нужно сделать public, чтобы использовать его в OnGUI()
            random_int = Random.Range(0, 7);
            if (random_int >= 3)
                FieldCreation.belonging_to_player [zone_number] = 1;
        }
    }

    void OnGUI() {
        random_int_style.fontSize = 100;
        GUI.Label(new Rect(10, 10, 100, 20), random_int.ToString(), random_int_style);
    }
}
