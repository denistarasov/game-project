using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	private Vector2 click_coordinates;

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			// Находим место, куда кликнул игрок, точка отсчета -- левый нижний угол
			click_coordinates = Input.mousePosition;

			/*// Переведем это значение в систему координат с отсчетом в левом верхнем углу
			click_coordinates.y -= Screen.height;
			Debug.Log ("click_coord x == " + click_coordinates.x.ToString());
			Debug.Log ("click_coord y == " + click_coordinates.y.ToString());


			// Учитываем сдвиг самого поля (сдвиг для выравнивания по центру)
			click_coordinates.x += FieldCreation.shift_horizontally;
			click_coordinates.y -= FieldCreation.shift_vertically;
			*/

			// НАДО ПЕРЕДЕЛАТЬ УЧИТЫВАЯ НАЧАЛА КООРДИНАТ В ЛЕВОМ НИЖНЕМ УГЛУ

			// Получаем координаты клетки, куда кликнул игрок
			click_coordinates.x = click_coordinates.x / FieldCreation.cell_size;
			click_coordinates.y = click_coordinates.y / FieldCreation.cell_size;
			int coordinates_in_array = (int)click_coordinates.y * FieldCreation.number_cells_y + (int)click_coordinates.x;

			// Ищем зону
			int zone_number = FieldCreation.belonging_to_player [FieldCreation.array_of_hexagons [coordinates_in_array] ];
			// Теперь ее владелец -- игрок
			FieldCreation.belonging_to_player [zone_number] = 1;

			Debug.Log ("zone_number == " + zone_number.ToString ());


		}
	}
}
