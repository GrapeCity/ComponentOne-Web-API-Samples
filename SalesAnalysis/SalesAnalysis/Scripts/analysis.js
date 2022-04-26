$(function () {
    var engine = c1.getService('engine'),
        listBox = wijmo.Control.getControl('#tableList'),
        updateTable = function () {
            setTableName(listBox.collectionView.currentItem);
        }, setTableName = function (value) {
            var newValue = serviceRoot + value;
            if (newValue === engine.itemsSource) return;
            engine.itemsSource = newValue;
        };

    listBox.selectedIndexChanged.addHandler(updateTable);
    updateTable();

    var chart = wijmo.Control.getControl('#chart'),
        $chartBtn = $('a[href="#chart"]'),
        grid = wijmo.Control.getControl('#grid'),
        $gridBtn = $('a[href="#grid"]');
    $chartBtn.on('click', function () {
        setTimeout(function () {
            chart.invalidate();
        }, 500);
    });
    $gridBtn.on('click', function () {
        setTimeout(function () {
            grid.invalidate();
        }, 500);
    });
});

var httpRequest = wijmo.httpRequest;
wijmo.httpRequest = function (url, settings) {
    settings = settings || {};
    var token = sessionStorage.getItem(tokenKey);
    if (token) {
        var requestHeaders = settings.requestHeaders || {};
        requestHeaders['Authorization'] = 'Bearer ' + token;
        settings.requestHeaders = requestHeaders;
    }

    return httpRequest(url, settings);
};