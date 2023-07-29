import { toSet, mod } from './common.mjs'
import { InvalidAlphabetError, InvalidKeywordError } from './errors.mjs'

export class VigenereCipher {
  constructor(keyword, alphabet) {
    const minAlphabetLength = 2
    const minKeywordLength = 2

    if (!alphabet || alphabet.length < minAlphabetLength) {
      throw new InvalidAlphabetError(
        `The alphabet must contain at least ${minAlphabetLength} symbols`
      )
    }

    this.alphabet = alphabet
    this.alphabetSet = toSet(alphabet)
    if (alphabet.length !== this.alphabetSet.size) {
      throw new InvalidAlphabetError(
        `The alphabet must not contain duplicate symbols`
      )
    }

    if (!keyword || keyword.Length < minKeywordLength) {
      throw new InvalidKeywordError(
        `The keyword must contain at least ${minKeywordLength} symbols`
      )
    }

    if (keyword.length !== toSet(keyword).size) {
      throw new InvalidKeywordError(
        `The key must not contain duplicate symbols`
      )
    }

    this.key = []
    for (let i = 0; i < keyword.length; i++) {
      if (!this.alphabetSet.has(keyword[i])) {
        throw new InvalidKeywordError(
          `Provided alphabet doesn't contain the symbol ${keyword[i]}`
        )
      } else {
        this.key.push(this.alphabet.indexOf(keyword[i]))
      }
    }
  }

  encode(message) {
    const result = []
    let keyIndex = 0
    for (const symbol of message) {
      if (this.alphabetSet.has(symbol)) {
        const symbolIndex = this.alphabet.indexOf(symbol)
        const encodedSymbolIndex = mod(
          symbolIndex + this.key[keyIndex],
          this.alphabet.length
        )
        keyIndex = mod(keyIndex + 1, this.key.length)
        result.push(this.alphabet[encodedSymbolIndex])
      } else {
        result.push(symbol)
      }
    }
    return result.join('')
  }

  decode(encodedMessage) {
    const result = []
    let keyIndex = 0
    for (const symbol of encodedMessage) {
      if (this.alphabetSet.has(symbol)) {
        const symbolIndex = this.alphabet.indexOf(symbol)
        const encodedSymbolIndex = mod(
          symbolIndex - this.key[keyIndex],
          this.alphabet.length
        )
        keyIndex = mod(keyIndex + 1, this.key.length)
        result.push(this.alphabet[encodedSymbolIndex])
      } else {
        result.push(symbol)
      }
    }
    return result.join('')
  }
}
