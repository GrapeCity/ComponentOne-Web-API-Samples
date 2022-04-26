'use strict';

var app = {};

function cmbDataSets_SelectIndexChanged(s, e) {
    app.setProperty('data', s.selectedValue);
}

function cmbRowTotals_SelectIndexChanged(s, e) {
    app.setProperty('showRowTotals', s.selectedValue);
}

function cmbColTotals_SelectIndexChanged(s, e) {
    app.setProperty('showColTotals', s.selectedValue);
}

function chkShowZeros_CheckedChanged() {
    var chkShowZeros = document.getElementById('chkShowZeros');
    app.setProperty('showZeros', chkShowZeros.checked);
}

app.propertyChanged = new wijmo.Event();
app.setProperty = function (name, value) {
    if (app[name] !== value) {
        var oldValue = app[name];
        app[name] = value;
        app.propertyChanged.raise(app, new wijmo.PropertyChangedEventArgs(name, oldValue, value));
    }
}
app.cubeFields = [
        {
            "dataType": 2, "format": "n0", "aggregate": 1, "header": "Internet Orders", "dimensionType": 1,
            "subFields": [
                { "dataType": 2, "format": "n0", "aggregate": 1, "binding": "[Measures].[Internet Order Count]", "header": "Internet Order Count", "dimensionType": 1 }
            ]
        },
        {
            "dataType": 0, "aggregate": 2, "header": "Product", "dimensionType": 0,
            "subFields": [
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Category]", "header": "Category", "dimensionType": 6
                },
                {
                    "dataType": 0, "aggregate": 2, "header": "Financial", "dimensionType": 5,
                    "subFields": [
                        {
                            "dataType": 1, "aggregate": 2, "binding": "[Product].[Dealer Price]", "header": "Dealer Price", "dimensionType": 6
                        },
                        {
                            "dataType": 1, "aggregate": 2, "binding": "[Product].[List Price]", "header": "List Price", "dimensionType": 6
                        },
                        {
                            "dataType": 1, "aggregate": 2, "binding": "[Product].[Standard Cost]", "header": "Standard Cost", "dimensionType": 6
                        }
                    ]
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Model Name]", "header": "Model Name", "dimensionType": 6
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Product]", "header": "Product", "dimensionType": 6
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Product Categories]", "header": "Product Categories", "dimensionType": 6
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Product Line]", "header": "Product Line", "dimensionType": 6
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Product Model Lines]", "header": "Product Model Lines", "dimensionType": 6
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Style]", "header": "Style", "dimensionType": 6
                },
                {
                    "dataType": 1, "aggregate": 2, "binding": "[Product].[Subcategory]", "header": "Subcategory", "dimensionType": 6
                }
            ]
        }
];

app.propertyChanged.addHandler(function (s, e) {
    var ng = wijmo.Control.getControl('#pivotPanel').engine;
    switch (e.propertyName) {
        case 'data':
            if (e.newValue == 'cube') {
                ng.autoGenerateFields = false;
                wijmo.copy(ng, { fields: app.cubeFields });
            } else {
                if (!ng.autoGenerateFields) {
                    ng.fields.clear();
                }
                ng.autoGenerateFields = true;
            }
            // no filter by value on server sources
            ng.defaultFilterType = wijmo.isString(ng.itemsSource) ? 1 : 3;

            // set the new data source
            ng.itemsSource = webAPIService + '/' + e.newValue;

            // set the default row fields and value fields
            if (ng.rowFields.length == 0 && ng.valueFields.length == 0) {
                if (e.newValue == 'cube') {
                    ng.rowFields.push('[Product].[Category]', '[Product].[Subcategory]');
                    ng.valueFields.push('[Measures].[Internet Order Count]');
                } else {
                    ng.rowFields.push('Product', 'Country');
                    ng.valueFields.push('Sales', 'Downloads');
                }
            }
            break;
        case 'showRowTotals':
            ng.showRowTotals = e.newValue;
            break;
        case 'showColTotals':
            ng.showColumnTotals = e.newValue;
            break;
        case 'showZeros':
            ng.showZeros = e.newValue;
            break;
    }
});

