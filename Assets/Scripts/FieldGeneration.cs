using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldGeneration : MonoBehaviour {
    // Переприсвоим переменные из другого класса, чтобы меньше писать потом
    private int rows = FieldCreation.number_cells_y; // == 10
    private int columns = FieldCreation.number_cells_x; // == 28
    private int number_of_zones = FieldCreation.belonging_to_player.Length;

    public static List<List <int> > field = new List<List <int> > ();

    void Start () {
        // Заполняем поле листами (рядами)
        for (int i = 0; i < rows; i++) {
            field.Add(new List<int>());
        }
        // Заполняем поле -1
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                field[i].Add(-1);
            }
        }

        // Запоняем поле восемью прямоугольниками 4x4 (основа для поля)
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                //bool i_is_less_than_four = (i < 4) ? true : false;
                //if ()
                if ((i < 4 || i > 5) && ((j / 4) % 2 == 0)) {
                    int owner = (j / 2) / 4;
                    owner += (i / 6) * 4;
                    field[i][j] = owner;
                }
            }
        }

        // Проверяем правильно ли все сделано
        string test_string = "";
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                test_string += field[i][j] + " ";
            }
            test_string += "\n";
        }
        Debug.Log(test_string);
    }

    void OnGUI () {
        
    }
}
