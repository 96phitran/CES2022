function formValidate() {
    var dateInputValidation = validateDateInput();
    var destinationValidation = validateDestination();
    var departureValidation = validateDeparture();
    var weightValidation = validateWeight();
    var lengthValidation = validateLength();

    if (dateInputValidation && destinationValidation && weightValidation && lengthValidation && departureValidation) {
        console.log("form submitted")
        document.getElementById("calculation-input-form").submit();
    }
    else {
        console.log(dateInputValidation, destinationValidation, weightValidation, lengthValidation, departureValidation)
        console.log("form not submitted, validation error")
    }

}

function validateDateInput() {
    let input = document.getElementById("date-input").value;
    let label = document.getElementById("dateLabel");
    if (input == "") {
        label.style.color = "Red";
        label.innerHTML = "ETD *Required";
        return false;
    }
    if (input != "") {
        label.innerHTML = "ETD";
        label.style.color = "#737b7d";
        if (!isValidDate(input)) {
            alert("ETD must be a valid date and follow this format 'dd-mm-yyyy'");
            return false;
        }
        return true
    }
    return false

}

function validateDeparture() {
    let input = document.getElementById("departure-list").value;
    let label = document.getElementById("departureLabel")
    if (input == "") {
        label.style.color = "Red";
        console.log("Departure empty")
        label.innerHTML = "Departure *Required";
        return false;
    }
    else {
        label.innerHTML = "Departure";
        label.style.color = "#737b7d"
        return true;
    }
    return false
}

function validateDestination() {
    let input = document.getElementById("destination-list").value;
    let label = document.getElementById("destinationLabel")
    let departureInput = document.getElementById("departure-list").value;
    if (input == "") {
        label.style.color = "Red";
        label.innerHTML = "Destination *Required";
        return false;
    }
    if (input != "") {
        label.innerHTML = "Destination";
        label.style.color = "#737b7d"
        if (input == departureInput) {
            alert("Destination must be different from Departure");
            return false;
        }
    } return true
    return false
}

function validateWeight() {
    let input = document.getElementById("weight-input").value;
    let label = document.getElementById("weightLabel");

    if (input == "") {
        label.style.color = "Red";
        label.innerHTML = "Destination *Required";
        return false;
    }
    if (input != "") {
        label.innerHTML = "Weight";
        label.style.color = "#737b7d"
        if (!input.match(/^(?=.)([+-]?([0-9]*)(\.([0-9]+))?)$/)) {
            alert("Weight must be a number.");
            return false;
        }
        if (input == 0) {
            alert("Weight must not be 0.");
            return false;
        }
        if (input < 0 || input > 100) {
            alert("Weight must be between 0 and 100 kg.");
            return false;
        }
    } return true
    return false
}

function validateLength() {
    let input = document.getElementById("length-input").value;
    let label = document.getElementById("lengthLabel");

    if (input == "") {
        label.style.color = "Red";
        label.innerHTML = "Destination *Required";
    }
    if (input != "") {
        label.innerHTML = "Length";
        label.style.color = "#737b7d"
        if (!input.match(/^(?=.)([+-]?([0-9]*)(\.([0-9]+))?)$/)) {
            alert("Length must be a number.");
            return false;
        }
        if (input == 0) {
            alert("Length must not be 0.");
            return false;
        }
        if (input < 0 || input > 100000) {
            alert("Length must be between 0 and 100000 cm.");
            return false;
        }
        return true
    }
    return false
}

function isValidDate(dateString) {

  // First check for the pattern
  if (!/^\d{1,2}\-\d{1,2}\-\d{4}$/.test(dateString)) return false;

  // Parse the date parts to integers
  var parts = dateString.split("-");
  var day = parseInt(parts[0], 10);
  var month = parseInt(parts[1], 10);
  var year = parseInt(parts[2], 10);

  // Check the ranges of month and year
  if (year < 1000 || year > 3000 || month == 0 || month > 12) return false;

  var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

  // Adjust for leap years
  if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
    monthLength[1] = 29;

  // Check the range of the day
  return day > 0 && day <= monthLength[month - 1];
}
