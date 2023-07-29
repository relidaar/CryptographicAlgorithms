import { mod, toSet } from './common.mjs'

export class PolybiusCipher {
  constructor(alphabet) {
    const minAlphabetLength = 3

    if (alphabet.length < minAlphabetLength) {
      throw new RangeError(
        `The alphabet must contain at least ${minAlphabetLength} symbols`
      )
    }

    if (alphabet.length !== toSet(alphabet).size) {
      throw new RangeError('The alphabet must not contain duplicate symbols')
    }

    // create Polybius squares for encoding and decoding
    this.encodingSquare = {}
    this.decodingSquare = {}

    const columns = Math.round(Math.sqrt(alphabet.length))
    let row = 1
    let col = 1
    for (const symbol of alphabet) {
      this.encodingSquare[symbol] = [row, col]
      this.decodingSquare[[row, col]] = symbol
      col++

      if (col == columns + 1) {
        col = 1
        row++
      }
    }
  }

  encode(message) {
    return message
      .split('')
      .filter((s) => s in this.encodingSquare)
      .map((s) => {
        const [row, col] = this.encodingSquare[s]
        return `${row}${col}`
      })
      .join('')
  }

  decode(encodedMessage) {
    let text = encodedMessage
      .replace(' ', '')
      .split('')
      .filter((c) => c >= '0' && c <= '9')
      .join('')
    if (text.length % 2 != 0) {
      text = text.substring(0, text.length - 1)
    }

    const result = []
    for (let i = 0; i < text.length; i += 2) {
      const pair = [text[i] - '0', text[i + 1] - '0']
      if (pair in this.decodingSquare) {
        result.push(this.decodingSquare[pair])
      }
    }
    return result.join('')
  }
}
