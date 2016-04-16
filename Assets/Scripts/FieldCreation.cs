using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldCreation : MonoBehaviour {
    // Объявление глобальных константных переменных
    // Количество клеток по горизонтали и вертикали
    public static int number_cells_x = 28;
    public static int number_cells_y = 10;

    // Сдвиг поля от (0;0)
    public static int shift_horizontally = 0;
    public static int shift_vertically = 0;

    // Сдвиг для границ
    public static int shift_for_borders = 1;

    // Размер одной клетки
    public static int cell_size = 30;

    // Размер границ
    public static int border_lesser_size = 4;
    public static int border_bigger_size = 34;

    // Разрешение будет 1024х768, соответственно делаем
    // размеры и количество клеток
    // Но пока сделаем его зависящим от размера клеток, а не наоборот
    public static int res_horizontal = 2 * shift_horizontally + cell_size * number_cells_x;
    public static int res_vertical = 2 * shift_vertically + cell_size * number_cells_y;

    // Создадим кнопки разных цветов
    private Texture square_button;
    private Texture red_square;
    private Texture green_square;
    private Texture white_square;
    private Texture horizontal_border;
    private Texture vertical_border;

    // Создаем матрицу кнопок, сверху которых будут спрайты-шестиугольники.
    // Каждая ячейка матрицы обозначает принадлежность к той или иной "зоне".
    // Зона -- это то, что может захватывать игрок
    public static int[] array_of_hexagons = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3,
                                                        0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3,
                                                        0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3,
                                                        4, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3,
                                                        4, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 7, 3, 3,
                                                        4, 4, 4, 0, 0, 4, 4, 5, 1, 1, 5, 5, 5, 5, 6, 6, 6, 2, 2, 6, 6, 7, 3, 3, 7, 7, 7, 7,
                                                        4, 4, 4, 4, 4, 4, 4, 5, 5, 1, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7,
                                                        4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7,
                                                        4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7,
                                                        4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7 };
    // Теперь создадим массив,
    // индексы -- это зоны,
    // элементы -- принадлежность конкретному игроку
    public static int[] belonging_to_player = new int[] {1, 0, 0, 0, 0, 0, 0, 2};

    // Лист клеток, снизу и правее которых нужно поставить границу
    List<int> list_of_horizontal_borders = new List<int>();
    List<int> list_of_vertical_borders = new List<int>();

    void Start () {
        // Разрешение выбрано так, чтобы поместилось поле и отображение "схватки",
        // т.е. отображение внизу рандомных чисел
        Screen.SetResolution(res_horizontal, res_vertical + 200, false);
        white_square = (Texture)Resources.Load("white_square");
        green_square = (Texture)Resources.Load("green_square");
        red_square = (Texture)Resources.Load("red_square");
        horizontal_border = (Texture)Resources.Load("horizontal_border");
        vertical_border = (Texture)Resources.Load("vertical_border");

        // Найдем все клетки, снизу которых нужно поставить горизонтальную границу
        for (int i = 0; i < number_cells_x * number_cells_y; ++i) {
            // Проверка, на последнем ли ряду.
            // Нужно, чтобы не выйти за границы массива при прибавлении number_cells_x
            if (i < number_cells_x * number_cells_y - number_cells_x)
                if (array_of_hexagons[i] != array_of_hexagons[i + number_cells_x]) {
                    list_of_horizontal_borders.Add(i + number_cells_x);
                }
        }

        // Найдем все клетки, правее которых нужно поставить вертикальную границу.
        // Вычитаем единицу, т.к. последний элемент проверять не надо: правее ничего нет
        for (int i = 0; i < number_cells_x * number_cells_y - 1; ++i) {
            // Проверка, что i не последний элемент в ряду.
            // Иначе границу справа рисовать не нужно -- там ничего нет
            if ((i + 1) % number_cells_x != 0)
                if (array_of_hexagons[i] != array_of_hexagons[i + 1]) {
                    list_of_vertical_borders.Add(i + 1);
                }
        }
    }

    void OnGUI () {
        // Выводим кнопки-шестиугольники с неким константным сдвигом cell_size
        for (int i = 0; i < number_cells_x * number_cells_y; ++i) {
            square_button = white_square;
            if (belonging_to_player [array_of_hexagons [i]] == 1)
                square_button = red_square;
            if (belonging_to_player [array_of_hexagons [i]] == 2)
                square_button = green_square;
            
            // Rect (horizontal, vertical, width, height)
            GUI.DrawTexture (new Rect (shift_horizontally + cell_size * (i % number_cells_x),
                                       shift_vertically + cell_size * (i / number_cells_x),
                                       cell_size,
                                       cell_size),
                             square_button
                            );
        }

        // Рисуем границы поверх клеток
        foreach (int i in list_of_horizontal_borders) {
            GUI.DrawTexture (new Rect (shift_horizontally + cell_size * (i % number_cells_x) - 2,
                                       shift_vertically + cell_size * (i / number_cells_x) - 2,
                                       border_bigger_size,
                                       border_lesser_size),
                             horizontal_border
                            );
        }
        foreach (int i in list_of_vertical_borders) {
            GUI.DrawTexture (new Rect (shift_horizontally + cell_size * (i % number_cells_x) - 2,
                                       shift_vertically + cell_size * (i / number_cells_x) - 2,
                                       border_lesser_size,
                                       border_bigger_size),
                             vertical_border
                            );
        }
        // Теперь границами "обвожим" само поле
        for (int i = 0; i < number_cells_x; ++i) {
            GUI.DrawTexture (new Rect (shift_horizontally + cell_size * (i % number_cells_x),
                                       shift_vertically,
                                       border_bigger_size,
                                       border_lesser_size),
                             horizontal_border
                            );
            GUI.DrawTexture (new Rect (shift_horizontally + cell_size * (i % number_cells_x),
                                       shift_vertically + cell_size * number_cells_y,
                                       border_bigger_size,
                                       border_lesser_size),
                             horizontal_border
                            );
        }
        for (int i = 0; i < number_cells_y; ++i) {
            GUI.DrawTexture (new Rect (shift_horizontally,
                                       shift_vertically + cell_size * i,
                                       border_lesser_size,
                                       border_bigger_size),
                             vertical_border
                            );
            GUI.DrawTexture (new Rect (shift_horizontally + cell_size * number_cells_x,
                                       shift_vertically + cell_size * i,
                                       border_lesser_size,
                                       border_bigger_size),
                             vertical_border
                            );
        }
    }
}
