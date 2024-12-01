// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let travelStageCount = 1;

// Dodaj nowy etap podróży
document.getElementById("add-stage").addEventListener("click", function () {
    travelStageCount++;
    const travelStages = document.getElementById("travel-stages");
    const newStage = createTravelStage(travelStageCount);
    travelStages.appendChild(newStage);
    updateStageNumbers();
});

// Utwórz nowy etap podróży
function createTravelStage(stageNumber) {
    const stage = document.createElement("fieldset");
    stage.classList.add("travel-stage");
    stage.innerHTML = `
    <legend>Etap podróży ${stageNumber}</legend>
    <label>Data przyjazdu <input type="date" name="arrival_date_${stageNumber}" required></label>
    <label>Data wyjazdu <input type="date" name="departure_date_${stageNumber}" required></label>
    <label>Kraj
      <select name="country_${stageNumber}" required>
        <option value="">Ładowanie krajów...</option>
      </select>
    </label>
    <label>Organizator pobytu
      <select name="organizer_${stageNumber}" required>
        <option value="">Wybierz</option>
        <option value="Hotel">Hotel</option>
        <option value="Firma">Firma</option>
        <option value="Prywatnie">Prywatnie</option>
      </select>
    </label>
    <label>Miejscowość <input type="text" name="city_${stageNumber}" required></label>
    <label>Ulica <input type="text" name="street_${stageNumber}" required></label>
    <label>Nr domu <input type="number" name="house_number_${stageNumber}" required></label>
    <label>Nr lokalu <input type="number" name="apartment_number_${stageNumber}"></label>
    <button type="button" class="remove-stage" onclick="removeTravelStage(this)">- Usuń etap</button>`;

    const newSelect = document.querySelector(`select[name="country_${stageNumber}"]`);
    loadCountriesForSelect(newSelect);

    return stage;
}

// Usuń etap podróży i zaktualizuj numerację
function removeTravelStage(button) {
    const travelStages = document.querySelectorAll(".travel-stage");
    if (travelStages.length === 1) {
        alert("Nie możesz usunąć ostatniego etapu podróży!");
        return;
    }
    const stage = button.parentElement;
    stage.remove();
    updateStageNumbers();
}

// Aktualizuj numerację etapów
function updateStageNumbers() {
    const travelStages = document.querySelectorAll(".travel-stage");
    travelStages.forEach((stage, index) => {
        const newNumber = index + 1;
        stage.querySelector("legend").textContent = `Etap podróży ${newNumber}`;

        // Zaktualizuj atrybuty pól formularza
        const inputs = stage.querySelectorAll("input, select");
        inputs.forEach((input) => {
            const oldName = input.getAttribute("name");
            if (oldName) {
                const updatedName = oldName.replace(/\d+$/, newNumber); // Zamiana liczby na końcu nazwy
                input.setAttribute("name", updatedName);
            }
        });
    });
    travelStageCount = travelStages.length; // Aktualizuj licznik
}

// Dodanie niestandardowych komunikatów walidacji
document.querySelectorAll("input, select").forEach((field) => {
    field.addEventListener("invalid", function (event) {
        const input = event.target;

        // Resetowanie niestandardowego komunikatu
        input.setCustomValidity("");

        if (!input.validity.valid) {
            if (input.name.startsWith("pesel")) {
                input.setCustomValidity("PESEL musi zawierać dokładnie 11 cyfr.");
            } else if (input.name.startsWith("phone")) {
                input.setCustomValidity("Numer telefonu musi zaczynać się od '+' i zawierać tylko cyfry.");
            } else if (input.type === "email") {
                input.setCustomValidity("Podaj poprawny adres email (np. przyklad@domena.com).");
            } else if (input.type === "number") {
                input.setCustomValidity("Podaj liczbę.");
            } else if (input.type === "date") {
                input.setCustomValidity("Podaj poprawną datę.");
            } else if (input.tagName === "SELECT") {
                input.setCustomValidity("Wybierz jedną z opcji.");
            } else if (input.required) {
                input.setCustomValidity("To pole jest wymagane.");
            }
        }
    });

    // Reset komunikatu przy poprawnej zmianie wartości
    field.addEventListener("input", function (event) {
        event.target.setCustomValidity("");
    });
});
/*
// Walidacja przy wysyłaniu formularza
document.getElementById("travel-form").addEventListener("submit", function (event) {
  const form = event.target;
  if (!form.checkValidity()) {
    event.preventDefault(); // Zatrzymanie wysyłania formularza w przypadku błędów
  }
});*/

//=========================================================================================================

// Zmienna do przechowywania wprowadzonego formularza
const travelForm = document.getElementById("travel-form");
const summaryView = document.getElementById("summary-view");
const summaryContent = document.getElementById("summary-content");

// Obsługa przycisku "Zatwierdź"
travelForm.addEventListener("submit", function (event) {
    event.preventDefault(); // Zatrzymaj wysyłanie formularza
    if (!travelForm.checkValidity()) {
        return; // Jeśli formularz zawiera błędy, nie pokazuj podsumowania
    }

    // Wypełnij podsumowanie danymi
    generateSummary();

    // Pokaż podsumowanie, ukryj formularz
    travelForm.classList.add("hidden");
    summaryView.classList.remove("hidden");
});

// Wygeneruj podsumowanie
function generateSummary() {
    const formData = new FormData(travelForm); // Działa tylko na głównym formularzu
    let summaryHtml = "<h3>Dane osobowe</h3>";

    // Dane osobowe
    summaryHtml += `
      <p><strong>Imię:</strong> ${formData.get("first_name")}</p>
      <p><strong>Nazwisko:</strong> ${formData.get("last_name")}</p>
      <p><strong>PESEL:</strong> ${formData.get("pesel")}</p>
      <p><strong>Telefon:</strong> ${formData.get("phone")}</p>
      <p><strong>Email:</strong> ${formData.get("email")}</p>
      <p><strong>Adres:</strong> ${formData.get("street")} ${formData.get("house_number")}${formData.get("apartment_number") ? ", " + formData.get("apartment_number") : ""
        }, ${formData.get("city")}, ${formData.get("country")}</p>
    `;

    // Etapy podróży
    summaryHtml += "<h3>Etapy podróży</h3>";
    const travelStages = document.querySelectorAll(".travel-stage"); // Pobierz wszystkie etapy podróży
    travelStages.forEach((stage, index) => {
        const arrivalDate = stage.querySelector(`input[name="arrival_date_${index + 1}"]`).value;
        const departureDate = stage.querySelector(`input[name="departure_date_${index + 1}"]`).value;
        const country = stage.querySelector(`select[name="country_${index + 1}"]`).value;
        const organizer = stage.querySelector(`select[name="organizer_${index + 1}"]`).value;
        const city = stage.querySelector(`input[name="city_${index + 1}"]`).value;
        const street = stage.querySelector(`input[name="street_${index + 1}"]`).value;
        const houseNumber = stage.querySelector(`input[name="house_number_${index + 1}"]`).value;
        const apartmentNumber = stage.querySelector(`input[name="apartment_number_${index + 1}"]`).value;
        const warning = countryWarnings[country] || "Brak ostrzeżeń dla tego kraju.";

        summaryHtml += `
        <h4>Etap ${index + 1}</h4>
        <p><strong>Data przyjazdu:</strong> ${arrivalDate}</p>
        <p><strong>Data wyjazdu:</strong> ${departureDate}</p>
        <p><strong>Kraj:</strong> ${country}</p>
        <p><strong>Organizator pobytu:</strong> ${organizer}</p>
        <p><strong>Adres:</strong> ${street} ${houseNumber}${apartmentNumber ? ", " + apartmentNumber : ""
            }, ${city}</p>
        <p class=ost><strong>Ostrzeżenie:</strong> ${warning}</p>
      `;
    });

    summaryContent.innerHTML = summaryHtml;
}


// Przyciski w podsumowaniu
document.getElementById("modify-form").addEventListener("click", function () {
    // Wróć do formularza
    summaryView.classList.add("hidden");
    travelForm.classList.remove("hidden");
});

document.getElementById("confirm-submit").addEventListener("click", function () {
    alert("Zgłoszenie zostało zatwierdzone!");
    // Możesz tu dodać logikę wysyłania danych do serwera
});

// Ostrzeżenia

document.addEventListener("DOMContentLoaded", loadWarnings);

let countryWarnings = {}; // Obiekt do przechowywania ostrzeżeń dla krajów

// Funkcja do załadowania ostrzeżeń z pliku XML
async function loadWarnings() {
    try {
        const response = await fetch("/Data/warnings.xml");
        const xmlText = await response.text();
        const parser = new DOMParser();
        const xmlDoc = parser.parseFromString(xmlText, "application/xml");

        const warnings = xmlDoc.getElementsByTagName("warning");
        for (let warning of warnings) {
            const country = warning.getElementsByTagName("country")[0].textContent;
            const message = warning.getElementsByTagName("message")[0].textContent;
            countryWarnings[country] = message;
        }
    } catch (error) {
        console.error("Nie udało się załadować ostrzeżeń:", error);
    }
}

// Kraje

// Funkcja do załadowania krajów z pliku XML
async function loadCountries() {
    try {
        const response = await fetch("/Data/countries.xml");
        const xmlText = await response.text();
        const parser = new DOMParser();
        const xmlDoc = parser.parseFromString(xmlText, "application/xml");

        const countries = xmlDoc.getElementsByTagName("country");
        const countryOptions = Array.from(countries).map(
            (country) => `<option value="${country.textContent}">${country.textContent}</option>`
        );

        // Uzupełnij pole kraju zamieszkania
        //const originSelect = document.getElementById("country");
        //originSelect.innerHTML = '<option value="">Wybierz kraj</option>' + countryOptions.join("");

        // Dodajemy opcje do każdego pola wyboru kraju
        const countrySelects = document.querySelectorAll("select[name^='country']");
        countrySelects.forEach((select) => {
            select.innerHTML = '<option value="">Wybierz kraj</option>' + countryOptions.join("");
        });
    } catch (error) {
        console.error("Nie udało się załadować listy krajów:", error);
    }
}

// Wywołaj funkcję na starcie
document.addEventListener("DOMContentLoaded", loadCountries);

// Funkcja ładująca kraje dla konkretnego <select>
function loadCountriesForSelect(selectElement) {
    fetch("/Data/countries.xml")
        .then((response) => response.text())
        .then((xmlText) => {
            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(xmlText, "application/xml");

            const countries = xmlDoc.getElementsByTagName("country");
            const countryOptions = Array.from(countries).map(
                (country) => `<option value="${country.textContent}">${country.textContent}</option>`
            );

            selectElement.innerHTML = '<option value="">Wybierz kraj</option>' + countryOptions.join("");
        })
        .catch((error) => {
            console.error("Nie udało się załadować krajów dla nowego etapu:", error);
        });
}
