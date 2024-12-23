package main

import (
	"fmt"
	"sort"
	"strings"
)

func day23_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	connected := make(map[string]map[string]bool)

	tComps := make(map[string]bool)

	for _, line := range lines {
		parts := strings.Split(line, "-")
		comp1 := parts[0]
		comp2 := parts[1]

		if _, exists1 := connected[comp1]; !exists1 {
			connected[comp1] = make(map[string]bool)
		}
		if _, exists2 := connected[comp2]; !exists2 {
			connected[comp2] = make(map[string]bool)
		}
		connected[comp1][comp2] = true
		connected[comp2][comp1] = true

		if strings.HasPrefix(comp1, "t") {
			tComps[comp1] = true
		}
		if strings.HasPrefix(comp2, "t") {
			tComps[comp2] = true
		}
	}

	previousCombo := make(map[string]bool)

	for tComp := range tComps {
		currentCombo := []string{"", "", ""}
		currentCombo[0] = tComp

		for comp := range connected[tComp] {
			currentCombo[1] = comp

			for comp3 := range connected[comp] {
				currentCombo[2] = comp3

				if !connected[comp3][tComp] {
					continue
				}

				sortedCombo := []string{
					currentCombo[0],
					currentCombo[1],
					currentCombo[2],
				}
				sort.Strings(sortedCombo)
				combo := sortedCombo[0] + sortedCombo[1] + sortedCombo[2]

				if !previousCombo[combo] {
					output++
					previousCombo[combo] = true
				}
			}
		}
	}

	fmt.Println("Output Day 23 Part 1", output)
}

func day23_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := ""

	connected := make(map[string]map[string]bool)

	for _, line := range lines {
		parts := strings.Split(line, "-")
		comp1 := parts[0]
		comp2 := parts[1]

		if _, exists1 := connected[comp1]; !exists1 {
			connected[comp1] = make(map[string]bool)
		}
		if _, exists2 := connected[comp2]; !exists2 {
			connected[comp2] = make(map[string]bool)
		}
		connected[comp1][comp2] = true
		connected[comp2][comp1] = true
	}

	isLAN := func(group []string) bool {
		for i := 0; i < len(group); i++ {
			for j := i + 1; j < len(group); j++ {
				if !connected[group[i]][group[j]] {
					return false
				}
			}
		}
		return true
	}

	largestGroup := []string{}
	cache := make(map[string]bool)

	for comp := range connected {
		queue := [][]string{{comp}}

		for len(queue) > 0 {
			currentGroup := queue[0]
			queue = queue[1:]

			if isLAN(currentGroup) {
				if len(currentGroup) > len(largestGroup) {
					largestGroup = currentGroup
				}

				for neighbour := range connected[currentGroup[len(currentGroup)-1]] {
					isNotInGroup := true
					for _, c := range currentGroup {
						if c == neighbour {
							isNotInGroup = false
							break
						}
					}

					if isNotInGroup {
						newGroup := append([]string{}, currentGroup...)
						newGroup = append(newGroup, neighbour)
						sort.Strings(newGroup)
						stringGroup := strings.Join(newGroup, "")
						if !cache[stringGroup] {
							cache[stringGroup] = true
							queue = append(queue, newGroup)
						}
					}
				}
			}
		}
	}

	sort.Strings(largestGroup)
	output = strings.Join(largestGroup, ",")

	fmt.Println("Output Day 23 Part 2", output)
}
