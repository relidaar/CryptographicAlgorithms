const LatinAlphabetUpperCase = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
const LatinAlphabetLowerCase = 'abcdefghijklmnopqrstuvwxyz'
const LatinAlphabetUpperCaseWithoutJ = 'ABCDEFGHIKLMNOPQRSTUVWXYZ'
const LatinAlphabetLowerCaseWithoutJ = 'abcdefghiklmnopqrstuvwxyz'

const mod = (x, m) => ((x % m) + m) % m

const alphabetToSet = (alphabet) => {
  const result = new Set()
  for (const symbol of alphabet) {
    result.add(symbol)
  }
  return result
}

export {
  LatinAlphabetUpperCase,
  LatinAlphabetLowerCase,
  LatinAlphabetUpperCaseWithoutJ,
  LatinAlphabetLowerCaseWithoutJ,
  mod,
  alphabetToSet,
}
