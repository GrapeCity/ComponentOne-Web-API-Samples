$(function () {
    setFolder(folder);
});

function folderRefresh() {
    setFolder(folder);
}

function folderUp() {
    folder = folder || '/';
    var names = folder.split('/');
    if (names.length > 0 && !names[names.length - 1]) {
        names.splice(names.length - 1, 1);
    }

    if (names.length > 0) {
        names.splice(names.length - 1, 1);
    }

    var newFolder = names.join('/');
    setFolder(newFolder);
}

function setFolder(value) {
    value = value || '/';
    if (value[value.length - 1] != '/'){
        value += '/';
    }

    folder = value;
    var $folder = $('#folder');
    $folder.text(value);

    $('#upBtn').prop('disabled', value == '/');

    var $catalogItems = $('#catalogItems');
    $catalogItems.html('Loading');

    wijmo.viewer.ReportViewer.getReports(reportApi, value).then(function (data) {
        $catalogItems.html('');
        data = data || {};
        var dataItems = data.items || [];
        var items = [];
        for (var i = 0, length = dataItems.length; i < length; i++) {
            var item = dataItems[i];
            if (item.type === wijmo.viewer.CatalogItemType.File) {
                items = items.concat(item.items);
                continue;
            }

            items.push(item);
        }

        for (var i = 0, length = items.length; i < length; i++) {
            var item = items[i], name = item.name, isReport = item.type === wijmo.viewer.CatalogItemType.Report;
            if (isReport) {
                var parts = item.path.split('/');
                name = parts[parts.length - 2] + '/' + parts[parts.length - 1];
            }

            var $item = $('<div>')
                .data('catalogItem', item)
                .addClass('catalog-item')
                .on('dblclick', function () {
                    var curItem = $(this);
                    var curData = curItem.data('catalogItem');
                    if (curData.type === wijmo.viewer.CatalogItemType.Report) {
                        showReport(curData.path);
                        return;
                    }

                    setFolder(curData.path);
                }),
                icon = $('<div>').addClass('glyphicon')
                .addClass(isReport ? 'glyphicon-file' : 'glyphicon-folder-close'),
                title = $('<div>').addClass('title').text(name);
            $item.append(icon).append(title);
            $catalogItems.append($item);
        }
    }).catch(function (xhr) {
        var message = xhr.status + ': ' + xhr.statusText;
        $catalogItems.html('<div class="error">' + message + '<br/>'
            + 'You don\'t have permission to access this folder. Please contact your network administrator.</div>' );
    });
}

function showReport(path) {
    var url = reportPageUrl + '?path=' + path;
    gotoUrl(url);
}

function gotoUrl(url) {
    window.location.href = url;
}

var httpRequest = wijmo.httpRequest;
wijmo.httpRequest = function (url, settings) {
    settings = settings || {};
    var token = sessionStorage.getItem(tokenKey);
    if (token) {
        var requestHeaders = settings.requestHeaders || {};
        requestHeaders['Authorization'] = 'Bearer ' + token;
        settings.requestHeaders = requestHeaders;
    }

    httpRequest(url, settings);
};