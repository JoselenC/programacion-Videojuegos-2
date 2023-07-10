using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoManager : MonoBehaviour
{
    [SerializeField] private Text developerInfo;
    void Start()
    {
        developerInfo.text = "Estudiantes: Joselen Cecilia (233552) - Chia Hung Hsieh (196306) \n" +"\n" +
                             "Curso: Programación de Videojuegos\n" + "\n" +
                             "Profesor: Ariel Coppes";
        
    }

}
