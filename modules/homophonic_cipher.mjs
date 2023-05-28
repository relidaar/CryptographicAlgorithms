import { toSet } from './common.mjs'

export class HomophonicCipher {
  constructor(frequencyTable) {
    const frequencyMaxValue = 99

    if (Object.values(frequencyTable).some((e) => !e || e.length === 0)) {
      throw new Error(
        'Each symbol in the frequency table must have at least one assigned frequency'
      )
    }

    const frequencies = Object.values(frequencyTable).flatMap((e) => e)
    if (frequencies.some((f) => f < 1 || f > frequencyMaxValue)) {
      throw new Error(
        `The frequencies must be in the range of [1:${frequencyMaxValue}]`
      )
    }

    if (frequencies.length !== toSet(frequencies).size) {
      throw new Error(
        'The frequency table must not contain duplicate frequencies'
      )
    }

    this.encodingTable = frequencyTable
    this.decodingTable = {}
    for (const [symbol, symbolFrequencies] of Object.entries(frequencyTable)) {
      for (const frequency of symbolFrequencies) {
        this.decodingTable[frequency] = symbol
      }
    }
  }

  encode(message) {
    return message
      .split('')
      .filter((c) => c in this.encodingTable)
      .map((c) => {
        const frequencies = this.encodingTable[c]
        const randomIndex = Math.floor(Math.random() * frequencies.length)
        return frequencies[randomIndex].toString().padStart(2, '0')
      })
      .join('')
  }

  decode(encodedMessage) {
    return this.parseFrequencies(encodedMessage)
      .filter((f) => f in this.decodingTable)
      .map((f) => this.decodingTable[f])
      .join('')
  }

  parseFrequencies(encodedMessage) {
    if (!encodedMessage) {
      return []
    }

    const digits = encodedMessage.split('').filter((c) => c && !isNaN(c))
    if (digits.length % 2 !== 0) {
      digits.pop()
    }

    const pairs = []
    for (let i = 0; i < digits.length; i += 2) {
      const frequency = parseInt(`${digits[i]}${digits[i + 1]}`)
      if (frequency) {
        pairs.push(frequency)
      }
    }
    return pairs
  }
}
