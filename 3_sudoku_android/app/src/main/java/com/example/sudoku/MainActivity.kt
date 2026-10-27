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
import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.samples.PuzzleSampleBoards
import com.example.sudoku.domain.samples.SampleBoards
import com.example.sudoku.domain.validators.GivensValidator
import com.example.sudoku.ui.board.SudokuGrid
import com.example.sudoku.ui.controls.ControlBoard
import com.example.sudoku.ui.controls.SampleBoardPicker
import com.example.sudoku.ui.feedback.LockGivens
import com.example.sudoku.ui.theme.SudokuTheme

class MainActivity : ComponentActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {

            var board by remember { mutableStateOf(SampleBoards.emptyBoard) }
            var notesOn by remember { mutableStateOf(false) }
            var showLockGivensDialog by remember { mutableStateOf(false) }

            SudokuTheme {
                Box(
                    modifier = Modifier
                        .fillMaxSize()
                        .padding(32.dp)
                ) {
                    Column(horizontalAlignment = Alignment.CenterHorizontally) {
                        SampleBoardPicker(
                            boards = PuzzleSampleBoards.all,
                            onBoardSelected = { board = it },
                        )
                        SudokuGrid(board, onCellSelected = { index ->
                            board = board.selectCell(index)
                        })
                        Box(modifier = Modifier.height(32.dp))
                        ControlBoard(
                            onDigitClick = { digit ->
                                board = board.setValue(digit)
                            }, onEraserClick = {
                                board = board.setValue(null)
                            }, onNotesClick = {
                                notesOn = !notesOn
                            }, onPhaseGameClick = {
                                when (board.phase) {
                                    GamePhase.EnteringGivens -> {
                                        showLockGivensDialog = true
                                    }

                                    GamePhase.Playing -> {
                                        // Handle playing phase logic
                                    }

                                    GamePhase.Solved -> {
                                        // Handle solved phase logic
                                    }
                                }
                            }, gamePhase = board.phase, showNotes = notesOn
                        )
                    }

                    if (showLockGivensDialog) {
                        LockGivens(isValid = GivensValidator.validateGivens(board), onConfirm = {
                            board = GivensValidator.lockInGivens(board)
                            showLockGivensDialog = false
                        }, onDismiss = {
                            showLockGivensDialog = false
                        })
                    }
                }
            }
        }
    }
}
