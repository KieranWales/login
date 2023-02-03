const messageBox = document.getElementById("message");
const username = document.getElementById("username");
const password = document.getElementById("password");
const login = document.getElementById("login");

function reset() {

}

function createUser() {

}

function validate() {
    presenceCheck(username, "Please enter a username", messageBox);
    presenceCheck(password, "Please enter a password", messageBox);
    lengthCheck(username, 3, 8, "Username should be between 3 and 8 characters", messageBox)
    lengthCheck(password, 3, 8, "Password should be between 3 and 8 characters", messageBox)
    login.Submit();
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