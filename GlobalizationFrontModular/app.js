const I18N_START_TOKEN = "#";
const I18N_END_TOKEN = "#";

import messages from "./locale/index.js";

/** @type {HTMLElement | null} */
let app = null;
/** @type {HTMLElement | null} */
let languageSelect = null;
let appWithTokens = "";

function getDefaultLanguage() {
  const lang = navigator.language || navigator.userLanguage;

  if (messages.hasOwnProperty(lang)) {
    return lang;
  }

  if (lang.startsWith("pt")) {
    return "pt";
  }

  return "en";
}

/**
 * @param {string} language
 */
function tokenReplacer(language) {
  const texts = messages[language] || messages["en"];
  let translatedApp = appWithTokens;

  for (const [key, value] of Object.entries(texts)) {
    if (typeof value === "object") {
      for (const [subKey, subValue] of Object.entries(value)) {
        const token = `${I18N_START_TOKEN}${key}.${subKey}${I18N_END_TOKEN}`;
        translatedApp = translatedApp.replace(token, subValue);
      }
    } else {
      const token = `${I18N_START_TOKEN}${key}${I18N_END_TOKEN}`;
      translatedApp = translatedApp.replace(token, value);
    }
  }

  app.innerHTML = translatedApp;
}

function titleReplacer(language) {
  const title = messages[language].title;
  document.title = title;
}

function onChangeLanguage(language) {
  tokenReplacer(language);
  titleReplacer(language);
  reassignSelect();
  languageSelect.value = language;
}

function reassignSelect() {
  languageSelect = document.getElementById("language-select");
  languageSelect.addEventListener("change", (event) => {
    onChangeLanguage(event.target.value);
  });
}

document.addEventListener("DOMContentLoaded", function () {
  app = document.getElementById("app");
  languageSelect = document.getElementById("language-select");
  appWithTokens = app.innerHTML;

  const defaultLanguage = getDefaultLanguage();
  onChangeLanguage(defaultLanguage);
});
