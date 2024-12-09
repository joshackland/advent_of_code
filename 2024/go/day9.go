package main

import (
	"fmt"
	"strconv"
)

func day9_1(input string) {
	blocks := []rune{}
	isBlock := true
	fileId := 0

	for _, numRune := range input {
		num, _ := strconv.Atoi(string(numRune))

		if isBlock {
			for i := 0; i < num; i++ {
				blocks = append(blocks, rune('0'+fileId))
			}
			fileId++
		} else {
			for i := 0; i < num; i++ {
				blocks = append(blocks, '.')
			}
		}
		isBlock = !isBlock
	}

	emptyIndex := 0
	lastFileIndex := len(blocks) - 1

	for blocks[emptyIndex] != '.' {
		emptyIndex++
	}

	for blocks[lastFileIndex] == '.' {
		lastFileIndex--
	}

	for emptyIndex <= lastFileIndex {
		blocks[emptyIndex] = blocks[lastFileIndex]
		blocks[lastFileIndex] = '.'

		for blocks[emptyIndex] != '.' {
			emptyIndex++
		}

		for blocks[lastFileIndex] == '.' {
			lastFileIndex--
		}
	}

	blockId := 0
	output := 0

	for blocks[blockId] != '.' {
		output += blockId * int(blocks[blockId]-'0')
		blockId++
	}

	fmt.Println("Output Day 9 Part 1", output)
}

func day9_2(input string) {
	blocks := []rune{}
	isBlock := true
	fileId := 0

	for _, numRune := range input {
		num, _ := strconv.Atoi(string(numRune))

		if isBlock {
			for i := 0; i < num; i++ {
				blocks = append(blocks, rune('0'+fileId))
			}
			fileId++
		} else {
			for i := 0; i < num; i++ {
				blocks = append(blocks, '.')
			}
		}
		isBlock = !isBlock
	}

	for currentFile := fileId - 1; currentFile >= 0; currentFile-- {
		fileBlocks := []int{}

		for i, block := range blocks {
			if block == rune('0'+currentFile) {
				fileBlocks = append(fileBlocks, i)
			}
		}

		freeStart := -1
		freeLength := 0

		for i := 0; i < fileBlocks[0]; i++ {
			if blocks[i] != '.' {
				freeStart = -1
				freeLength = 0
				continue
			}

			if freeStart == -1 {
				freeStart = i
			}

			freeLength++

			if freeLength == len(fileBlocks) {
				break
			}
		}

		if freeLength == len(fileBlocks) {
			for i := 0; i < freeLength; i++ {
				blocks[fileBlocks[i]] = '.'
				blocks[freeStart+i] = rune('0' + currentFile)
			}
		}
	}

	output := 0

	for blockId := 0; blockId < len(blocks); blockId++ {
		if blocks[blockId] != '.' {
			output += blockId * int(blocks[blockId]-'0')
		}
	}

	fmt.Println("Output Day 9 Part 2", output)
}
