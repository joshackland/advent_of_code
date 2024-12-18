package main

import (
	"fmt"
	"strconv"
	"strings"
)

func day18_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0
	gridSize := 71
	totalCorrupted := 1024
	corrupted := make(map[Point]bool)
	directions := []Point{
		{0, -1},
		{1, 0},
		{0, 1},
		{-1, 0},
	}

	for i := 0; i < totalCorrupted; i++ {
		parts := strings.Split(lines[i], (","))
		x, _ := strconv.Atoi(parts[0])
		y, _ := strconv.Atoi(parts[1])
		corrupted[Point{x, y}] = true
	}

	start := Point{0, 0}
	end := Point{gridSize - 1, gridSize - 1}

	type State struct {
		position Point
		score    int
	}

	queue := []State{{start, 0}}
	visited := make(map[Point]bool)
	visited[start] = true

	var current State

	for len(queue) > 0 {
		current = queue[0]
		queue = queue[1:]

		if current.position == end {
			output = current.score
			break
		}

		for _, dir := range directions {
			next := Point{current.position.x + dir.x, current.position.y + dir.y}

			if next.x < 0 || next.x >= gridSize || next.y < 0 || next.y >= gridSize {
				continue
			}

			if corrupted[next] || visited[next] {
				continue
			}

			visited[next] = true

			queue = append(queue, State{next, current.score + 1})
		}
	}

	fmt.Println("Output Day 18 Part 2", output)
}

func day18_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := ""
	gridSize := 71
	totalCorrupted := 1024
	corrupted := make(map[Point]bool)
	directions := []Point{
		{0, -1},
		{1, 0},
		{0, 1},
		{-1, 0},
	}

	for i := 0; i < totalCorrupted; i++ {
		parts := strings.Split(lines[i], (","))
		x, _ := strconv.Atoi(parts[0])
		y, _ := strconv.Atoi(parts[1])
		corrupted[Point{x, y}] = true
	}

	start := Point{0, 0}
	end := Point{gridSize - 1, gridSize - 1}

	type State struct {
		position Point
		score    int
	}

	for i := totalCorrupted; i < len(lines); i++ {
		parts := strings.Split(lines[i], (","))
		x, _ := strconv.Atoi(parts[0])
		y, _ := strconv.Atoi(parts[1])

		corrupted[Point{x, y}] = true
		queue := []State{{start, 0}}
		visited := make(map[Point]bool)
		visited[start] = true

		var current State

		for len(queue) > 0 {
			current = queue[0]
			queue = queue[1:]

			if current.position == end {
				break
			}

			for _, dir := range directions {
				next := Point{current.position.x + dir.x, current.position.y + dir.y}

				if next.x < 0 || next.x >= gridSize || next.y < 0 || next.y >= gridSize {
					continue
				}

				if corrupted[next] || visited[next] {
					continue
				}

				visited[next] = true

				queue = append(queue, State{next, current.score + 1})
			}
		}

		if current.position != end {
			output = fmt.Sprintf("%d,%d", x, y)
			break
		}
	}

	fmt.Println("Output Day 18 Part 2", output)
}
