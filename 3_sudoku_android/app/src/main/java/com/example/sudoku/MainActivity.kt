package com.example.sudoku

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.SudokuBoardState
import com.example.sudoku.ui.board.SudokuGrid
import com.example.sudoku.ui.controls.ControlBoard
import com.example.sudoku.ui.theme.SudokuTheme

class MainActivity : ComponentActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {

            //var board by remember { mutableStateOf(getSampleBoard()) }
            var board by remember { mutableStateOf(SudokuBoardState()) }

            SudokuTheme {
                Box(
                    modifier = Modifier
                        .fillMaxSize()
                        .padding(64.dp)
                ) {
                    Column(horizontalAlignment = Alignment.CenterHorizontally) {
                        SudokuGrid(board, onCellSelected = { index ->
                            board = board.selectCell(index)
                        })
                        Box(modifier = Modifier.height(32.dp))
                        ControlBoard(onDigitClick = { digit ->
                            board = board.setValue(digit)
                        }, onEraserClick = {
                            board = board.setValue(null)
                        })
                    }
                }
            }
        }
    }
}
