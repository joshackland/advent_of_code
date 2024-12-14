package main

import (
	"fmt"
	"strings"
)

func day13_1(input string) {
	machines := strings.Split(strings.TrimSpace(input), "\n\n")
	output := 0

	for _, machine := range machines {
		var ax, ay, bx, by, px, py int
		lines := strings.Split(machine, "\n")
		fmt.Sscanf(lines[0], "Button A: X+%d, Y+%d", &ax, &ay)
		fmt.Sscanf(lines[1], "Button B: X+%d, Y+%d", &bx, &by)
		fmt.Sscanf(lines[2], "Prize: X=%d, Y=%d", &px, &py)

		minCost := -1
		found := false

		for aPress := 0; aPress < 100; aPress++ {
			for bPress := 0; bPress < 100; bPress++ {
				if ax*aPress+bx*bPress == px && ay*aPress+by*bPress == py {
					cost := aPress*3 + bPress*1
					if !found || cost < minCost {
						minCost = cost
						found = true
					}
				}
			}
		}

		if found {
			output += minCost
		}
	}

	fmt.Println("Output Day 13 Part 1", output)
}

func day13_2(input string) {
	machines := strings.Split(strings.TrimSpace(input), "\n\n")
	output := 0

	for _, machine := range machines {
		var ax, ay, bx, by, px, py int
		lines := strings.Split(machine, "\n")
		fmt.Sscanf(lines[0], "Button A: X+%d, Y+%d", &ax, &ay)
		fmt.Sscanf(lines[1], "Button B: X+%d, Y+%d", &bx, &by)
		fmt.Sscanf(lines[2], "Prize: X=%d, Y=%d", &px, &py)

		px += 10000000000000
		py += 10000000000000

		det := ax*by - ay*bx

		if det == 0 {
			continue
		}

		aPress := (px*by - py*bx) / det
		bPress := (ax*py - ay*px) / det

		if ax*aPress+bx*bPress == px && ay*aPress+by*bPress == py && aPress >= 0 && bPress >= 0 {
			cost := aPress*3 + bPress
			output += cost
		}
	}

	fmt.Println("Output Day 13 Part 2:", output)
}
