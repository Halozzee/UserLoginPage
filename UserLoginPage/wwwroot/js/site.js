// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

if (document.getElementById("signUpUser") != null) {
    document.getElementById("signUpUser").onclick = trySignUpUser;
}

if (document.getElementById("logInUser") != null) {
    document.getElementById("logInUser").onclick = tryLogInUser;
}

if (document.getElementById("changePassword") != null) {
    document.getElementById("changePassword").onclick = tryChangePassword;
}

if (document.getElementById("logOut") != null) {
    document.getElementById("logOut").onclick = tryLogOutUser;
}

let loginBox = document.getElementById("loginBox");
let firstPasswordBox = document.getElementById("firstPasswordBox");
let secondPasswordBox = document.getElementById("secondPasswordBox");
let messageBox = document.getElementById("messages");

function trySignUpUser()
{
    if (!loginValidation())
    {
        return;
    }

    if (emptyPasswordValidation()) {
        return;
    }

    if (!signUpPasswordValidation())
    {
        return;
    }

    sendSignUpRequest(loginBox.value, firstPasswordBox.value);
}

function tryLogInUser() {
    if (!loginValidation()) {
        return;
    }

    if (emptyPasswordValidation()) {
        return;
    }

    sendLogInRequest(loginBox.value, firstPasswordBox.value);
}

function loginValidation()
{
    if (loginBox.value != "") {
        return true;
    }
    else {

        messageBox.innerHTML = "Login required"
        return false;
    }
}

function emptyPasswordValidation() {
    if (firstPasswordBox.value == "")
        return true;
    else if (secondPasswordBox) {
        messageBox.innerHTML = "Passwords are not matching"
        return false;
    }
}

function signUpPasswordValidation()
{
    if (firstPasswordBox.value == secondPasswordBox.value)
        return true;
    else {
        messageBox.innerHTML = "Passwords are not matching"
        return false;
    }
}

// Write your JavaScript code.
function sendSignUpRequest(login, password)
{
    $.ajax({
        type: "POST",
        url: "https://localhost:7175/api/user/SignUp?login=" + login + "&passwordhash=" + CryptoJS.MD5(password).toString(),
	    success: function (data)
        {
            let response = JSON.parse(data);

            messageBox.innerHTML = response.ResponseMessage;
	    }
    });
}

function sendLogInRequest(login, password)
{
    $.ajax({
        type: "POST",
        url: "https://localhost:7175/api/user/LogIn?login=" + login + "&passwordhash=" + CryptoJS.MD5(password).toString(),
        success: function (data) {
            let response = JSON.parse(data);

            messageBox.innerHTML = response.ResponseMessage;

            if (response.UserResponseStatus == 0)
                window.location.replace("https://localhost:7175/Home/UserProfilePage?login=" + response.Token);
        }
    });
}

function tryChangePassword()
{
    if (emptyPasswordValidation()) {
        return;
    }

    if (!signUpPasswordValidation()) {
        return;
    }

    sendChangePasswordRequest(document.getElementById("login").innerHTML, firstPasswordBox.value)
}

function sendChangePasswordRequest(login, password) {

    $.ajax({
        type: "POST",
        url: "https://localhost:7175/api/user/ChangePassword?login=" + login + "&passwordhash=" + CryptoJS.MD5(password).toString(),
        success: function (data) {
            let response = JSON.parse(data);

            messageBox.innerHTML = response.ResponseMessage;

        }
    });
}

function tryLogOutUser()
{
    $.ajax({
        type: "POST",
        url: "https://localhost:7175/api/user/LogOut?login=" + document.getElementById("login").innerHTML,
        success: function (data) {
            let response = JSON.parse(data);
            messageBox.innerHTML = response.ResponseMessage;
            window.location.replace("https://localhost:7175/");
        }
    });

}