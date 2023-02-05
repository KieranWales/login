const messageBox = document.getElementById("message");
const username = document.getElementById("username");
const password = document.getElementById("password");
const password2 = document.getElementById("password2");
const login = document.getElementById("loginForm");

const url = "https://en.wikipedia.org/wiki/Phishing";
clickCount = 0;

function reset() {

}

function createUser() {
    passCheck(password, password2, "Password's must be the same to create account", messageBox);
}

function validate() {
    presenceCheck(username, "Please enter a username", messageBox);
    presenceCheck(password, "Please enter a password", messageBox);
    lengthCheck(username, 3, 8, "Username should be between 3 and 8 characters", messageBox);
    lengthCheck(password, 3, 8, "Password should be between 3 and 8 characters", messageBox);
    login.submit();
}

function lengthCheck(input, min, max, message, output) {
    if (input.value.length > max || input.value.length < min) {
        output.innerText = message;
        throw message;
    }
    else {
        output.innerText = "Login to Blendr";
    }
}

function presenceCheck(input, message, output) {
    if (input.value == "") {
        output.innerText = message;
        throw message;
    }
    else {
        output.innerText = "Login to Blendr";
    }
}

function passCheck(input1, input2, message, output) {
    if (input1.value != input2.value) {
        output.innerText = message;
        throw message;
    }
    else {
        output.innerText = "Login to Blendr";
    }
}

function imgClick() {
    clickCount += 1;
    if (clickCount == 50) {
        alert("This could for example lead to a phishing page, be careful on the internet");
        window.open(url);
    }
}