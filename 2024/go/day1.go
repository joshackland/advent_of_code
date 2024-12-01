package main

import (
	"fmt"
	"sort"
	"strconv"
	"strings"
)

func day1_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")

	var left []int
	var right []int

	for _, line := range lines {
		parts := strings.Fields(line)

		leftNum, _ := strconv.Atoi(parts[0])
		rightNum, _ := strconv.Atoi(parts[1])

		left = append(left, leftNum)
		right = append(right, rightNum)
	}

	sort.Ints(left)
	sort.Ints(right)

	totalDistance := 0

	for i := 0; i < len(left); i++ {
		distance := left[i] - right[i]

		if distance < 0 {
			distance *= -1
		}

		totalDistance += distance
	}

	fmt.Println("Output Day 1 Part 1", totalDistance)
}

func day1_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")

	var left []int
	var right []int

	for _, line := range lines {
		parts := strings.Fields(line)

		leftNum, _ := strconv.Atoi(parts[0])
		rightNum, _ := strconv.Atoi(parts[1])

		left = append(left, leftNum)
		right = append(right, rightNum)
	}

	rightMap := make(map[int]int)

	for _, num := range right {
		rightMap[num]++
	}

	similarityScore := 0

	for _, num := range left {
		similarityScore += num * rightMap[num]
	}

	fmt.Println("Output Day 1 Part 2", similarityScore)
}
