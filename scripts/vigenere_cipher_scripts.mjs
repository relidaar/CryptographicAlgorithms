import { LatinAlphabetUpperCase } from '../modules/common.mjs'
import { VigenereCipher } from '../modules/vigenere_cipher.mjs'
import {
  InvalidAlphabetError,
  InvalidKeywordError,
} from '../modules/errors.mjs'

const useEncoder = () => {
  try {
    const encoder = new VigenereCipher(keywordInput.value, alphabetInput.value)

    const input = inputTextArea.value
    if (encodeRadio.checked) {
      outputTextArea.value = encoder.encode(input)
    } else if (decodeRadio.checked) {
      outputTextArea.value = encoder.decode(input)
    }
    keywordInputAlert.hidden = true
    alphabetInputAlert.hidden = true
  } catch (error) {
    if (error instanceof InvalidAlphabetError) {
      alphabetInputAlert.innerHTML = error.message
      alphabetInputAlert.hidden = false
    } else if (error instanceof InvalidKeywordError) {
      keywordInputAlert.innerHTML = error.message
      keywordInputAlert.hidden = false
    }
  }
}

const keywordInput = document.querySelector('#keywordInput')
keywordInput.value = 'KEYWORD'
const keywordInputAlert = document.querySelector('#keywordInputAlert')
keywordInput.addEventListener('input', useEncoder)

const alphabetInput = document.querySelector('#alphabetInput')
alphabetInput.value = LatinAlphabetUpperCase
const alphabetInputAlert = document.querySelector('#alphabetInputAlert')
alphabetInput.addEventListener('input', useEncoder)

const encodeRadio = document.querySelector('#encodeRadio')
const decodeRadio = document.querySelector('#decodeRadio')

const outputTextArea = document.querySelector('#outputTextArea')

const inputTextArea = document.querySelector('#inputTextArea')
inputTextArea.addEventListener('input', useEncoder)
