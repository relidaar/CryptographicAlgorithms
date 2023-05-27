import { mod, alphabetToSet } from './common.mjs'

export class CaesarCipher {
  constructor(shift = 3, alphabet) {
    const minAlphabetLength = 2
    this.shift = shift

    if (alphabet.length < minAlphabetLength) {
      throw new RangeError(
        `The alphabet must contain at least ${minAlphabetLength} symbols`
      )
    }

    this.alphabet = alphabet
    this.alphabetSet = alphabetToSet(alphabet)
    if (alphabet.length != this.alphabetSet.size) {
      throw new RangeError('The alphabet must not contain duplicate symbols')
    }
  }

  encode(message) {
    const result = []
    for (const symbol of message) {
      if (this.alphabetSet.has(symbol)) {
        const symbolIndex = this.alphabet.indexOf(symbol)
        const encodedSymbolIndex = mod(
          symbolIndex + this.shift,
          this.alphabet.length
        )
        result.push(this.alphabet[encodedSymbolIndex])
      } else {
        result.push(symbol)
      }
    }
    return result.join('')
  }

  decode(message) {
    const result = []
    for (const symbol of message) {
      if (this.alphabetSet.has(symbol)) {
        const symbolIndex = this.alphabet.indexOf(symbol)
        const encodedSymbolIndex = mod(
          symbolIndex - this.shift,
          this.alphabet.length
        )
        result.push(this.alphabet[encodedSymbolIndex])
      } else {
        result.push(symbol)
      }
    }
    return result.join('')
  }
}
