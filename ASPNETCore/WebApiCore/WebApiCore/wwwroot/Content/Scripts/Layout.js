function initHamburgerNav() {
    var hamburgerNavBtn = document.querySelector('.hamburger-nav-btn');
    var hamburgerNavEle = document.querySelector('.narrow-screen.site-nav');
    new c1.sample.MultiLevelMenu(hamburgerNavEle, hamburgerNavBtn);
}

function tree_ItemFormatter(sender, args) {
    var panel = args.panel,
        r = args.row,
        count = sender.updateCount,
        maxCount = sender.maxUpdateCount;

    if (wijmo.grid.CellType.Cell == panel.cellType) {
        var gr = wijmo.tryCast(sender.rows[r], wijmo.grid.GroupRow);
        if (!count && count != 0) {
            count = 0;
            sender.updateCount = 0;
        }
        if (!maxCount && maxCount != 0) {
            maxCount = 1;
            sender.maxUpdateCount = 1;
        }

        if (gr && count <= maxCount && gr.level == maxCount - count) {
            gr.isCollapsed = true;
        }
    }
}

function tree_UpdatedView(sender, args) {
    if (!sender.updateCount) {
        sender.updateCount = 0;
    }
    if (!sender.maxUpdateCount) {
        sender.maxUpdateCount = 1;
    }
    var count = sender.updateCount + 1;
    sender.updateCount = count;
    if (count > sender.maxUpdateCount) {
        sender.formatItem.removeHandler(tree_ItemFormatter);
        sender.updatedView.removeHandler(tree_UpdatedView, sender);
    }
}

function createTree(element, source, binding, childItemsPath) {
    var grid = new wijmo.grid.FlexGrid(element, {
        autoGenerateColumns: false,
        childItemsPath: childItemsPath,
        columns: [
            { binding: binding, width: '*' }
        ],
        headersVisibility: 'None',
        selectionMode: 'Cell',
        showAlternatingRows: false,
        formatItem: tree_ItemFormatter
    });
    grid.itemsSource = source;
    grid.maxUpdateCount = 1;
    grid.updatedView.addHandler(tree_UpdatedView, grid);
}

$(document).ready(function () {
    initHamburgerNav();

    // update copyright
    $('#crYear').html(new Date().getFullYear());
});