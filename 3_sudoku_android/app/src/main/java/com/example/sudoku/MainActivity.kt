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
import com.example.sudoku.app.GameState
import com.example.sudoku.app.reduce
import com.example.sudoku.domain.validators.GivensValidator
import com.example.sudoku.domain.validators.SubmitBoardValidator
import com.example.sudoku.ui.board.SudokuGrid
import com.example.sudoku.ui.controls.ControlBoard
import com.example.sudoku.ui.feedback.BoardValidationPrompt
import com.example.sudoku.ui.feedback.LockGivens
import com.example.sudoku.ui.theme.SudokuTheme

class MainActivity : ComponentActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {

            var state by remember { mutableStateOf(GameState()) }

            SudokuTheme {
                Box(
                    modifier = Modifier
                        .fillMaxSize()
                        .padding(32.dp)
                ) {
                    Column(horizontalAlignment = Alignment.CenterHorizontally) {
                        SudokuGrid(state.board, onCellSelected = { index ->
                            state = state.copy(board = state.board.selectCell(index))
                        })
                        Box(modifier = Modifier.height(16.dp))
                        ControlBoard(
                            gamePhase = state.boardPhase,
                            showNotes = state.notesOn,
                            onAction = { action -> state = state.reduce(action) })
                    }

                    if (state.showLockGivensDialog) {
                        LockGivens(
                            isValid = GivensValidator.validateGivens(state.board),
                            validationAnswer = GivensValidator.validateGrid(
                                state.board
                            ),
                            onConfirm = {
                                state = state.lockInGivens()
                            },
                            onDismiss = {
                                state = state.copy(showLockGivensDialog = false)
                            })
                    }

                    if (state.showSubmitBoardDialog) {
                        val validationAnswer = SubmitBoardValidator().validateSolution(state.board)
                        BoardValidationPrompt(validationAnswer, onConfirm = {
                            state = state.newGame()
                        }, onValidDismiss = {
                            state = state.lockBoard()
                        }, onInvalidDismiss = {
                            state = state.copy(showSubmitBoardDialog = false)
                        })
                    }
                }
            }
        }
    }
}
