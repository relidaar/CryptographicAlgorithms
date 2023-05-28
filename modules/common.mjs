const LatinAlphabetUpperCase = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
const LatinAlphabetLowerCase = 'abcdefghijklmnopqrstuvwxyz'
const LatinAlphabetUpperCaseWithoutJ = 'ABCDEFGHIKLMNOPQRSTUVWXYZ'
const LatinAlphabetLowerCaseWithoutJ = 'abcdefghiklmnopqrstuvwxyz'

const mod = (x, m) => ((x % m) + m) % m

const toSet = (list) => {
  const result = new Set()
  for (const symbol of list) {
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
  toSet,
}
