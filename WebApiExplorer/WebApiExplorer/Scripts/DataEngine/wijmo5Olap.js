'use strict';
// define all when page loads
onload = function () {

    // define sample data sets
    app.dataSets = [
        { name: 'DataEngine(100,000 items)', value: 'complex10' },
        { name: 'DataEngine(500,000 items)', value: 'complex50' },
        { name: 'DataEngine(1,000,000 items)', value: 'complex100' },
        { name: 'DataSet(100,000 items)', value: 'dataset10' },
        { name: 'DataSet(500,000 items)', value: 'dataset50' },
        { name: 'DataSet(1,000,000 items)', value: 'dataset100' },
        { name: 'SSAS (Adventure Works)', value: 'cube' }
    ];

    // define ShowTotals options/values
    app.showTotals = [
        { name: 'None', value: wijmo.olap.ShowTotals.None },
        { name: 'Grand totals', value: wijmo.olap.ShowTotals.GrandTotals },
        { name: 'Subtotals', value: wijmo.olap.ShowTotals.Subtotals },
    ];

    // 1) create/bind the controls that configure the PivotPanel control
    app.cmbDataSets = new wijmo.input.ComboBox('#cmbDataSets', {
        itemsSource: app.dataSets,
        displayMemberPath: 'name',
        selectedValuePath: 'value',
        selectedIndexChanged: cmbDataSets_SelectIndexChanged
    });
    app.cmbRowTotals = new wijmo.input.ComboBox('#cmbRowTotals', {
        itemsSource: app.showTotals,
        displayMemberPath: 'name',
        selectedValuePath: 'value',
        selectedIndexChanged: cmbRowTotals_SelectIndexChanged
    });
    app.cmbColTotals = new wijmo.input.ComboBox('#cmbColTotals', {
        itemsSource: app.showTotals,
        displayMemberPath: 'name',
        selectedValuePath: 'value',
        selectedIndexChanged: cmbColTotals_SelectIndexChanged
    });

    // 2) create/bind the PivotPanel and PivotGrid controls
    app.panel = new wijmo.olap.PivotPanel('#pivotPanel');
    var ng = app.panel.engine;
    ng.itemsSource = webAPIService + '/' + app.dataSets[0].value;
    ng.rowFields.push('Product', 'Country');
    ng.valueFields.push('Sales', 'Downloads');

    app.pivotGrid = new wijmo.olap.PivotGrid('#pivotGrid', {
        itemsSource: app.panel,
        showSelectedHeaders: 'All'
    });

    // initialize app properties
    app.setProperty('showRowTotals', wijmo.olap.ShowTotals.None);
    app.setProperty('showColTotals', wijmo.olap.ShowTotals.None);
    app.setProperty('showZeros', false);
    app.setProperty('data', app.dataSets[0].value);
}
