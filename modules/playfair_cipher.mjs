import { mod, toSet } from './common.mjs'
import {
  InvalidAlphabetError,
  InvalidKeywordError,
  InvalidSubstitutionSymbolError,
} from './errors.mjs'

export class PlayfairCipher {
  constructor(alphabet, keyword, substitutionSymbol) {
    const minAlphabetLength = 4
    const minKeywordLength = 4

    if (alphabet.length < minAlphabetLength) {
      throw new InvalidAlphabetError(
        `The alphabet must contain at least ${minAlphabetLength} symbols`
      )
    }

    if (
      Math.sqrt(alphabet.length) - Math.floor(Math.sqrt(alphabet.length)) >
      0
    ) {
      throw new InvalidAlphabetError(
        `The length of the alphabet must be a perfect square`
      )
    }

    const alphabetSet = toSet(alphabet)
    if (alphabet.length !== alphabetSet.size) {
      throw new InvalidAlphabetError(
        'The alphabet must not contain duplicate symbols'
      )
    }

    if (substitutionSymbol.length !== 1) {
      throw new InvalidSubstitutionSymbolError(
        `The substitution symbol must be a single character`
      )
    }
    if (!alphabetSet.has(substitutionSymbol)) {
      throw new InvalidSubstitutionSymbolError(
        `The alphabet must contain the substitution symbol ${substitutionSymbol}`
      )
    }
    this.substitutionSymbol = substitutionSymbol

    if (keyword && keyword.length !== toSet(keyword).size) {
      throw new InvalidKeywordError(
        `The key must not contain duplicate symbols`
      )
    }

    for (const s of keyword) {
      if (!alphabetSet.has(s)) {
        throw new InvalidKeywordError(
          `Provided alphabet doesn't contain the symbol ${s}`
        )
      } else {
        alphabetSet.delete(s)
      }
    }

    this.posToSymbols = {}
    this.symbolsToPos = {}

    const alphabetWithoutKeyword = [...alphabetSet].sort()
    this.squareSize = Math.round(
      Math.sqrt(keyword.length + alphabetWithoutKeyword.length)
    )
    let row = 0
    let col = 0

    for (const symbol of [...keyword].concat(alphabetWithoutKeyword)) {
      this.posToSymbols[[row, col]] = symbol
      this.symbolsToPos[symbol] = [row, col]
      col++

      if (col === this.squareSize) {
        col = 0
        row++
      }
    }
  }

  encode(message) {
    return this.createPairs(message)
      .filter((p) => p[0] in this.symbolsToPos && p[1] in this.symbolsToPos)
      .map((p) => this.encodePair(p))
      .map((p) => `${this.posToSymbols[p[0]]}${this.posToSymbols[p[1]]}`)
      .join('')
  }

  decode(encodedMessage) {
    return this.createPairs(encodedMessage)
      .filter((p) => p[0] in this.symbolsToPos && p[1] in this.symbolsToPos)
      .map((p) => this.decodePair(p))
      .map((p) => `${this.posToSymbols[p[0]]}${this.posToSymbols[p[1]]}`)
      .join('')
  }

  createPairs(text) {
    if (!text) return []

    text = text.replace(' ', '').split('')
    for (let i = 0; i < text.length; i++) {
      if (text[i] == text[i - 1] && text[i] != _substitutionSymbol) {
        text.splice(i, 0, this.substitutionSymbol)
      }
    }

    if (text.length % 2 !== 0) {
      text.push(this.substitutionSymbol)
    }

    const pairs = []
    for (let i = 0; i < text.length; i += 2) {
      pairs.push([text[i], text[i + 1]])
    }
    return pairs
  }

  encodePair([item1, item2]) {
    const pos1 = this.symbolsToPos[item1]
    const pos2 = this.symbolsToPos[item2]

    if (item1 === item2 && item1 === this.substitutionSymbol) {
      return [pos1, pos2]
    } else if (pos1[1] === pos2[1]) {
      return this.encodeFromColumn(pos1, pos2)
    } else if (pos1[0] === pos2[0]) {
      return this.encodeFromRow(pos1, pos2)
    }
    return this.fromRectangle(pos1, pos2)
  }

  decodePair([item1, item2]) {
    const pos1 = this.symbolsToPos[item1]
    const pos2 = this.symbolsToPos[item2]

    if (item1 === item2 && item1 === this.substitutionSymbol) {
      return `${item1}${item2}`
    } else if (pos1[1] === pos2[1]) {
      return this.decodeFromColumn(pos1, pos2)
    } else if (pos1[0] === pos2[0]) {
      return this.decodeFromRow(pos1, pos2)
    }
    return this.fromRectangle(pos1, pos2)
  }

  fromRectangle(pos1, pos2) {
    const newPos1 = [pos1[0], pos2[1]]
    const newPos2 = [pos2[0], pos1[1]]
    return [newPos1, newPos2]
  }

  encodeFromRow(pos1, pos2) {
    const newPos1 = [pos1[0], mod(pos1[1] + 1, this.squareSize)]
    const newPos2 = [pos2[0], mod(pos2[1] + 1, this.squareSize)]
    return [newPos1, newPos2]
  }

  decodeFromRow(pos1, pos2) {
    const newPos1 = [pos1[0], mod(pos1[1] - 1, this.squareSize)]
    const newPos2 = [pos2[0], mod(pos2[1] - 1, this.squareSize)]
    return [newPos1, newPos2]
  }

  encodeFromColumn(pos1, pos2) {
    const newPos1 = [mod(pos1[0] + 1, this.squareSize), pos1[1]]
    const newPos2 = [mod(pos2[0] + 1, this.squareSize), pos2[1]]
    return [newPos1, newPos2]
  }

  decodeFromColumn(pos1, pos2) {
    const newPos1 = [mod(pos1[0] - 1, this.squareSize), pos1[1]]
    const newPos2 = [mod(pos2[0] - 1, this.squareSize), pos2[1]]
    return [newPos1, newPos2]
  }
}
