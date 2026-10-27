package com.example.sudoku.app

import com.example.sudoku.domain.state.SudokuBoardState

data class GameState(
    val board: SudokuBoardState = SudokuBoardState(),
    val notesOn: Boolean = false,
    val showLockGivensDialog: Boolean = false,
)
