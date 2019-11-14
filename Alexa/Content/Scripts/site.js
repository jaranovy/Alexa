function AlexaFormSubmit(formId) {
    var selectedItems = "";

    log("AlexaFormSubmit [" + formId + "]")

    $(".select-btn").each(function (index, item) {

        log("AlexaFormSubmit " + item.id )

        if ($(item).hasClass("item-selected")) {
            selectedItems += item.id + ";"
        }
    });

    $("#SelectedItems").val(selectedItems);

    log("SelectedItems [" + $("#SelectedItems").val() + "]");

    $(formId).submit();
}

function SelectButtonClicked(btn) {

    log("SelectButtonClicked() [" + btn.id + "]")

    if ($(btn).hasClass("item-selected")) {
        $(btn).removeClass("item-selected");
        $(btn).addClass("item-no-selected");

        log("SelectButtonClicked() [" + btn.id + "] - was 'item-selected' will be 'item-no-selected'")
    }
    else if ($(btn).hasClass("item-no-selected")) {
        $(btn).removeClass("item-no-selected");
        $(btn).addClass("item-selected");

        log("SelectButtonClicked() [" + btn.id + "] - was 'item-no-selected' will be 'item-selected'")
    }
}

function addZero(x, n) {
    while (x.toString().length < n) {
        x = "0" + x;
    }
    return x;
}

function timestamp() {
    var d = new Date();
    var h = addZero(d.getHours(), 2);
    var m = addZero(d.getMinutes(), 2);
    var s = addZero(d.getSeconds(), 2);
    var ms = addZero(d.getMilliseconds(), 3);
    return h + ":" + m + ":" + s + ":" + ms;
}

function log(msg) {
    console.log("[" + timestamp() + "] " + msg);
    /* alert(msg); */
}