package main

import (
	"fmt"
	"strconv"
	"strings"
)

func day22_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	for _, line := range lines {
		secret, _ := strconv.Atoi(line)
		for i := 0; i < 2000; i++ {
			secret ^= secret << 6
			secret %= 16777216

			secret ^= secret >> 5
			secret %= 16777216

			secret ^= secret << 11
			secret %= 16777216
		}
		output += secret
	}

	fmt.Println("Output Day 22 Part 1", output)
}

func day22_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	secretDiff := make(map[[4]int]int)

	for _, line := range lines {
		secret, _ := strconv.Atoi(line)
		secrets := make([]int, 2000)
		secrets = append(secrets, secret)
		currentSecretDiffExist := make(map[[4]int]bool)

		for i := 0; i < 2000; i++ {
			secret ^= secret << 6
			secret %= 16777216

			secret ^= secret >> 5
			secret %= 16777216

			secret ^= secret << 11
			secret %= 16777216

			secrets[i] = secret

			if i >= 3 {
				firstNum := 0
				if i > 3 {
					firstNum = secrets[i-4] % 10
				}
				n1 := secrets[i-3] % 10
				n2 := secrets[i-2] % 10
				n3 := secrets[i-1] % 10
				n4 := secrets[i-0] % 10

				diff := [4]int{
					n1 - firstNum,
					n2 - n1,
					n3 - n2,
					n4 - n3,
				}

				if !currentSecretDiffExist[diff] {
					secretDiff[diff] += n4
					currentSecretDiffExist[diff] = true
				}
			}
		}
	}

	for _, value := range secretDiff {
		if value > output {
			output = value
		}
	}

	fmt.Println("Output Day 22 Part 2", output)
}
