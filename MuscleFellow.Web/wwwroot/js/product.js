$(function () {
    var spinner = $("#spinner").spinner();
    spinner.spinner({
        min: 1,
        step: 1
    });
    spinner.spinner("value", 1);
});

function GetAmount() {
    var spinner = $("#spinner").spinner();
    return spinner.spinner("value");
}