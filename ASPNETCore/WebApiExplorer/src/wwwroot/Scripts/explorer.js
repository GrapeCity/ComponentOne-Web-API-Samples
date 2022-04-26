function hasClass(obj, cls) {
    return obj && obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function addClass(obj, cls) {
    if (!this.hasClass(obj, cls)) obj.className += " " + cls;
}

function removeClass(obj, cls) {
    if (hasClass(obj, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        obj.className = obj.className.replace(reg, ' ');
    }
}

function toggleClass(obj, cls) {
    if (hasClass(obj, cls)) {
        removeClass(obj, cls);
    } else {
        addClass(obj, cls);
    }
}

function toggleTabPanelVisibility(tabs, panels, i) {
    var activeID = 0, j;
    for (j = 0; j < tabs.length; j++) {
        if (tabs[j].parentElement.className.indexOf("active") >= 0) {
            activeID = j;
            break;
        }
    }
    if (activeID != i) {
        removeClass(tabs[activeID].parentElement, "active");
        removeClass(panels[activeID], "active");
        addClass(tabs[i].parentElement, "active");
        addClass(panels[i], "active");
    }
    //event.preventDefault();
}

function addEventListenerForTab(tabs, panels, i) {
    tabs[i].addEventListener("click", function (event) {
        toggleTabPanelVisibility(tabs, panels, i);
        event.preventDefault();
    });
}

function createTabs(tabId, panelId) {
    var tabCon = document.getElementById(tabId),
        tabs, panel, panels, i, className;

    if (tabCon == null) {
        return;
    }

    tabs = tabCon.getElementsByTagName("a");
    panel = document.getElementById(panelId);
    panels = new Array();

    for (i = 0; i < panel.children.length; i++) {
        className = panel.children[i].className;
        if (className && className.indexOf("tab-pane") >= 0) {
            panels.push(panel.children[i]);
        }
    }
    addClass(tabs[0].parentElement, "active");
    addClass(panels[0], "active");
    for (i = 0; i < tabs.length; i++) {
        addEventListenerForTab(tabs, panels, i);
    }
}

function createCollapseStruct(titleEle, toggleClassName) {
    titleEle.addEventListener("click", function (event) {
        var list = event.currentTarget.nextElementSibling;
        if (list) {
            toggleClass(list, toggleClassName);
            event.preventDefault();
        }
    });
}

document.onreadystatechange = function () {
    if (document.readyState !== "complete") {
        return;
    }

    var li = document.createElement("li"),
        docs = document.getElementById("docs"),
        ul = document.getElementById("navList"),
        titles = document.getElementsByClassName("collapse-title"),
        sampleNavBtn = document.getElementById("sampleNavBtn");

    createTabs("navList", "panelList");
    createTabs("sourceTab", "sourcePanel");

    //add documentation tab to the navTabs.
    if (docs) {
        docs.style.display = "";
        li.appendChild(docs);
        ul.appendChild(li);
    }

    SyntaxHighlighter.all();
    SyntaxHighlighter.defaults['toolbar'] = false;
    SyntaxHighlighter.defaults['quick-code'] = false;

    for (var i = 0; i < titles.length; i++) {

        if (titles[i] === sampleNavBtn) {
            createCollapseStruct(sampleNavBtn, "mob-hide-0");
        } else {
            createCollapseStruct(titles[i], "mob-hide-1");
        }
    }
}

function typeChanged(val) {
    $(".settings-panel-detail").hide();
    $.each($(".settings-panel-detail").find("input,select"), function (i, v) {
        var defaultvalue = $(v).attr("defaultvalue");
        if (defaultvalue !== null && defaultvalue !== undefined) {
            if ($(v).is(":checkbox")) {
                defaultvalue === "true" ? $(v).attr("checked", 'true') : $(v).removeAttr("checked");
            }
            else {
                $(v).val(defaultvalue);
            }
        }
    });
    $(".settings-panel-" + val).show();
}

function getUrl(id, url) {
    var fullUrl = url + "?";
    $.each($("#" + id + " .param-value"), function (index, input) {
        if ($(input).attr("type") == "checkbox") {
            if ($(input).attr("defaultvalue") !== $(input).is(":checked").toString()) {
                fullUrl += $(input).attr("name") + "=" + $(input).is(":checked").toString() + "&";
            }
        }
        else if ($(input).val() !== $(input).attr("defaultvalue") && $(input).val() !== 'none') {
            fullUrl += $(input).attr("name") + "=" + encodeURIComponent($(input).val()) + "&";
        }
    })
    return fullUrl.substr(0, fullUrl.length - 1);
}

function showResult(id, url) {
    var fullUrl;
    if (id.indexOf('POST') > 0) {
        fullUrl = url;
    } else {
        fullUrl = getUrl(id, url);
    }
    $("#" + id + "result").show();
    $("#" + id + "url pre").html(fullUrl);
    return fullUrl;
}

var ua = window.navigator.userAgent;
var isIeOrEdge = ua.indexOf("MSIE") > -1 || ua.indexOf("Edge") > -1 || ua.indexOf("Trident") > -1;
var includeColumnHeaders = false;

function formGet(containerId, url) {
    var container = $("#" + containerId + " .settings-panel"), $inputs, data, input,
        isFile = $("[name='type']", container).val() !== "json" && $("[name='type']", container).val() !== "xml";
    hideResultPanel(containerId);
    if (isFile) {
        window.open(url);
    } else {
        $.ajax({
            url: url,
            cache: false,
            contentType: false,
            processData: false,
            type: 'GET',
            success: function (data, success, obj) {
                processSuccessData(data, obj, containerId);
            }
        });
    }
}

function formPost(containerId, url) {
    var container = $("#" + containerId + " .settings-panel"), $inputs, 
        posted = false, body = $(document.body), $form, files = [], form,
        isFile = $("[name='type']", container).val() !== "json" && $("[name='type']", container).val() !== "xml";
    $inputs = $("*[name]", container);
    hideResultPanel(containerId);
    if ($('#' + containerId + 'iframe').length > 0) {
        $('#' + containerId + 'iframe').remove();
    }
    if (!isFile) {
        var data = new FormData();
        $inputs.each(function () {
            var input = $(this);
            if (input.attr("type") === "file") {
                $.each(input[0].files, function (i, file) {
                    data.append(input.attr("name"), file);
                });
            } else {
                data.append(input.attr("name"), input.val());
            }
        });
        $.ajax({
            url: url,
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data, success, obj) {
                processSuccessData(data, obj, containerId);
            }
        });
    } else {
        $('<iframe>').css('display', 'none').attr({ 'src': 'about:blank', 'id': containerId + 'iframe' })
        .on('load', function () {
            if (posted) {
                $(this).off('load').remove();
                return;
            }

            var formDoc = _getiframeDocument(this);
            formDoc.write("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'></head>" + "<body><form method='POST' enctype='multipart/form-data' accept-charset='utf-8' action='" + url + "'>" + "</form></body></html>");
            form = formDoc.querySelector("form");
            $form = $(form);
            copyInputs($inputs, files, $form)
            form.submit();
            fakeFiles(files)
            posted = true;
        }).appendTo(body);
    }
}

function hideResultPanel(containerId) {
    $("#" + containerId + "result .content-json-data").hide();
    $("#" + containerId + "showdata").hide();
    $("#" + containerId + "result .content-json-xml").hide();
    $("#" + containerId + "result .result-content-xml").hide();
}

function processSuccessData(data, obj, containerId) {
    var container = $("#" + containerId + " .settings-panel");
    if ($("[name='type']", container).val() === "json") {
        var grid = wijmo.Control.getControl("#" + containerId + "showdata");
        if (!grid) {
            grid = new wijmo.grid.FlexGrid("#" + containerId + "showdata");
        }
        wijmo.grid.excel.ExcelToFlexGridLoader.Load(grid, data, null);
        //if (createFlexGrid(data, containerId)) {
        $("#" + containerId + "showdata").show();
        $("#" + containerId + "result .content-json-data").show();
        //}
    } else {
        $("#" + containerId + "result .content-json-xml").show();
        $("#" + containerId + "result .result-content-xml").show();
        $("#" + containerId + "result .result-content-xml").text(obj.responseText);
    }
}

function isFlexSheet(grid) {
    return "sheets" in grid;
}

function createFlexGrid(jsonData, containerId) {

    var lineHeight = 1.4285, defaultFontSize = 12, minRowHeight = lineHeight * defaultFontSize,
        grid = wijmo.Control.getControl("#" + containerId + "showdata");

    if (!grid) {
        grid = new wijmo.grid.FlexGrid("#" + containerId + "showdata");
    }

    jsonData.sheets && jsonData.sheets.forEach(function (sheet) {
        sheet.rows && sheet.rows.forEach(function (row) {
            // #121509
            if (row.height < minRowHeight) {
                delete row.height;
            }

            // #121516
            row.cells && row.cells.forEach(function (cell) {
                if (cell && cell.isDate) {
                    cell.value = new Date(cell.value);
                }
            });
        });
    });

    if (isFlexSheet(grid)) {
        grid.loadFromJSON(jsonData);
    } else {
        var workbook = new wijmo.xlsx.Workbook();
        workbook._deserialize(jsonData);
        grid.itemsSource = null;
        grid.columns.clear();
        wijmo.grid.xlsx.FlexGridXlsxConverter.load(grid, workbook, {
            includeColumnHeaders: includeColumnHeaders,
            needGetCellStyles: true
        });
        includeColumnHeaders = false;
    }
    return true;
}

function copyInputs($inputs, files, $form) {
    $inputs.each(function () {
        var input = $(this);
        if (input.attr("type") === "file") {
            files.push(input);
            $("<span>").hide().attr('id', input.attr('id') + '_position').insertAfter(input);
            $(this).appendTo($form);
        } else {
            $(this).clone().val($(this).val()).appendTo($form);
        }
    });
}

function fakeFiles(files) {
    var body = $(document.body);
    $(files).each(function () {
        var fake = body.find("#" + this.attr('id') + '_position');
        if (isIeOrEdge) {
            $(this[0].outerHTML).val("").insertAfter(fake);
        } else {
            this.insertAfter(fake);
        }

        fake.remove();
    });
}

function _getiframeDocument(iframe) {
    var iframeDoc = iframe.contentWindow || iframe.contentDocument;
    if (iframeDoc.document) {
        iframeDoc = iframeDoc.document;
    }
    return iframeDoc;
}

function _getButtonInCurrentPanel($select) {
    var panel = $select.closest(".settings-panel-content");
    if (panel) {
        return panel.find("button");
    }
    return null;
}

function updateReportNames(folderPath, url, $select) {
    var fullUrl = url + "api/report/" + folderPath;
    var btn = _getButtonInCurrentPanel($select);
    btn && btn.attr("disabled", true);

    $.ajax({
        type: "GET",
        url: fullUrl,
        cache: false,
        success: function (data) {
            $select.html("");
            $.each(data.items, function (i, v) {
                $select.append($("<option>" + v.name + "</option>"));
            });
            btn && btn.attr("disabled", false);
            $select.change();
        }
    });
}

function updateReportExportFormats(folderPath, reportName, url, $select) {
    var fullUrl = url + "api/report/" + folderPath + "/" + reportName + "/$report/supportedformats";
    updateExportFormats(fullUrl, $select);
}

function updateReportInstanceExportFormats(reportFullPath,cacheId, url, $select) {
    var fullUrl = url + "api/report/" + reportFullPath + "/$instances/" + cacheId + "/supportedformats";
    updateExportFormats(fullUrl, $select);
}

function updatePdfExportFormats(pdfPath, url, $select) {
    var fullUrl = url + "api/pdf/" + pdfPath + "/$pdf/supportedformats";
    updateExportFormats(fullUrl, $select);
}

function updateExportFormats(fullUrl, $select) {
    var btn = _getButtonInCurrentPanel($select);
    btn && btn.attr("disabled", true);

    $.ajax({
        type: "GET",
        url: fullUrl,
        cache: false,
        success: function (data) {
            $select.html("");
            $.each(data, function (i, v) {
                $select.append($("<option value='" + v.format + "'>" + v.name + "</option>"));
            });
            btn && btn.attr("disabled", false);
            $select.change();
        }
    });
}

function updateViewFilesLink(selector, url, paths)
{
    $(selector).html("");
    $.each(paths, function (index, val) {
        $("<a></a>").attr("href", url + "api/storage/" + encodeURI(val)).text(val).css("margin", 10).appendTo($(selector));
    })
}

function formPostWithOptions(containerId, url, optionsString) {
    var body = $(document.body), posted = false;
    if ($('#' + containerId + 'iframe').length > 0) {
        $('#' + containerId + 'iframe').remove();
    }
    $('<iframe>').css('display', 'none').attr({ 'src': 'about:blank' })
    .on('load', function () {
        if (posted) {
            $(this).off('load').remove();
            return;
        }

        var formDoc = _getiframeDocument(this);
        formDoc.write("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'></head>" + "<body><form method='POST' accept-charset='utf-8' action='" + url + "'>" + "</form></body></html>");
        form = formDoc.querySelector("form");
        $form = $(form);
        generateInputs(optionsString, $form);
        form.submit();
        posted = true;
    }).appendTo(body);
}

function generateInputs(optionsString, $form) {
    optionsString.split("&").forEach(function (option) {
        var optionDic = option.split("="), key = optionDic[0],
            value = optionDic[1], input = document.createElement("input");
        input.setAttribute("name", key);
        input.setAttribute("value", value);
        $form.append(input);
    });
}