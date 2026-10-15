package com.example.sudoku

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import com.example.sudoku.domain.SudokuBoardState
import com.example.sudoku.ui.board.SudokuGrid
import com.example.sudoku.ui.board.getSampleBoard
import com.example.sudoku.ui.theme.SudokuTheme

class MainActivity : ComponentActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
//        enableEdgeToEdge()
        setContent {

            var board by remember { mutableStateOf(SudokuBoardState()) }

            board = getSampleBoard()

            SudokuTheme {
                SudokuGrid(board, onCellSelected = { index ->
                    board = board.selectCell(index)
                })
            }
        }
    }
}
