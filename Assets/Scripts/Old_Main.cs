using UnityEngine;
using System.Collections;

public class Old_Main : MonoBehaviour {

	private Texture hexagon_button;
	// Use this for initialization
	void Start () {
		hexagon_button = (Texture)Resources.Load("hexagon");
		// создаем матрицу кнопок, сверху которых будут спрайты-шестиугольники
		// каждая ячейка матрицы обозначает принадлежность к той или иной "зоне"
		// зона -- это то, что может захватывать игрок
		int[][] array_of_hexagons = new int[][] { new int[] {0, 0, 1, 1, 1},
			new int[] {0, 0, 1, 1, 2},
			new int[] {0, 3, 3, 2, 2},
			new int[] {3, 3, 3, 2, 2} };

		// теперь создадим массив
		// индексы -- это зоны
		// элементы -- принадлежность конкретному игроку
		int[] belonging_to_player = new int[] {1, 0, 2, 0};

		// еще один массив
		// индексы -- игрок
		// элементы -- цвет

		//int[] player_colors = new int[] {white, red, blue};
	}

	// Update is called once per frame
	void Update () {

	}

	// OnGUI
	void OnGUI () {
		// выводим кнопки-шестиугольники с неким константным сдвигом
		int step = 115;
		for (int i = 0; i < 20; ++i) {
			// Rect(horizontal, vertical, width, height)
			GUI.Button (new Rect (step * (i % 5), step * (i / 5), step - 10, step), hexagon_button);
		}
	}
}
