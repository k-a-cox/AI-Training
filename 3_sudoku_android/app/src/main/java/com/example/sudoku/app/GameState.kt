package com.example.sudoku.app

import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.validators.GivensValidator

data class GameState(
    val board: SudokuBoardState = SudokuBoardState(),
    val boardPhase: GamePhase = GamePhase.EnteringGivens,
    val notesOn: Boolean = false,
    val showLockGivensDialog: Boolean = false,
) {
//    fun togglePhase(): GameState = copy(
//        boardPhase = when (boardPhase) {
//            GamePhase.EnteringGivens -> GamePhase.Playing
//            GamePhase.Playing -> GamePhase.Solved
//            GamePhase.Solved -> GamePhase.EnteringGivens
//        }
//    )

    fun lockInGivens(): GameState = copy(
        board = GivensValidator.lockInGivens(board),
        boardPhase = GamePhase.Playing,
        showLockGivensDialog = false
    )
}
