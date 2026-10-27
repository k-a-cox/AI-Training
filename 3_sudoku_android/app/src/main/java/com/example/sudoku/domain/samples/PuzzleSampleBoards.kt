package com.example.sudoku.domain.samples

import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.state.SudokuCellState

/**
 * Boards with 30-50% of cells filled, none of which conflict in a row, column, or box.
 * Values are not locked as givens, so they can be used to exercise the confirm-givens flow.
 */
object PuzzleSampleBoards {

    private fun boardFromGrid(grid: String): SudokuBoardState {
        val symbols = grid.filterNot { it.isWhitespace() }
        require(symbols.length == 81) { "Grid must have 81 cells." }

        return SudokuBoardState(
            cells = symbols.mapIndexed { index, symbol ->
                SudokuCellState(index = index, value = symbol.digitToIntOrNull())
            }
        )
    }

    // 38 values (47%), exactly one solution.
    val uniqueEasyBoard = boardFromGrid(
        """
        12.4..7.9
        ...7891.3
        ..9...4.6
        2....789.
        .6...1.3.
        .9.23.5.7
        34.6..9.2
        6789.....
        9.23...7.
        """
    )

    // 34 values (42%), exactly one solution.
    val uniqueMediumBoard = boardFromGrid(
        """
        .2..5..8.
        .56..91..
        ..91.34..
        2.4..7891
        ...8.....
        .91..4.6.
        .45...91.
        .78....45
        91....67.
        """
    )

    // 30 values (37%), exactly one solution.
    val uniqueSparseBoard = boardFromGrid(
        """
        123.....9
        4......23
        .8....4..
        ..45..8..
        .6..9.2..
        ..1.34...
        .4...89..
        ....12345
        ..23..67.
        """
    )

    // 36 values (44%), conflict-free but has more than one solution.
    val multipleSolutionsBoard = boardFromGrid(
        """
        12.45...9
        .56.89..3
        ..9...4.6
        23..67...
        ...8.....
        8.12..5.7
        .4..789..
        ...912.4.
        ..23.567.
        """
    )

    // 39 values (48%), conflict-free but has no solution.
    val noSolutionBoard = boardFromGrid(
        """
        1254..7.9
        ...7891.3
        ..9...4.6
        2....789.
        .6...1.3.
        .9.23.5.7
        34.6..9.2
        6789.....
        9.23...7.
        """
    )
}
