package com.example.sudoku.domain.validators

import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.state.SudokuBoardState

object GivensValidator {

    fun validateGivens(boardState: SudokuBoardState): Boolean {
        if (!isStartingEmpty(boardState)) return false
        if (!validateGrid(boardState)) return false


        return true
    }

    fun isStartingEmpty(boardState: SudokuBoardState): Boolean {
        for ((_, _, isGiven) in boardState.cells) {
            if (isGiven)
                return false
        }
        return true
    }

    fun validateGrid(boardState: SudokuBoardState): Boolean {
        return ValidateGrid.validateGrid(boardState, needsToBeFull = false) == IsValidBoard.Valid
    }

    fun lockInGivens(boardState: SudokuBoardState): SudokuBoardState {
        val lockedCells = boardState.cells.map { cell ->
            if (cell.value != null) cell.copy(isGiven = true) else cell
        }
        return boardState.copy(cells = lockedCells, phase = GamePhase.Playing)
    }

}