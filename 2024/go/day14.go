package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

type robot struct {
	x  int
	y  int
	vx int
	vy int
}

func day14_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	width, height := 101, 103
	seconds := 100

	robots := []robot{}

	for _, line := range lines {
		var x, y, vx, vy int
		fmt.Sscanf(line, "p=%d,%d v=%d,%d", &x, &y, &vx, &vy)
		r := robot{
			x,
			y,
			vx,
			vy,
		}
		robots = append(robots, r)
	}

	for i := range robots {
		robots[i].x = (robots[i].x + (robots[i].vx * seconds)) % width
		robots[i].y = (robots[i].y + (robots[i].vy * seconds)) % height

		for robots[i].x < 0 {
			robots[i].x += width
		}

		for robots[i].y < 0 {
			robots[i].y += height
		}
	}

	quadrants := [4]int{}

	for _, r := range robots {
		if r.x < width/2 && r.y < height/2 {
			quadrants[0]++
		} else if r.x < width/2 && r.y > height/2 {
			quadrants[1]++
		} else if r.x > width/2 && r.y < height/2 {
			quadrants[2]++
		} else if r.x > width/2 && r.y > height/2 {
			quadrants[3]++
		}
	}

	output = quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3]

	fmt.Println("Output Day 14 Part 1", output)
}

func day14_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	width, height := 101, 103

	robots := []robot{}

	for _, line := range lines {
		var x, y, vx, vy int
		fmt.Sscanf(line, "p=%d,%d v=%d,%d", &x, &y, &vx, &vy)
		r := robot{
			x,
			y,
			vx,
			vy,
		}
		robots = append(robots, r)
	}

	for n := 1; n < 1000000000; n++ {
		grid := make([][]string, height)
		for j := 0; j < height; j++ {
			grid[j] = make([]string, width)
		}
		locations := make(map[[2]int]bool)
		duplicate := false
		for i := range robots {
			robots[i].x = (robots[i].x + robots[i].vx) % width
			robots[i].y = (robots[i].y + robots[i].vy) % height

			for robots[i].x < 0 {
				robots[i].x += width
			}

			for robots[i].y < 0 {
				robots[i].y += height
			}
			if locations[[2]int{robots[i].y, robots[i].x}] {
				duplicate = true
				continue
			}
			locations[[2]int{robots[i].y, robots[i].x}] = true
			grid[robots[i].y][robots[i].x] = "#"
		}
		if duplicate {
			continue
		}
		fmt.Printf("@@@@@@@@@@@@@@@@%d@@@@@@@@@@@@@@@@\n", n)
		for _, row := range grid {
			fmt.Println(row)
		}

		scanner := bufio.NewScanner(os.Stdin)
		scanner.Scan()
	}

	fmt.Println("Output Day 14 Part 2", output)
}
