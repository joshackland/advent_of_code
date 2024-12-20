package main

import (
	"fmt"
	"math"
	"strings"
)

func day20_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]rune, len(lines))
	output := 0

	var start, end Point

	for y, line := range lines {
		grid[y] = []rune(line)

		for x, char := range line {
			if char == 'S' {
				start = Point{x, y}
			} else if char == 'E' {
				end = Point{x, y}
			}
		}
	}

	directions := []Point{
		{0, -1},
		{1, 0},
		{0, 1},
		{-1, 0},
	}

	type StateD20 struct {
		position Point
		tiles    []Point
	}

	previousPositions := make(map[Point]bool)

	queue := []StateD20{{start, []Point{start}}}

	lowestState := func() StateD20 {
		min := StateD20{Point{0, 0}, []Point{}}
		index := -1

		for i, s := range queue {
			if len(min.tiles) == 0 || len(s.tiles) < len(min.tiles) {
				min = s
				index = i
			}
		}

		queue = append(queue[:index], queue[index+1:]...)

		return min
	}

	var shortestState StateD20

	for {
		currentState := lowestState()

		if currentState.position == end {
			shortestState = currentState
			break
		}

		for _, dir := range directions {
			nextPoint := Point{currentState.position.x + dir.x, currentState.position.y + dir.y}
			nextTiles := append(append([]Point{}, currentState.tiles...), nextPoint)
			nextState := StateD20{nextPoint, nextTiles}

			if previousPositions[nextState.position] {
				continue
			}

			if grid[nextState.position.y][nextState.position.x] == '#' {
				continue
			}

			previousPositions[nextState.position] = true
			queue = append(queue, nextState)
		}
	}

	totalTiles := len(shortestState.tiles) - 1
	maxTiles := totalTiles - 99
	correctTiles := map[Point]int{}

	for i, tile := range shortestState.tiles {
		correctTiles[tile] = i
	}

	cheated := make(map[int]int)

	for currentTile, _ := range correctTiles {
		for _, dir := range directions {
			nextPoint := Point{currentTile.x + dir.x, currentTile.y + dir.y}

			if nextPoint.y < 0 || nextPoint.y >= len(grid) || nextPoint.x < 0 || nextPoint.x >= len(grid[0]) {
				continue
			}

			if grid[nextPoint.y][nextPoint.x] != '#' {
				continue
			}

			for _, dir2 := range directions {
				nextPoint2 := Point{nextPoint.x + dir2.x, nextPoint.y + dir2.y}

				if nextPoint2.y < 0 || nextPoint2.y >= len(grid) || nextPoint2.x < 0 || nextPoint2.x >= len(grid[0]) {
					continue
				}

				if nextPoint2 == nextPoint || nextPoint2 == currentTile {
					continue
				}

				if grid[nextPoint2.y][nextPoint2.x] != '#' {
					currentTileCount := correctTiles[currentTile] + 2
					skipTileCount := correctTiles[nextPoint2]
					skipTotal := skipTileCount - currentTileCount
					finalTiles := totalTiles - skipTotal

					if finalTiles < maxTiles {
						cheated[skipTotal]++
					}
				}
			}
		}
	}

	for _, total := range cheated {
		output += total
	}

	fmt.Println("Output Day 20 Part 1", output)
}

func day20_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]rune, len(lines))
	output := 0

	var start, end Point

	for y, line := range lines {
		grid[y] = []rune(line)

		for x, char := range line {
			if char == 'S' {
				start = Point{x, y}
			} else if char == 'E' {
				end = Point{x, y}
			}
		}
	}

	directions := []Point{
		{0, -1},
		{1, 0},
		{0, 1},
		{-1, 0},
	}

	type StateD20 struct {
		position Point
		tiles    []Point
	}

	previousPositions := make(map[Point]bool)

	queue := []StateD20{{start, []Point{start}}}

	lowestState := func() StateD20 {
		min := StateD20{Point{0, 0}, []Point{}}
		index := -1

		for i, s := range queue {
			if len(min.tiles) == 0 || len(s.tiles) < len(min.tiles) {
				min = s
				index = i
			}
		}

		queue = append(queue[:index], queue[index+1:]...)

		return min
	}

	var shortestState StateD20

	for {
		currentState := lowestState()

		if currentState.position == end {
			shortestState = currentState
			break
		}

		for _, dir := range directions {
			nextPoint := Point{currentState.position.x + dir.x, currentState.position.y + dir.y}
			nextTiles := append(append([]Point{}, currentState.tiles...), nextPoint)
			nextState := StateD20{nextPoint, nextTiles}

			if previousPositions[nextState.position] {
				continue
			}

			if grid[nextState.position.y][nextState.position.x] == '#' {
				continue
			}

			previousPositions[nextState.position] = true
			queue = append(queue, nextState)
		}
	}

	totalTiles := len(shortestState.tiles) - 1
	maxTiles := totalTiles - 99
	correctTiles := map[Point]int{}

	for i, tile := range shortestState.tiles {
		correctTiles[tile] = i
	}

	cheated := make(map[int]int)

	for currentTile, _ := range correctTiles {
		for y := -20; y <= 20; y++ {
			maxX := int(20 - math.Abs(float64(y)))
			for x := -maxX; x <= maxX; x++ {
				nextPoint := Point{currentTile.x + x, currentTile.y + y}

				if nextPoint.y < 0 || nextPoint.y >= len(grid) || nextPoint.x < 0 || nextPoint.x >= len(grid[0]) {
					continue
				}

				if grid[nextPoint.y][nextPoint.x] == '#' {
					continue
				}

				currentTileCount := correctTiles[currentTile] + int(math.Abs(float64(y))) + int(math.Abs(float64(x)))
				skipTileCount := correctTiles[nextPoint]
				skipTotal := skipTileCount - currentTileCount
				finalTiles := totalTiles - skipTotal

				if finalTiles < maxTiles {
					cheated[skipTotal]++
				}
			}
		}

	}

	for _, total := range cheated {
		output += total
	}

	fmt.Println(cheated)

	fmt.Println("Output Day 20 Part 2", output)
}
