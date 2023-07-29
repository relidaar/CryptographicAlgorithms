import { LatinAlphabetUpperCase } from '../modules/common.mjs'
import { PolybiusCipher } from '../modules/polybius_cipher.mjs'

const useEncoder = () => {
  try {
    const encoder = new PolybiusCipher(alphabetInput.value)

    const input = inputTextArea.value
    if (encodeRadio.checked) {
      outputTextArea.value = encoder.encode(input)
    } else if (decodeRadio.checked) {
      outputTextArea.value = encoder.decode(input)
    }
    alphabetInputAlert.hidden = true
  } catch (error) {
    alphabetInputAlert.innerHTML = error.message
    alphabetInputAlert.hidden = false
  }
}

const alphabetInput = document.querySelector('#alphabetInput')
alphabetInput.value = LatinAlphabetUpperCase
const alphabetInputAlert = document.querySelector('#alphabetInputAlert')
alphabetInput.addEventListener('input', useEncoder)

const encodeRadio = document.querySelector('#encodeRadio')
const decodeRadio = document.querySelector('#decodeRadio')

const outputTextArea = document.querySelector('#outputTextArea')

const inputTextArea = document.querySelector('#inputTextArea')
inputTextArea.addEventListener('input', useEncoder)
