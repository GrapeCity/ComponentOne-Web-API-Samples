function initializeFlexGrid(grid) {
    grid.initialize({
        autoGenerateColumns: false,
        columns: [
            { header: 'Id', binding: 'id' },
            { header: 'Date', binding: 'date', format: 'MMM/dd/yyyy' },
            { header: 'Time', binding: 'time', format: "t" },
            { header: 'Country', binding: 'country', width: '*', minWidth: 125 },
            { header: 'Downloads', binding: 'downloads' },
            { header: 'Sales', binding: 'sales' },
            { header: 'Expenses', binding: 'expenses' },
            { header: 'Checked', binding: 'checked' }
        ],
        itemsSource: getGridData(100)
    });
}

function getGridData(count) {
    var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
        data = [], countryIndex;
    for (var i = 0; i < count; i++) {
        countryIndex = parseInt(Math.random() * 6);
        data.push({
            id: i,
            date: new Date(2015, parseInt(Math.random() * 12), parseInt(Math.random() * 25 + 1)),
            time: new Date(2015, parseInt(Math.random() * 12), parseInt(Math.random() * 25 + 1), parseInt(Math.random() * 24), parseInt(Math.random() * 60), 0),
            country: countries[countryIndex],
            countryMapped: countryIndex,
            downloads: Math.round(Math.random() * 999),
            sales: Math.random() * 10000,
            expenses: Math.random() * 5000,
            checked: i % 9 == 0
        });
    }
    return data;
}