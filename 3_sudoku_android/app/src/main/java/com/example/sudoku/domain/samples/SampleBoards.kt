package com.example.sudoku.domain.samples

import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.state.SudokuCellState

object SampleBoards {
    private fun boardOf(valueAt: (index: Int) -> Int?) = SudokuBoardState(
        cells = List(81) { index -> SudokuCellState(index = index, value = valueAt(index)) }
    )

    val emptyBoard = SudokuBoardState()

    val rowGoodBoard = boardOf { index -> index % 9 + 1 }

    val columnGoodBoard = boardOf { index -> index / 9 + 1 }

    val boxGoodBoard = boardOf { index -> ((index / 9) % 3) * 3 + ((index % 9) % 3) + 1 }

    val invalidColumnBoard = boardOf { index -> ((index % 9) + (index / 9) * 3) % 9 + 1 }

    val invalidRowBoard = boardOf { index -> ((index * 3 % 9) + (index / 9)) % 9 + 1 }

    val invalidBoxBoard = boardOf { index -> ((index % 9) + (index / 9) + (index % 9)) % 9 + 1 }

    val validFullBoard = boardOf { index ->
        ((index % 9) + ((index / 9) * 3) + ((index / 9) / 3)) % 9 + 1
    }

    private val partialValues = mapOf(
        0 to 5, 1 to 3, 4 to 7, 8 to 2,
        9 to 6, 11 to 2, 13 to 1, 17 to 8,
        18 to 1, 20 to 8, 24 to 7,
        27 to 8, 30 to 6, 35 to 3,
        36 to 4, 40 to 5, 44 to 1,
        54 to 7, 58 to 1, 62 to 6,
        72 to 3, 76 to 8, 80 to 9
    )

    private val partialGivenIndices = setOf(
        0, 4, 11, 18, 30, 40, 54, 76
    )

    private val partialNotes = mapOf(
        3 to setOf(2, 4, 7),
        5 to setOf(3, 6, 8),
        6 to setOf(2, 5),
        10 to setOf(4, 7, 8),
        12 to setOf(2, 3, 6),
        14 to setOf(5, 8),
        16 to setOf(2, 4, 6),
        19 to setOf(3, 5, 7),
        21 to setOf(2, 6, 8),
        22 to setOf(4, 5),
        25 to setOf(3, 7, 8),
        26 to setOf(2, 5, 6)
    )

    val partialGameBoard = SudokuBoardState(
        cells = List(81) { index ->
            SudokuCellState(
                index = index,
                value = partialValues[index],
                isGiven = index in partialGivenIndices,
                notes = partialNotes[index].orEmpty()
            )
        },
        selectedCell = 39,
        phase = com.example.sudoku.domain.GamePhase.Playing
    )
}
