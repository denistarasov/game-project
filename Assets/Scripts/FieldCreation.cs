using UnityEngine;
using System.Collections;

public class FieldCreation : MonoBehaviour {

	// Объявление глобальных константных переменных
	// Количество клеток по горизонтали и вертикали
	public static int number_cells_x = 28;
	public static int number_cells_y = 10;

	// Сдвиг поля от (0;0)
	public static int shift_horizontally = 5;
	public static int shift_vertically = 9;

	// Размер одной клетки
	public static int cell_size = 30;

	// Создадим кнопки разных цветов
	private Texture square_button;
	private Texture red_square;
	private Texture green_square;
	private Texture white_square;

	// Создаем матрицу кнопок, сверху которых будут спрайты-шестиугольники.
	// Каждая ячейка матрицы обозначает принадлежность к той или иной "зоне".
	// Зона -- это то, что может захватывать игрок
	public static int[] array_of_hexagons = new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3,
												  		0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3,
												  		0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3,
												  		0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3,
												  		0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3,
												  		4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7,
												  		4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7,
												  		4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7,
												  		4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7,
												  		4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7 };
	// Теперь создадим массив,
	// индексы -- это зоны,
	// элементы -- принадлежность конкретному игроку
	public static int[] belonging_to_player = new int[] {1, 0, 0, 0, 0, 0, 0, 2};

	void Start () {
		white_square = (Texture)Resources.Load("white_square");
		green_square = (Texture)Resources.Load("green_square");
		red_square = (Texture)Resources.Load("red_square");
	}

	void OnGUI () {
		// Выводим кнопки-шестиугольники с неким константным сдвигом cell_size
		for (int i = 0; i < 280; ++i) {
			square_button = white_square;
			if (belonging_to_player [array_of_hexagons [i]] == 1)
				square_button = red_square;
			if (belonging_to_player [array_of_hexagons [i]] == 2)
				square_button = green_square;
			
			// Rect (horizontal, vertical, width, height)
			GUI.DrawTexture (new Rect (shift_horizontally + cell_size * (i % number_cells_x), shift_vertically + cell_size * (i / number_cells_x), cell_size, cell_size), square_button);
		}
	}
}
