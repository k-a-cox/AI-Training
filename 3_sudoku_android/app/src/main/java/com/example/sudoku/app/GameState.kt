package com.example.sudoku.app

import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.validators.GivensValidator

data class GameState(
    val board: SudokuBoardState = SudokuBoardState(),
    val boardPhase: GamePhase = GamePhase.EnteringGivens,
    val notesOn: Boolean = false,
    val showLockGivensDialog: Boolean = false,
    val showSubmitBoardDialog: Boolean = false,
) {

    fun lockInGivens(): GameState = copy(
        board = GivensValidator.lockInGivens(board),
        boardPhase = GamePhase.Playing,
        showLockGivensDialog = false
    )

    fun lockBoard(): GameState = copy(
        boardPhase = GamePhase.Solved,
        showSubmitBoardDialog = false
    )

    fun newGame(): GameState = copy(
        board = SudokuBoardState(),
        boardPhase = GamePhase.EnteringGivens,
        notesOn = false,
        showLockGivensDialog = false,
        showSubmitBoardDialog = false
    )
}
