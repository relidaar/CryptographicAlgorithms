import { CaesarCipher } from '../modules/caesar_cipher.mjs'
import { LatinAlphabetUpperCase } from '../modules/common.mjs'

const useEncoder = () => {
  try {
    if (!shiftInput.value) {
      shiftInputAlert.innerHTML = 'The shift input must contain a numeric value'
      shiftInputAlert.hidden = false
      return
    }

    const encoder = new CaesarCipher(
      shiftInput.valueAsNumber,
      alphabetInput.value
    )

    const input = inputTextArea.value
    if (encodeRadio.checked) {
      outputTextArea.value = encoder.encode(input)
    } else if (decodeRadio.checked) {
      outputTextArea.value = encoder.decode(input)
    }
    shiftInputAlert.hidden = true
    alphabetInputAlert.hidden = true
  } catch (error) {
    alphabetInputAlert.innerHTML = error.message
    alphabetInputAlert.hidden = false
  }
}

const shiftInput = document.querySelector('#shiftInput')
shiftInput.value = 3
const shiftInputAlert = document.querySelector('#shiftInputAlert')
shiftInput.addEventListener('input', useEncoder)

const alphabetInput = document.querySelector('#alphabetInput')
alphabetInput.value = LatinAlphabetUpperCase
const alphabetInputAlert = document.querySelector('#alphabetInputAlert')
alphabetInput.addEventListener('input', useEncoder)

const encodeRadio = document.querySelector('#encodeRadio')
const decodeRadio = document.querySelector('#decodeRadio')

const outputTextArea = document.querySelector('#outputTextArea')

const inputTextArea = document.querySelector('#inputTextArea')
inputTextArea.addEventListener('input', useEncoder)
