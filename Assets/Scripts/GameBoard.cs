using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    List<BoardNumber> _board;
    // Start is called before the first frame update
    void Start()
    {
        _board = GameObject.FindObjectsByType<BoardNumber>(FindObjectsSortMode.None).ToList().OrderBy(x => x.Number).ToList();
        foreach (BoardNumber number in _board) 
        {
            number.OnButtonClicked += Number_OnButtonClicked;
        }
    }

    private void Number_OnButtonClicked(int number)
    {
        //open popup for this number
        //process bets
        var boardNumber = _board[number];
    }
}
