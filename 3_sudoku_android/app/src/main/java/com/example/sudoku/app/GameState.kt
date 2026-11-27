package com.example.sudoku.app

import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.validators.GivensValidator

data class GameState(
    val board: SudokuBoardState = SudokuBoardState(),
    val boardPhase: GamePhase = GamePhase.EnteringGivens,
    val notesOn: Boolean = false,
    val cellsToHighlight: Set<Int> = mutableSetOf(),
    val showLockGivensDialog: Boolean = false,
    val showSubmitBoardDialog: Boolean = false,
) {

    fun lockInGivens(): GameState = copy(
        board = GivensValidator.lockInGivens(board),
        boardPhase = GamePhase.Playing,
        cellsToHighlight = emptySet(),
        showLockGivensDialog = false,
    )

    fun lockBoard(): GameState = copy(
        boardPhase = GamePhase.Solved,
        cellsToHighlight = emptySet(),
        showSubmitBoardDialog = false,
    )

    fun newGame(): GameState = copy(
        board = SudokuBoardState(),
        boardPhase = GamePhase.EnteringGivens,
        notesOn = false,
        cellsToHighlight = emptySet(),
        showLockGivensDialog = false,
        showSubmitBoardDialog = false,
    )
}
