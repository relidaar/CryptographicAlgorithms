const LatinAlphabetUpperCase = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
const LatinAlphabetLowerCase = 'abcdefghijklmnopqrstuvwxyz'
const LatinAlphabetUpperCaseWithoutJ = 'ABCDEFGHIKLMNOPQRSTUVWXYZ'
const LatinAlphabetLowerCaseWithoutJ = 'abcdefghiklmnopqrstuvwxyz'

const mod = (x, m) => ((x % m) + m) % m

export {
  LatinAlphabetUpperCase,
  LatinAlphabetLowerCase,
  LatinAlphabetUpperCaseWithoutJ,
  LatinAlphabetLowerCaseWithoutJ,
  mod,
}
