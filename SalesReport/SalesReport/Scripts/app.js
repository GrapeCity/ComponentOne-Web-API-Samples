var tokenKey = 'accessToken';
var loginUserData;

function register() {
    var email = $("#registerEmail").val();
    var password = $("#registerPassword").val();
    var confirmPassword = $("#registerConfirmPassword").val();

    var data = {
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword
    };

    $.ajax({
        type: 'POST',
        url: '/api/Account/Register',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data)
    }).done(function (data) {
        showSuccess("Register success!");
    }).fail(showError);
}

function login(successUrl) {
    var loginData = {
        grant_type: 'password',
        username: $("#loginEmail").val(),
        password: $("#loginPassword").val()
    };

    $this = $(this);
    $this.prop('disabled', true);

    $.ajax({
        type: 'POST',
        url: '/Token',
        data: loginData
    }).done(function (data) {
        loginUserData = data.userName;
        // Cache the access token in session storage.
        sessionStorage.setItem(tokenKey, data.access_token);
        window.location.href = successUrl;
    }).fail(function(xhr){
        $this.prop('disabled', false);
        showError(xhr);
    });
}

function logout() {
    // Log out from the cookie based logon.
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: 'POST',
        url: '/api/Account/Logout',
        headers: headers
    }).done(function (data) {
        // Successfully logged out. Delete the token.
        loginUserData = null;
        sessionStorage.removeItem(tokenKey);
        window.location.href = homeUrl;
    }).fail(showError);
}

function showSuccess(msg) {
    console.log(msg);
}

function showError(error) {
    var message = typeof (error) === 'string' ? error : error.status + ': ' + error.statusText;
    var $error = $('.error');
    if ($error && $error.length) {
        $error.text(message);
    } else {
        alert(message);
    }
}

$(function () {
    initHamburgerNav();
    var token = sessionStorage.getItem(tokenKey);
    if (!token) {
        var signOut = $('.signout-btn');
        signOut.hide();
    }
});

function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}