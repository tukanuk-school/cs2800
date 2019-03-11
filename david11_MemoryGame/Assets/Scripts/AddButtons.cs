using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{

    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    public static int NumCards { get; set; }

    private void Awake()
    {
        // give NumCards an initial value for scene testing
        if (NumCards == 0 ) NumCards = 2;

        for (int i = 0; i < NumCards; ++i)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }

    }

    //internal static void NumCards(float slideValue)
    //{
    //    throw new NotImplementedException();
    //}
}
