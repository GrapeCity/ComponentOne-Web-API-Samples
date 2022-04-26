var tokenKey = 'accessToken',
    emailKey = 'email',
    roleKey = 'role';

function saveUserInfo(data) {
    sessionStorage.setItem(tokenKey, data.access_token);
    sessionStorage.setItem(emailKey, data.userName);
    sessionStorage.setItem(tokenKey, data.access_token);
}

function register(data, success, error) {
    $.ajax({
        type: 'POST',
        url: '/api/Account/Register',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data)
    }).done(function (resultData) {
        if (success) success(resultData);
    }).fail(function (xhr) {
        if (error) error(xhr);
    });
}

function login(data, success, error) {
    data.grant_type = 'password';
    $.ajax({
        type: 'POST',
        url: '/Token',
        data: data
    }).done(function (resultData) {
        // Cache the access token in session storage.
        sessionStorage.setItem(tokenKey, resultData.access_token);
        if (typeof (success) === 'string') {
            window.location.href = success;
            return;
        }

        if (success) success(resultData);
    }).fail(function (xhr) {
        if(error) error(xhr);
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
        sessionStorage.removeItem(tokenKey);
        window.location.href = homeUrl;
    }).fail(function () { window.location.href = homeUrl; });
}

function getMsgFromResponse(xhr) {
    if(xhr.responseJSON){
        if (xhr.responseJSON.Message) return xhr.responseJSON.Message + '<br/><br/>Detail:<br/>' + JSON.stringify(xhr.responseJSON);
        if(xhr.responseJSON.error_description) return xhr.responseJSON.error_description;
    }

    return xhr.status + ': ' + xhr.statusText;
}

var _msgPopup;
function getMsgPopup() {
    return _msgPopup = _msgPopup || wijmo.Control.getControl('#msgPopup');
}

function showMsg(content, closable) {
    content = content || 'No message.';
    var message = (typeof (content) === 'string') ? content : getMsgFromResponse(content),
        popup = getMsgPopup(),
        $footer = $(popup.hostElement).find('.modal-footer'),
        $body = $(popup.hostElement).find('.modal-body');
    $body.html(message);
    if (closable) $footer.show();
    else $footer.hide();
    popup.show();
}

function closeMsgPopup() {
    getMsgPopup().hide();
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