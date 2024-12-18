package main

import (
	"fmt"
	"strings"
)

type Point struct {
	x, y int
}

type StateD16 struct {
	position  Point
	direction int
	score     int
	tiles     []Point
}

var directions = []Point{
	{0, -1},
	{1, 0},
	{0, 1},
	{-1, 0},
}

func day16_1(input string) {
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

	previousPositions := make(map[[3]int]bool)

	queue := []StateD16{StateD16{start, 1, 0, []Point{}}}

	lowestState := func() StateD16 {
		min := StateD16{Point{0, 0}, 0, -1, []Point{}}
		index := -1

		for i, s := range queue {
			if min.score == -1 || s.score < min.score {
				min = s
				index = i
			}
		}

		queue = append(queue[:index], queue[index+1:]...)

		return min
	}

	for {
		currentState := lowestState()

		if currentState.position == end {
			output = currentState.score
			break
		}

		if previousPositions[[3]int{currentState.position.y, currentState.position.x, currentState.direction}] {
			continue
		}

		previousPositions[[3]int{currentState.position.y, currentState.position.x, currentState.direction}] = true

		leftDirKey := (currentState.direction - 1)
		if leftDirKey < 0 {
			leftDirKey += 4
		} else {
			leftDirKey %= 4
		}

		currentLeft := StateD16{currentState.position, leftDirKey, currentState.score, []Point{}}
		currentRight := StateD16{currentState.position, ((currentState.direction + 1) % 4), currentState.score, []Point{}}

		currentDir := directions[currentState.direction]
		leftDir := directions[currentLeft.direction]
		rightDir := directions[currentRight.direction]

		if grid[currentLeft.position.y+leftDir.y][currentLeft.position.x+leftDir.x] != '#' {
			currentLeft.position = Point{currentLeft.position.x, currentLeft.position.y}
			currentLeft.score += 1000

			queue = append(queue, currentLeft)
		}

		if grid[currentRight.position.y+rightDir.y][currentRight.position.x+rightDir.x] != '#' {
			currentRight.position = Point{currentRight.position.x, currentRight.position.y}
			currentRight.score += 1000

			queue = append(queue, currentRight)
		}

		if grid[currentState.position.y+currentDir.y][currentState.position.x+currentDir.x] != '#' {
			currentState.position = Point{currentState.position.x + currentDir.x, currentState.position.y + currentDir.y}
			currentState.score += 1

			queue = append(queue, currentState)
		}

	}

	fmt.Println("Output Day 16 Part 1", output)
}

func day16_2(input string) {
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

	previousPositions := make(map[[3]int]int)
	queue := []StateD16{{start, 1, 0, []Point{start}}}

	lowestState := func() StateD16 {
		min := StateD16{Point{0, 0}, 0, -1, []Point{}}
		index := -1

		for i, s := range queue {
			if min.score == -1 || s.score < min.score {
				min = s
				index = i
			}
		}

		queue = append(queue[:index], queue[index+1:]...)
		return min
	}

	minScore := -1
	bestTiles := make(map[Point]bool)

	for len(queue) > 0 {
		currentState := lowestState()

		if minScore != -1 && minScore < currentState.score {
			continue
		}

		if currentState.position == end {
			minScore = currentState.score
			for _, tile := range currentState.tiles {
				bestTiles[tile] = true
			}
			continue
		}

		if previousPositions[[3]int{currentState.position.y, currentState.position.x, currentState.direction}] != 0 && previousPositions[[3]int{currentState.position.y, currentState.position.x, currentState.direction}] < currentState.score {
			continue
		}

		previousPositions[[3]int{currentState.position.y, currentState.position.x, currentState.direction}] = currentState.score

		leftDirKey := (currentState.direction - 1)
		if leftDirKey < 0 {
			leftDirKey += 4
		} else {
			leftDirKey %= 4
		}

		currentLeft := StateD16{currentState.position, leftDirKey, currentState.score, currentState.tiles}
		currentRight := StateD16{currentState.position, ((currentState.direction + 1) % 4), currentState.score, currentState.tiles}

		currentDir := directions[currentState.direction]
		leftDir := directions[currentLeft.direction]
		rightDir := directions[currentRight.direction]

		if grid[currentLeft.position.y+leftDir.y][currentLeft.position.x+leftDir.x] != '#' {
			currentLeft.position = Point{currentLeft.position.x, currentLeft.position.y}
			currentLeft.score += 1000
			currentLeft.tiles = append([]Point{}, currentState.tiles...)

			queue = append(queue, currentLeft)
		}

		if grid[currentRight.position.y+rightDir.y][currentRight.position.x+rightDir.x] != '#' {
			currentRight.position = Point{currentRight.position.x, currentRight.position.y}
			currentRight.score += 1000
			currentRight.tiles = append([]Point{}, currentState.tiles...)

			queue = append(queue, currentRight)
		}

		if grid[currentState.position.y+currentDir.y][currentState.position.x+currentDir.x] != '#' {
			currentState.position = Point{currentState.position.x + currentDir.x, currentState.position.y + currentDir.y}
			currentState.score += 1
			currentState.tiles = append(currentState.tiles, currentState.position)

			queue = append(queue, currentState)
		}

	}

	output = len(bestTiles)

	fmt.Println("Output Day 16 Part 2", output)
}
