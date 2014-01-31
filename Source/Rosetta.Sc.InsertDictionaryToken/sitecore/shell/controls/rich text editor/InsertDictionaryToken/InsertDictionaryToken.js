function GetDialogArguments() {
    return getRadWindow().ClientParameters;
}

function getRadWindow() {
    if (window.radWindow) {
        return window.radWindow;
    }

    if (window.frameElement && window.frameElement.radWindow) {
        return window.frameElement.radWindow;
    }

    return null;
}

var isRadWindow = true;

var radWindow = getRadWindow();

if (radWindow) {
    if (window.dialogArguments) {
        radWindow.Window = window;
    }
}

function scClose(text) {    
    console.log(text)
    var returnValue = {
        text: text
    };

    getRadWindow().close(returnValue);
}

function insertTokenizedText(sender, returnValue) {
    scEditor.pasteHtml(returnValue.text);
}

function scCancel() {
    getRadWindow().close();
}

// It seems that this function is unused.
function scCloseWebEdit(url) {
    window.top.returnValue = window.returnValue = url;
    window.top.close();
}

if (window.focus && Prototype.Browser.Gecko) {
    window.focus();
}

RadEditorCommandList["InsertDictionaryToken"] = function (commandName, editor, args) {
    var id;
    if (!id) {
        id = scItemID;
    }

    scEditor = editor;

    editor.showExternalDialog(
      "/sitecore/shell/default.aspx?xmlcontrol=RichText.InsertDictionaryToken&la=" + scLanguage + "&fo=" + id + (scDatabase ? "&databasename=" + scDatabase : ""),
      null, //argument
      1100,
      500,
      insertTokenizedText, //callback
      null, // callback args
      "Insert Link",
      true, //modal
      Telerik.Web.UI.WindowBehaviors.Close, // behaviors
      false, //showStatusBar
      false //showTitleBar
    );
};