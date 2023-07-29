import {
  LatinAlphabetUpperCase,
  LatinAlphabetUpperCaseWithoutJ,
} from '../modules/common.mjs'
import {
  InvalidAlphabetError,
  InvalidKeywordError,
  InvalidSubstitutionSymbolError,
} from '../modules/errors.mjs'
import { PlayfairCipher } from '../modules/playfair_cipher.mjs'

const useEncoder = () => {
  try {
    const encoder = new PlayfairCipher(
      alphabetInput.value,
      keywordInput.value,
      substitutionSymbolInput.value
    )

    const input = inputTextArea.value
    if (encodeRadio.checked) {
      outputTextArea.value = encoder.encode(input)
    } else if (decodeRadio.checked) {
      outputTextArea.value = encoder.decode(input)
    }
    keywordInputAlert.hidden = true
    alphabetInputAlert.hidden = true
    substitutionSymbolInputAlert.hidden = true
  } catch (error) {
    if (error instanceof InvalidAlphabetError) {
      alphabetInputAlert.innerHTML = error.message
      alphabetInputAlert.hidden = false
    } else if (error instanceof InvalidKeywordError) {
      keywordInputAlert.innerHTML = error.message
      keywordInputAlert.hidden = false
    } else if (error instanceof InvalidSubstitutionSymbolError) {
      substitutionSymbolInputAlert.innerHTML = error.message
      substitutionSymbolInputAlert.hidden = false
    }
  }
}

const substitutionSymbolInput = document.querySelector(
  '#substitutionSymbolInput'
)
substitutionSymbolInput.value = 'X'
const substitutionSymbolInputAlert = document.querySelector(
  '#substitutionSymbolInputAlert'
)
substitutionSymbolInput.addEventListener('input', useEncoder)

const keywordInput = document.querySelector('#keywordInput')
keywordInput.value = 'KEYWORD'
const keywordInputAlert = document.querySelector('#keywordInputAlert')
keywordInput.addEventListener('input', useEncoder)

const alphabetInput = document.querySelector('#alphabetInput')
alphabetInput.value = LatinAlphabetUpperCaseWithoutJ
const alphabetInputAlert = document.querySelector('#alphabetInputAlert')
alphabetInput.addEventListener('input', useEncoder)

const encodeRadio = document.querySelector('#encodeRadio')
const decodeRadio = document.querySelector('#decodeRadio')

const outputTextArea = document.querySelector('#outputTextArea')

const inputTextArea = document.querySelector('#inputTextArea')
inputTextArea.addEventListener('input', useEncoder)
