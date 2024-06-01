document.addEventListener("DOMContentLoaded", () => {
    const messages = {
        en: {
            label: "Select Language:",
            message: "Hello, World!",
            options: {
                "en": "English",
                "pt-BR": "Portuguese (Brazil)",
                "pt-PT": "Portuguese (Portugal)",
                "pt": "Portuguese"
            }
        },
        "pt-BR": {
            label: "Selecione o idioma:",
            message: "Olá, Mundo!",
            options: {
                "en": "Inglês",
                "pt-BR": "Português (Brasil)",
                "pt-PT": "Português (Portugal)",
                "pt": "Português"
            }
        },
        "pt-PT": {
            label: "Selecione o idioma:",
            message: "Olá, Mundo!",
            options: {
                "en": "Inglês",
                "pt-BR": "Português (Brasil)",
                "pt-PT": "Português (Portugal)",
                "pt": "Português"
            }
        },
        pt: {
            label: "Selecione o idioma:",
            message: "Olá, Mundo!",
            options: {
                "en": "Inglês",
                "pt-BR": "Português (Brasil)",
                "pt-PT": "Português (Portugal)",
                "pt": "Português"
            }
        }
    };

    const languageSelect = document.getElementById("language-select");
    const messageElement = document.getElementById("message");
    const labelElement = document.getElementById("label-language");

    function setLanguage(lang) {
        const texts = messages[lang] || messages["en"];
        messageElement.textContent = texts.message;
        labelElement.textContent = texts.label;

        for (const [value, text] of Object.entries(texts.options)) {
            const option = languageSelect.querySelector(`option[value="${value}"]`);
            if (option) {
                option.textContent = text;
            }
        }

        languageSelect.value = lang;
    }

    function getDefaultLanguage() {
        const lang = navigator.language || navigator.userLanguage;
        if (messages.hasOwnProperty(lang)) {
            return lang;
        }
        // Fallback for general Portuguese if a specific one is not found
        if (lang.startsWith('pt')) {
            return 'pt';
        }
        return 'en';
    }

    languageSelect.addEventListener("change", (event) => {
        setLanguage(event.target.value);
    });

    // Set default language based on browser preferences
    const defaultLanguage = getDefaultLanguage();
    setLanguage(defaultLanguage);
});
