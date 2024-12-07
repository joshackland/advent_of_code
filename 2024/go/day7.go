package main

import (
	"fmt"
	"strconv"
	"strings"
)

func day7_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	var evaluate func(target int, numbers []int, index int, currentResult int) bool
	evaluate = func(target int, numbers []int, index int, currentResult int) bool {
		if index >= len(numbers) {
			return currentResult == target
		}

		currentNumber := numbers[index]

		newResult := currentResult + currentNumber
		if newResult <= target {
			if evaluate(target, numbers, index+1, newResult) {
				return true
			}
		}

		newResult = currentResult * currentNumber
		if newResult <= target {
			return evaluate(target, numbers, index+1, newResult)
		}

		return false
	}

	for _, line := range lines {
		parts := strings.Split(line, ":")
		target, _ := strconv.Atoi(parts[0])
		numberStrings := strings.Fields(parts[1])
		numbers := make([]int, len(numberStrings))

		for i, numString := range numberStrings {
			numbers[i], _ = strconv.Atoi(numString)
		}

		if evaluate(target, numbers, 0, 0) {
			output += target
		}
	}

	fmt.Println("Output Day 7 Part 1", output)
}

func day7_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	var evaluate func(target int, numbers []int, index int, currentResult int) bool
	evaluate = func(target int, numbers []int, index int, currentResult int) bool {
		if index >= len(numbers) {
			return currentResult == target
		}

		currentNumber := numbers[index]

		newResult := currentResult + currentNumber
		if newResult <= target {
			if evaluate(target, numbers, index+1, newResult) {
				return true
			}
		}

		newResult = currentResult * currentNumber
		if newResult <= target {
			if evaluate(target, numbers, index+1, newResult) {
				return true
			}
		}

		concatenated, _ := strconv.Atoi(fmt.Sprintf("%d%d", currentResult, currentNumber))
		if concatenated <= target {
			correct := evaluate(target, numbers, index+1, concatenated)
			return correct
		}

		return false
	}

	for _, line := range lines {
		parts := strings.Split(line, ":")
		target, _ := strconv.Atoi(parts[0])
		numberStrings := strings.Fields(parts[1])
		numbers := make([]int, len(numberStrings))

		for i, numString := range numberStrings {
			numbers[i], _ = strconv.Atoi(numString)
		}

		if evaluate(target, numbers, 0, 0) {
			output += target
		}
	}

	fmt.Println("Output Day 7 Part 2", output)
}
