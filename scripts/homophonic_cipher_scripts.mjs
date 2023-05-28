import { HomophonicCipher } from '../modules/homophonic_cipher.mjs'

let frequencyTable = {
  A: [21, 27, 31, 40],
  B: [15],
  C: [1, 33],
  D: [20, 34],
  E: [22, 28, 32, 36, 37],
  F: [5],
  G: [17],
  H: [14],
  I: [2, 29, 38, 41],
  J: [19],
  K: [3],
  L: [7, 39, 42],
  M: [9, 43],
  N: [12, 48, 97],
  O: [18, 60, 85],
  P: [26, 44],
  Q: [25],
  R: [24, 49],
  S: [10, 30, 45, 99],
  T: [6, 96, 55],
  U: [16, 94],
  V: [23],
  W: [13],
  X: [11],
  Y: [8],
  Z: [4],
}

const useEncoder = () => {
  try {
    const encoder = new HomophonicCipher(frequencyTable)

    const input = inputTextArea.value
    if (encodeRadio.checked) {
      outputTextArea.value = encoder.encode(input)
    } else if (decodeRadio.checked) {
      outputTextArea.value = encoder.decode(input)
    }
    frequencyTableAlert.hidden = true
  } catch (error) {
    frequencyTableAlert.innerHTML = error.message
    frequencyTableAlert.hidden = false
  }
}

const updateFrequencyTable = (symbolInput, frequenciesInput) => {
  symbolInput.value = symbolInput.value.trim()
  frequenciesInput.value = frequenciesInput.value.trim()

  const symbol = symbolInput.value
  if (symbol.length !== 1) {
    throw Error('A new symbol must be a single character')
  }

  const frequencies = frequenciesInput.value.split(',').map((f) => parseInt(f))
  if (frequencies.some((f) => !f || Number.isNaN(f))) {
    throw Error('Invalid frequencies')
  }

  const tempFrequencyTable = { ...frequencyTable }
  tempFrequencyTable[symbol] = frequencies
  new HomophonicCipher(tempFrequencyTable)
  frequencyTable[symbol] = frequencies
  symbolInput.value = ''
  frequenciesInput.value = ''
  frequencyTableAlert.hidden = true
}

const renderFrequencyTable = () => {
  frequencyTableDiv.innerHTML = ''
  for (const [symbol, frequencies] of Object.entries(frequencyTable)) {
    frequencyTableDiv.innerHTML += `
      <tr>
        <th id="${symbol}-symbolInput" scope="row" class="col-1 text-center align-middle"/>${symbol}
            </th>
        <td>
          <input
            class="form-control"
            id="${symbol}-frequenciesInput"
            value=${frequencies.join(',')}>
        </td>
        <td class="col-1 text-center">
          <button 
            id="${symbol}-removeRowButton"
            type="button" 
            class="remove-row-btn btn btn-danger">Remove</button>
        </td>
      </tr>
    `

    const symbolInput = document.querySelector(`#${symbol}-symbolInput`)
    const frequenciesInput = document.querySelector(
      `#${symbol}-frequenciesInput`
    )

    frequenciesInput.addEventListener('input', () => {
      try {
        updateFrequencyTable(symbolInput, frequenciesInput)
        renderFrequencyTable()
        useEncoder()
      } catch (error) {
        frequencyTableAlert.innerHTML = error.message
        frequencyTableAlert.hidden = false
      }
    })
  }

  var removeRowButtons = document.querySelectorAll(`.remove-row-btn`)
  for (const btn of removeRowButtons) {
    btn.addEventListener('click', (event) => {
      try {
        delete frequencyTable[event.target.id[0]]
        renderFrequencyTable()
        useEncoder()
      } catch (error) {
        frequencyTableAlert.innerHTML = error.message
        frequencyTableAlert.hidden = false
      }
    })
  }
}

const addFrequencyTableRow = () => {
  try {
    updateFrequencyTable(newSymbolInput, newFrequenciesInput)
    renderFrequencyTable()
    useEncoder()
  } catch (error) {
    frequencyTableAlert.innerHTML = error.message
    frequencyTableAlert.hidden = false
  }
}

const frequencyTableDiv = document.querySelector('#frequencyTable')
const frequencyTableAlert = document.querySelector('#frequencyTableAlert')

const newSymbolInput = document.querySelector('#newSymbolInput')
const newFrequenciesInput = document.querySelector('#newFrequenciesInput')
document
  .querySelector('#addFrequencyTableRowButton')
  .addEventListener('click', addFrequencyTableRow)

document
  .querySelector('#clearFrequencyTableButton')
  .addEventListener('click', () => {
    frequencyTable = {}
    frequencyTableDiv.innerHTML = ''
    useEncoder()
  })

const encodeRadio = document.querySelector('#encodeRadio')
const decodeRadio = document.querySelector('#decodeRadio')

const outputTextArea = document.querySelector('#outputTextArea')

const inputTextArea = document.querySelector('#inputTextArea')
inputTextArea.addEventListener('input', useEncoder)

renderFrequencyTable()
