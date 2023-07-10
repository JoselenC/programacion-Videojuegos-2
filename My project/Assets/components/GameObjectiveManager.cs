using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectiveManager : MonoBehaviour
{
    [SerializeField] private Text objective;

    void Start()
    {
        objective.text = "El juego 'Cosmic Conquest' es un emocionante Space Shooter 2D clásico de tipo endless autogenerado. Sumérgete en una trepidante aventura espacial donde te encuentras en constante movimiento hacia adelante mientras te enfrentas a una variedad de desafíos. Tu objetivo principal es sobrevivir y acumular la mayor puntuación posible. " ;
    }

    void Update()
    {
        
    }
}
